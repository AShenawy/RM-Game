using UnityEngine;

namespace Methodyca.Core
{
    //  This script handles the dimension switching when the item is clicked in inverntory (button)
    public class DimeSwitch : MonoBehaviour
    {
        public void OnClicked()
        {
            GameManager.instance.SwitchDimension();
        }
    }
}