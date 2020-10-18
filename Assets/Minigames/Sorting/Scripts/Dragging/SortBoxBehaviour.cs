using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Methodyca.Core;


namespace Methodyca.Minigames.SortGame
{
    // this script handles the QL/QN boxes and placement of items in them
    public class SortBoxBehaviour : MonoBehaviour, IDropHandler
    {
        // even will be called whenever a correct item is placed in the box
        public delegate void ItemDropped(int itemCount);
        public event ItemDropped onItemDropped;

        public SortingManager gameManager;
        public SortBoxBehaviour otherSortBox;
        
        public GameObject placementReference;      // the parent of the items placed in the box
        
        public string acceptableItemTag;           // The tag name for the boxes in the game either QA or QN.
        private int correctItemsInBoxCount = 0;    // how many correct items are currently in the box

        public List <GameObject> inTheBox = new List<GameObject>();     //The list for items dropped.

        public Vector2 itemPlacementShift;       // the distance between the items in box,for spacing. 

        public Sound onItemPlacedSFX;
      

        // Mouse released. 
        public void OnDrop(PointerEventData eventData) 
        {
            if (eventData.pointerDrag.GetComponent<Drag>() == null)
                return;

            // Sound Effect
            SoundManager.instance.PlaySFX(onItemPlacedSFX, true);


            // This is basically to define the things on the table and, make them snap to the box when clicked. 
            GameObject droppedItem = eventData.pointerDrag;

            if (droppedItem.GetComponent<Drag>().box != gameObject)
                otherSortBox.RemoveFromBox(droppedItem);

            // do action of placing dropped item into the box 
            PlaceInBox(droppedItem);

            if (inTheBox.Contains(droppedItem))
                return;

            inTheBox.Add(droppedItem);

            // To compare with Tags (QN and QA), with the box and see which enters which. 
            if (droppedItem.CompareTag(acceptableItemTag))
            {
                correctItemsInBoxCount++;
            }

            // invoke event to tell if correct item was added
            onItemDropped?.Invoke(correctItemsInBoxCount);
        }

        void PlaceInBox(GameObject item)
        {
            item.GetComponent<Drag>().GoIntoBox(gameObject, placementReference, itemPlacementShift);
        }

        public void RemoveFromBox(GameObject itemInBox)    //The method to remove things in the box. 
        {
            if (!inTheBox.Contains(itemInBox))      //If the object isnt in the box it wouldnt remove.
                return;

            // A check to see if the tags are correct and if there is a point award to the box already.
            if (itemInBox.CompareTag(acceptableItemTag))
            {
                if (correctItemsInBoxCount > 0)
                    correctItemsInBoxCount--;
            }
            
            inTheBox.Remove(itemInBox);

            // rearrange items existing in the box to not sit on top of the new placed item
            ReshuffleBox();

            // invoke event to check if a correct item was removed
            onItemDropped?.Invoke(correctItemsInBoxCount);
        }

        void ReshuffleBox()
        {
            foreach (GameObject placedItem in inTheBox)
                PlaceInBox(placedItem);
        }

        public void EmptyBox()
        {
            // copy list to array since we're modifying original list by removing entries
            // This prevents the foreach enumerator from going through entries
            foreach (GameObject placedItem in inTheBox.ToArray())
            {
                RemoveFromBox(placedItem);
                placedItem.GetComponent<Drag>().ReturnOriginalLocation();
            }
        }
    }
}
