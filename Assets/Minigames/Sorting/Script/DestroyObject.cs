using UnityEngine;

namespace Methodyca.Minigames.SortGame
{
    public class DestroyObject : MonoBehaviour
    {
        public GameObject objectToDestroy;

        public void DestroyGameObject()
        {
            Destroy(objectToDestroy);
        }
    }
}