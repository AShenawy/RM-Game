using UnityEngine;

namespace Methodyca.Core
{
    // script for Continue button in main menu for loading autosave file
    public class ContinueGameBehaviour : MonoBehaviour
    {

        private void Start()
        {
            if (!SaveLoadManager.GetAutosaveAvailable())
                gameObject.SetActive(false);
        }

        public void ContinueGame()
        {
            SaveLoadManager.LoadGameAuto();
        }
    }
}