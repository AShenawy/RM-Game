using UnityEngine;


namespace Methodyca.Core
{
    // script added to room game objects for hiding objects in random locations
    public class HideObjectInRoom : MonoBehaviour, ILoadable
    {
        public GameObject objectToHide;
        public GameObject[] hidingSpots;
        private int spotNumber = -1;
        
        void Start()
        {
            LoadState();
            if (spotNumber == -1)   // if LoadState() returned nothing then assign a new value
                spotNumber = Random.Range(0, hidingSpots.Length);

            Instantiate(objectToHide, hidingSpots[spotNumber].transform);
            SaveState(spotNumber);
        }

        public void LoadState()
        {
            // don't use TryGetValue as it returns 0 if no key is found. this negates the RNG
            if (SaveLoadManager.interactableStates.ContainsKey(name))
                spotNumber = SaveLoadManager.interactableStates[name];
        }

        // saving the state without implementing ISaveable interface due to method argument
        public void SaveState(int value)
        {
            SaveLoadManager.SetInteractableState(name, value);
        }
    }
}