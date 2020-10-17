using UnityEngine;

namespace Methodyca.Minigames.SortGame
{
    // this script destroys instantiated enlarged object display prefab via UI button
    public class DestroyObject : MonoBehaviour
    {
        public GameObject objectToDestroy;

        public void DestroyGameObject()
        {
            Destroy(objectToDestroy);
        }
    }
}