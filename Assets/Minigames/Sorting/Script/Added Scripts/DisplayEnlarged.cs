using UnityEngine;

namespace Methodyca.Minigames.SortGame
{
    // this script handles instantiating the front view larger image prefab
    public class DisplayEnlarged : MonoBehaviour
    {
        public GameObject enlargedPrefab;

        public void InstantiateEnlarged()
        {
            Instantiate(enlargedPrefab);
        }
    }
}