using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Methodyca.Core;

// This script handles transporting the player between rooms
public class Door : ObjectInteraction, ISaveable, ILoadable
{
    [Header("Specific Door Parameters")]
    public GameObject targetRoom;
    public bool isLocked;
    [Multiline, Tooltip("In-game text to be displayed if door is locked")]
    public string responseForLocked;
    public Sound doorLockedSFX;
    public Sound doorOpenSFX;


    protected override void Start()
    {
        LoadState();

        base.Start();
    }

    public override void InteractWithObject()
    {
        base.InteractWithObject();

        if (isLocked)
        {
            DialogueHandler.instance.DisplayDialogue(responseForLocked);
            SoundManager.instance.PlaySFXOneShot(doorLockedSFX);
        }
        else
        {
            GameManager.instance.GoToRoom(targetRoom);  // Door is unlocked and player can proceed
            SoundManager.instance.PlaySFXOneShot(doorOpenSFX);
        }
    }

    public override void UseWithHeldItem(Item item)
    {
        base.UseWithHeldItem(item);

        if (usedCorrectItem)
        {
            requiredItemsLeft--; // take down the required items count by 1
            InventoryManager.instance.Remove(item);
        }
        else
            return;

        // if all required items are used, then unlock
        if (requiredItemsLeft < 1)
        {
            Unlock();
        }
    }

    public void Unlock()
    {
        isLocked = false;
        SaveState();
    }

    public void LoadState()
    {
        if (SaveLoadManager.interactableStates.TryGetValue(name, out int lockedState))
            isLocked = (lockedState == 0) ? false : true;
    }

    public void SaveState()
    {
        SaveLoadManager.SetInteractableState(name, isLocked ? 1 : 0);
    }
}
   
