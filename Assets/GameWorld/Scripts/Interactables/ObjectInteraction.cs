using UnityEngine;
using UnityEngine.UI;

namespace Methodyca.Core
{
    // This script is the base class for all interactable game objects in game world
    public class ObjectInteraction : MonoBehaviour
    {
        [TextArea, Tooltip("Information when player inspects the object.")]
        public string inGameDescription;
        
        [Tooltip("Whether this object can be picked to inventory or not")]
        public bool canPickUp = false;
        
        [Tooltip("Can the player interact with this object?")]
        public bool canInteract = true;

        [Tooltip("Does this object require an item to use with it?")]
        public bool isItemRequired = false;
        [Tooltip("The item required for use with this object")]
        public Item requiredItem;

        // this method will be overridden for when object is inspected
        public virtual string InspectObject()
        {
            return inGameDescription;
        }

        // this method will be overridden for when object is interacted with
        public virtual void InteractWithObject()
        {

        }

        public virtual void UseHeldItem(Item item)
        {
            print("using " + item.name);
        }

        // this method will be overridden for when object is picked up
        public virtual void PickUpObject()
        {
            if (canPickUp)
            {
                print("Picked up " + name);
            }
            else
            {
                print("Cannot pick up " + name);
            }
        }
    }
}