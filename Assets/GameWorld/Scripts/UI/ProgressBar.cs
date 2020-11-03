using UnityEngine;
using UnityEngine.UI;

namespace Methodyca.Core
{
    // this class is for loading screens
    public class ProgressBar : MonoBehaviour
    {
        public Image progressBar;
        public GameObject startButton;
        [HideInInspector] public bool isStartClicked;

        public void OnStart()
        {
            isStartClicked = true;
        }
    }
}