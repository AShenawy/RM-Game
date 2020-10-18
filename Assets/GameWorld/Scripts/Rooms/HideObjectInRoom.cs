using UnityEngine;


namespace Methodyca.Core
{
    public class HideObjectInRoom : MonoBehaviour
    {
        public GameObject objectToHide;
        public GameObject[] hidingSpots;

        // Start is called before the first frame update
        void Start()
        {
            int random = Random.Range(0, hidingSpots.Length);

            Instantiate(objectToHide, hidingSpots[random].transform);
        }
    }
}