using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : ObjectInteraction
{

    public override void InteractWithObject()
    {
        base.InteractWithObject();
        PickUpObject();
        
    }

    public override void PickUpObject()
    {
        base.PickUpObject();
        if (canPickUp)
        {
            print(name + " added to inventory.");
        }
        else
        {
            print("I can't pick the " + name + " right now");
        }
    }
}
   
