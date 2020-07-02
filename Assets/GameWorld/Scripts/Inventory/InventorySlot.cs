using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This script was created with help from tutorials provided by Brackeys
// https://www.youtube.com/c/Brackeys/about

namespace Methodyca.Core
{
    // This script is for each instant of inventory item/slot at the top of screen.
    public class InventorySlot : MonoBehaviour
    {
        public Image icon;

        private Item item;

        public void AddItem(Item newItem)
        {
            // set inventory slot info to the new item's
            item = newItem;
            icon.sprite = item.icon;
            icon.enabled = true;
        }

        public void ClearSlot()
        {
            icon.enabled = false;
            icon.sprite = null;
            item = null;
        }

        public void RemoveItem()
        {
            InventoryManager.instance.Remove(item);
        }
    }
}