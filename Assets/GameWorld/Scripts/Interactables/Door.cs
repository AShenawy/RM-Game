using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Methodyca.Core;

// This script handles transporting the player between rooms
public class Door : ObjectInteraction
{
    public GameObject targetRoom;
    public bool isLocked;

    public override void InteractWithObject()
    {
        base.InteractWithObject();


        if(!isLocked)
            GameManager.instance.GoToRoom(targetRoom);
    }
}
   
