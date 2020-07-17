using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Methodyca.Core
{
    public class PlayerItemHandler : MonoBehaviour
    {
        public Item heldItem;

        private void Update()
        {
            if (Input.GetButtonDown("Fire2"))
                PutItemBack();
        }

        public void HoldItem(Item inventoryItem)
        {
            heldItem = inventoryItem;
        }

        private void PutItemBack()
        {
            heldItem = null;
        }
    }
}