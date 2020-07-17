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
        public bool canPickUp;
        [Tooltip("Can the player interact with the object?")]
        public bool canInteract = true;

        // this method will be overridden for when object is inspected
        public virtual string InspectObject()
        {
            print("Inspecting " + name);
        
            return inGameDescription;
        }

        // this method will be overridden for when object is interacted with
        public virtual void InteractWithObject()
        {
            print("Interacting with " + name);
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