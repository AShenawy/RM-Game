using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script was created with help from tutorials provided by Brackeys
// https://www.youtube.com/c/Brackeys/about

namespace Methodyca.Core
{
    // This script handles the inventory UI presentation at top of screen.
    public class InventoryUI : MonoBehaviour
    {
        private InventoryManager inventory;
        private InventorySlot[] slots;


        void Start()
        {
            inventory = InventoryManager.instance;
            inventory.itemChanged += UpdateUI;      // subscribe to inventory item change event

            slots = GetComponentsInChildren<InventorySlot>(true);
        }

        void UpdateUI()
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if(i < inventory.items.Count)
                {
                    slots[i].AddItem(inventory.items[i]);   // fill slot with item carried in inventory
                }
                else
                {
                    slots[i].ClearSlot();
                }
            }
        }

        // unsubscribe from event upon disable of this script
        void OnDisable()
        {
            InventoryManager.instance.itemChanged -= UpdateUI;
        }
    }
}