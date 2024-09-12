using UnityEngine;
using Methodyca.Core;
using System.Collections.Generic;

// This script handles general use of objects
[RequireComponent(typeof(SwitchImageDisplay))]
public class Operate : ObjectInteraction, ISaveable, ILoadable
{
    [Header("Specific Operate Parameters")]
    [Tooltip("Can the player operate this object?")]
    public bool canOperate;
    [Tooltip("Dialogue to display on successful operate")]
    public string onOperateSuccessText;
    [Tooltip("Dialogue to display on failed operate")]
    public string onOperateFailText;
    
    public event System.Action<Operate, Item> onCorrectItemUsed;     // keeps track of progress for mirror QL/QN objects
    public event System.Action<Operate> onOperation;      // keeps track of progress for linked objects

    private bool isOperated;    // check for saving/loading object state
    public List<Item> givenItems = new List<Item>();        // TODO make it private after debugging

    public Sound SFX; //Sound of the interaction
    public Sound WrongSFX;
    public Sound CorrectSFX;

    protected override void Start()
    {
        // keep above LoadState() so requiredItemsLeft isn't overwritten by base.Start()
        base.Start();

        LoadObjectState();
    }

    public override void LoadObjectState()
    {
        LoadState();
        if (isOperated)
            Use();
    }

    public override void InteractWithObject()
    {
        if (canOperate)
        {
            Use();
            DialogueHandler.instance.DisplayDialogue(onOperateSuccessText);
        }
        else
            PlayError();
            DialogueHandler.instance.DisplayDialogue(onOperateFailText);
            
    }

    void Use()
    {
        GetComponent<SwitchImageDisplay>().SwitchImage();
        ToggleInteraction(false);

        isOperated = true;
        SoundManager.instance.PlaySFXOneShot(SFX);
        onOperation?.Invoke(this);
        SaveState();    // update _operated state
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
            SoundManager.instance.PlaySFXOneShot(CorrectSFX);
        }
        else
        {
            PlayError();
            return;
        }

        // if all required items are given, then allow operation (if set to false)
        if (requiredItems.Count < 1)
        {
            canOperate = true;
            DialogueHandler.instance.DisplayDialogue(itemsProvidedText);
        }

        SaveState();        // update given items & operateable states
    }

    public void SaveState()
    {
        foreach (Item i in givenItems)
            SaveLoadManager.SetInteractableState($"{name}_given_{i.name}", 1);

        SaveLoadManager.SetInteractableState(name + "_operateable", canOperate ? 1 : 0);
        SaveLoadManager.SetInteractableState(name + "_operated", isOperated ? 1 : 0);
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

        if (SaveLoadManager.interactableStates.TryGetValue(name + "_operateable", out int operateableState))
            canOperate = (operateableState == 0) ? false : true;

        if (SaveLoadManager.interactableStates.TryGetValue(name + "_operated", out int operatedState))
            isOperated = (operatedState == 0) ? false : true;
    }
    void PlayError()
    {
        SoundManager.instance.PlaySFXOneShot(WrongSFX);
    }
}