using UnityEngine;

namespace Methodyca.Core
{
    // This script is the base class for all interactable game objects in game world
    public class ObjectInteraction : MonoBehaviour
    {
        [Header("General Object Interaction Parameters")]
        [TextArea, Tooltip("Information when player inspects the object.")]
        public string inGameDescription;
        
        [Tooltip("Whether this object can be picked to inventory or not")]
        public bool canPickUp = false;
        [Tooltip("Dialogue to display on successful pick up of object")]
        public string PickUpSuccessText;
        [Tooltip("Dialogue to display on failing to pick up object")]
        public string PickUpFailText;
        
        [Tooltip("Can the player interact with this object?")]
        public bool canInteract = true;

        [Tooltip("Does this object require an item to use with it?")]
        public bool isItemRequired = false;
        [Tooltip("The item required for use with this object")]
        public Item requiredItem;

        // this method will be overridden for when object is inspected
        public virtual string InspectObject()
        {
            DialogueHandler.instance.DisplayDialogue(inGameDescription);
            return inGameDescription;
        }

        // this method will be overridden for when object is interacted with
        public virtual void InteractWithObject()
        {

        }

        public virtual void UseWithHeldItem(Item item)
        {
            DialogueHandler.instance.DisplayDialogue($"Used {item.name}");
        }

        // this method will be overridden for when object is picked up
        public virtual void PickUpObject()
        {
            if (canPickUp)
            {
                DialogueHandler.instance.DisplayDialogue(PickUpSuccessText);
            }
            else
            {
                DialogueHandler.instance.DisplayDialogue(PickUpFailText);
            }
        }
    }
}