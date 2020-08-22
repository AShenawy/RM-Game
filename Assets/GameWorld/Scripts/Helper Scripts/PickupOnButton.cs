using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Methodyca.Core
{
    // This is a helper script which makes a button give the player a pickup
    public class PickupOnButton : MonoBehaviour
    {
        // The item to give the player
        public Item pickupItem;

        public void GiveItem(Item item)
        {
            InventoryManager.instance.Add(item);
        }
    }
}