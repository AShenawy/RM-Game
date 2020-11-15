﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script was created with help from tutorials provided by Brackeys
// https://www.youtube.com/c/Brackeys/about

namespace Methodyca.Core
{
    // This script handles the player inventory UI
    public class InventoryManager : MonoBehaviour
    {
        // Make this class a singleton
        #region Singleton
        public static InventoryManager instance;
        private void Awake()
        {
            if (instance == null)
                instance = this;
        }
        #endregion

        // create event when inventory items change
        public delegate void OnItemChanged();
        public event OnItemChanged itemChanged;

        // set the limit of how many items player can carry in inventory. 13 is the max using current UI icon size.
        public int space = 12;
        public List<Item> items = new List<Item>();
        public GameObject dimeSwitcherItem;

        public void Add(Item item)
        {
            if(items.Count >= space)
            {
                Debug.Log("Inventory is full");
                return;
            }

            // add item to inventory list
            items.Add(item);
            print(item.itemName + " added to inventory");

            // invoke event
            itemChanged?.Invoke();
        }


        /// <summary>
        /// Removes item from player inventory
        /// </summary>
        /// <param name="item">The item to be removed</param>
        public void Remove(Item item)
        {
            // Remove item from inventory
            items.Remove(item);
            print(item.itemName + " removed from inventory");

            // invoke event
            itemChanged?.Invoke();
        }

        public void GiveSwitcherItem()
        {
            dimeSwitcherItem.SetActive(true);
        }
    }
}