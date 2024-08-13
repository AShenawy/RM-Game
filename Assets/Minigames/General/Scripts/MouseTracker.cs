using UnityEngine;


namespace Methodyca.Minigames
{
    public class MouseTracker : MonoBehaviour
    {
        public float mouseX;
        public float mouseY;
        public float mouseZ;
        public float screenHeight;
        public float screenWidth;

        // Update is called once per frame
        void Update()
        {
            Vector3 mouseInput = Input.mousePosition;
            mouseX = mouseInput.x;
            mouseY = mouseInput.y;
            mouseZ = mouseInput.z;
            screenHeight = Screen.height;
            screenWidth = Screen.width;
        }
    }
}