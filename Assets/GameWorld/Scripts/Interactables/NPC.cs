using UnityEngine;
using System.Collections.Generic;
using Methodyca.Core;

// This script is for non-playable characters (NPCs) which the player will interact with
public class NPC : ObjectInteraction, ISaveable, ILoadable
{
    [Header("Specific NPC paramters")]
    public bool canSpeakWith = true;
    [TextArea] public string[] textDialogues;
    [Tooltip("Dialogue to display if player can't speak to NPC")] public string onFailedSpeakText;
    public System.Action<NPC, Item> onCorrectItemUsed;
    public System.Action onGivenAllItems;

    public List<Item> givenItems = new List<Item>();        // TODO make it private after debugging


    protected override void Start()
    {
        base.Start();
        LoadState();
    }

    public override void InteractWithObject()
    {
        if (canSpeakWith)
            Talk();
        else
            DialogueHandler.instance.DisplayDialogue(onFailedSpeakText);
    }


    public override void UseWithHeldItem(Item item)
    {
        base.UseWithHeldItem(item);

        if (usedCorrectItem)
        {
            requiredItems.Remove(item);     // remove the item required from the list
            givenItems.Add(item);       // add item to givenItems list for when loading state
            InventoryManager.instance.Remove(item);
            onCorrectItemUsed?.Invoke(this, item);
        }
        else
            return;

        // if all required items are given, then allow operation (if set to false)
        if (requiredItems.Count < 1)
        {
            canSpeakWith = true;
            onGivenAllItems?.Invoke();
            DialogueHandler.instance.DisplayDialogue(itemsProvidedText);
        }

        SaveState();        // update given items & speakable states
    }

    void Talk()
    {
        // feed text dialogues to dialogue output box
        DialogueHandler.instance.DisplayDialogue(textDialogues);
    }

    public void SaveState()
    {
        foreach (Item i in givenItems)
            SaveLoadManager.SetInteractableState($"{name}_given_{i.name}", 1);

        SaveLoadManager.SetInteractableState(name + "_speakable", canSpeakWith ? 1 : 0);
    }

    public void LoadState()
    {
        foreach (Item i in requiredItems.ToArray())
        {
            if (SaveLoadManager.interactableStates.TryGetValue($"{name}_given_{i.name}", out int givenState))
            {
                requiredItems.Remove(i);
                givenItems.Add(i);
            }
        }

        if (SaveLoadManager.interactableStates.TryGetValue(name + "_speakable", out int speakableState))
            canSpeakWith = (speakableState == 0) ? false : true;
    }
}