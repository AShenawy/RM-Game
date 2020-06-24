﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : ObjectInteraction
{
    public GameObject targetRoom;

    public override void InteractWithObject()
    {
        base.InteractWithObject();
        GameManager.gm.GoToRoom(targetRoom);
    }
}
   