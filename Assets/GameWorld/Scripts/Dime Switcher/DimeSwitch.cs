using UnityEngine;

namespace Methodyca.Core
{
    //  This script handles the dimension switching when the item is clicked in inverntory (button)
    public class DimeSwitch : MonoBehaviour
    {
        public void OnClicked()
        {
            // moving player isn't good. The target room is inactive. need to do it through game manager instead
            //GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().DoSwitch();
            GetComponentInChildren<SwapImageUI>().SwapImage();
        }
    }
}