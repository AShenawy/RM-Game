using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Methodyca.Core;

// This script handles transporting the player between rooms
public class Door : ObjectInteraction
{
    public GameObject targetRoom;
    public bool isLocked;
    [Multiline, Tooltip("In-game text to be displayed if door is locked")]
    public string responseForLocked;

    public override void InteractWithObject()
    {
        base.InteractWithObject();

        if(isLocked)
            DialogueHandler.instance.DisplayDialogue(responseForLocked);
        else
            GameManager.instance.GoToRoom(targetRoom);  // Door is unlocked and player can proceed
    }

    public override void UseWithHeldItem(Item item)
    {
        base.UseWithHeldItem(item);

        if (item == requiredItem)
        {
            isLocked = false;
            InventoryManager.instance.Remove(item);
        }
    }
}
   
