using UnityEngine;

namespace Methodyca.Core
{
    public class Rotator : MonoBehaviour
    {
        public GameObject rotateAround;
        public float rotationRate;
        

        // Update is called once per frame
        void Update()
        {
            gameObject.transform.RotateAround(rotateAround.transform.position, Vector3.forward * -1, rotationRate * Time.deltaTime);
        }
    }
}