using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Methodyca.Core;

// This script handles transporting the player between rooms
public class Door : ObjectInteraction
{
    public GameObject targetRoom;

    public override void InteractWithObject()
    {
        base.InteractWithObject();
        GameManager.instance.GoToRoom(targetRoom);
    }
}
   
