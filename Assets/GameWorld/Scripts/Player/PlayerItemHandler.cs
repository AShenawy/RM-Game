using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Methodyca.Core
{
    public class PlayerItemHandler : MonoBehaviour
    {
        public Item heldItem;

        public delegate void OnItemHeld();
        public event OnItemHeld itemHeld;

        private void Update()
        {
            if (Input.GetButtonDown("Fire2"))
                RemoveFromHand();
        }

        public void HoldInHand(Item inventoryItem)
        {
            heldItem = inventoryItem;

            GameManager.instance.isPlayerHoldingItem = true;

            // Change cursor image to that of item being held
            CursorManager.instance.SetCursor(CursorTypes.ItemHeld, heldItem.cursorImage);
        }

        public void RemoveFromHand()
        {
            heldItem = null;

            GameManager.instance.isPlayerHoldingItem = false;
        }
    }
}