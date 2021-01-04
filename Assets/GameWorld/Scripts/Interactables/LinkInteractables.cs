using UnityEngine;


namespace Methodyca.Core
{
    // this script links 2 interactable objects for loading the game states if not in same room
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