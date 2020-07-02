using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Methodyca.Core;

public class PickUp : ObjectInteraction
{
    public Item item;

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
            InventoryManager.instance.Add(item);
            Destroy(gameObject);    // destroy object in game world after it's moved to inventory
        }
        else
        {
            print("I can't pick the " + item.name + " right now");
        }
    }
}
   
