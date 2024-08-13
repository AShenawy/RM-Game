using UnityEngine;


namespace Methodyca.Core
{
    // this script links 2 interactable objects, forcing the linked object to load its state
    // before becoming active and running Start() on itself
    public class LinkInteractables : MonoBehaviour
    {
        public ObjectInteraction linkedInteractableObject;

        // Use this for initialization
        void Start()
        {
            linkedInteractableObject.LoadObjectState();
        }
    }
}