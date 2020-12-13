using UnityEngine;


namespace Methodyca.Core
{
    //  This script handles the dimension switching when the item is clicked in inverntory (button)
    public class DimeSwitch : MonoBehaviour
    {
        SwapImageUI swapper;

        private void Start()
        {
            swapper = GetComponentInChildren<SwapImageUI>();

            if (GameManager.instance.GetCurrentRoomTag() == "QN")
                swapper.SwapImage();
        }

        public void OnClicked()
        {
            GameManager.instance.SwitchDimension();
        }
    }
}