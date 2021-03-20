using UnityEngine;

namespace Methodyca.Core
{
    // this script clears any hanging game objects when leaving a minigame and returning to the main game
    // meant to be used with the quit game button in a minigame
    public class DestroyMinigameObjects : MonoBehaviour
    {
        public GameObject[] objectsToDestroy;
        [Tooltip("Use this if the object cannot be added directly to 'Objects To Destroy', like objects brought from another scene or instantiated in runtime.")]
        public string[] namedObjectsToDestroy;

        // button click action
        public void DestroyObjects()
        {
            if (objectsToDestroy.Length > 0)
            {
                Debug.Log($"{objectsToDestroy.Length} listed objects will be destroyed on exiting minigame.");
                foreach (GameObject go in objectsToDestroy)
                    Destroy(go);
            }

            if (namedObjectsToDestroy.Length > 0)
            {
                Debug.Log($"{namedObjectsToDestroy.Length} named objects will be destroyed on exiting minigame.");
                foreach (string go in namedObjectsToDestroy)
                    Destroy(GameObject.Find(go));
            }
        }
    }
}