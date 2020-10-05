using System.Collections.Generic;
using UnityEngine;

namespace Methodyca.Core
{
    // This script is the base class for all interactable game objects in game world
    [RequireComponent(typeof(BoxCollider))]
    public abstract class ObjectInteraction : MonoBehaviour
    {
        [Header("General Object Interaction Parameters")]
        [Tooltip("Can the player interact with this object?")]
        public bool canInteract = true;
        [TextArea, Tooltip("Information when player inspects the object")]
        public string inGameDescription;
        
        [Space]
        [Tooltip("Whether this object can be picked to inventory or not")]
        public bool canPickUp = false;
        [Tooltip("Dialogue to display on successful pick up of object")]
        public string PickUpSuccessText;
        [Tooltip("Dialogue to display on failing to pick up object")]
        public string PickUpFailText;
        
        [Space]
        [Tooltip("Does this object require an item to use with it?")]
        public bool isItemRequired = false;
        [Tooltip("The item(s) required to allow using this object")]
        public List<Item> requiredItems = new List<Item>();
        [Tooltip("Dialogue to display if using wrong item")]
        public string wrongItemText;

        protected bool usedCorrectItem;     // check when player uses item with object that requires one
        protected int requiredItemsLeft;    // counter for how many items before object can be used/unlocked

        public Sound InterSFX;

        protected virtual void Start()
        {
            if (!canInteract)
                ToggleInteraction(false);

            // set the counter value
            requiredItemsLeft = requiredItems.Count;
        }

        // this method will be overridden by derived classes
        public virtual void InspectObject()
        {
            DialogueHandler.instance.DisplayDialogue(inGameDescription);
        }

        // this method will be overridden by derived classes
        public virtual void InteractWithObject()
        {
            

        }

        // this method will be overridden by derived classes
        public virtual void UseWithHeldItem(Item item)
        {
            if (isItemRequired)
            {
                // reset the check
                usedCorrectItem = false;

                // check if used item is the same one required
                for (int i = 0; i < requiredItems.Count; i++)
                {
                    // if used item is correct, mark as so
                    if (item == requiredItems[i])
                        usedCorrectItem = true;
                }
            }
            else
            {
                // if no item is required then any item used with this object doesn't work
                DialogueHandler.instance.DisplayDialogue(wrongItemText);
                return;
            }

            if (usedCorrectItem)
                DialogueHandler.instance.DisplayDialogue($"Used {item.itemName}");
            else
            {
                // if used item doesn't match with those required, then don't proceed further
                DialogueHandler.instance.DisplayDialogue(wrongItemText);
            }
        }

        // this method will be overridden by derived classes
        public virtual void PickUpObject()
        {
            if (canPickUp)
                DialogueHandler.instance.DisplayDialogue(PickUpSuccessText);
            else
                DialogueHandler.instance.DisplayDialogue(PickUpFailText);
        }

        // disable interaction collider on object to allow interaction with things below/inside it
        protected void ToggleInteraction(bool value)
        {
            canInteract = value;
            gameObject.GetComponent<Collider>().enabled = value;
        }
    }
}