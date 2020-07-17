using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Methodyca.Core;

// This script handles transporting the player between rooms
public class Door : ObjectInteraction
{
    public GameObject targetRoom;
    public bool isLocked;
    public string unlockItemName;

    public override void InteractWithObject()
    {
        base.InteractWithObject();

        if(isLocked)
        {
            // TODO Check if player is using correct item
        }
        else
            GameManager.instance.GoToRoom(targetRoom);  // Door is unlocked and player can proceed
    }
}
   
