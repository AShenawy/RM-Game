using UnityEngine;

namespace Methodyca.Core
{
    // This script is the base class for all interactable game objects in game world
    public class ObjectInteraction : MonoBehaviour
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
        [Tooltip("The item required for use with this object")]
        public Item requiredItem;
        [Tooltip("Dialogue to display if using wrong item")]
        public string wrongItemText;

        private void Start()
        {
            if(!canInteract)
                DisableInteraction();
        }

        // this method will be overridden by derived classes
        public virtual string InspectObject()
        {
            DialogueHandler.instance.DisplayDialogue(inGameDescription);
            return inGameDescription;
        }

        // this method will be overridden by derived classes
        public virtual void InteractWithObject()
        {

        }

        // this method will be overridden by derived classes
        public virtual void UseWithHeldItem(Item item)
        {
            if (item == requiredItem)
                DialogueHandler.instance.DisplayDialogue($"Used {item.name}");
            else
                DialogueHandler.instance.DisplayDialogue(wrongItemText);
        }

        // this method will be overridden by derived classes
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

        // disable interaction collider on object to allow interaction with things below/inside it
        protected void DisableInteraction()
        {
            canInteract = false;
            gameObject.GetComponent<Collider>().enabled = false;
        }
    }
}