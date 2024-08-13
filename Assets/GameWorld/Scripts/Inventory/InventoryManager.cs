using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script was created with help from tutorials provided by Brackeys
// https://www.youtube.com/c/Brackeys/about

namespace Methodyca.Core
{
    // This script handles the player inventory UI
    public class InventoryManager : MonoBehaviour, ISaveable, ILoadable
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

        private void Start()
        {
            // when game is loaded from save file will update the inventory
            // Method call delayed to allow InventoryUI script to subscribe to itemChanged event and show added items on UI slots
            Invoke("LoadState", 0.05f);
        }

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
            SaveState(item);
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
            SaveState(item, false);
        }

        public void GiveSwitcherItem()
        {
            dimeSwitcherItem.SetActive(true);
        }

        public void SaveState()
        {
            //TODO remove if redundant
            //List<string> heldItemsNames = new List<string>();

            //for (int i = 0; i < items.Count; i++)
            //    heldItemsNames.Add(items[i].name);

            //SaveLoadManager.SetCurrentInventoryItems(items);
        }

        void SaveState(Item item, bool isAdded = true)
        {
            if (isAdded)
                SaveLoadManager.AddInventoryItem(item);
            else
            {
                // item is removed from inventory
                SaveLoadManager.RemoveInventoryItem(item);
            }
        }

        public void LoadState()
        {
            List<string> heldItemsNames = new List<string> (SaveLoadManager.currentInventoryItems);
            items.Clear();

            foreach (string name in heldItemsNames.ToArray())
                Add(Resources.Load<Item>($"Inventory Items/{name}"));
        }
    }
}