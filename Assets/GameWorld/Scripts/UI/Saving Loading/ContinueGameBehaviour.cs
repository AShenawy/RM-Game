using UnityEngine;

namespace Methodyca.Core
{
    // script for Continue button in main menu for loading autosave file
    public class ContinueGameBehaviour : MonoBehaviour
    {
        public event System.Action onLoadGame;

        private void OnEnable()
        {
            if (!SaveLoadManager.GetAutosaveAvailable())
                gameObject.SetActive(false);

            SceneManagerScript.instance.SubscribeToOnLoadEvent(this);
        }

        private void OnDisable()
        {
            SceneManagerScript.instance.UnSubscribeFromOnLoadEvent(this);
        }

        public void ContinueGame()
        {
            SaveLoadManager.LoadGameAuto();
            onLoadGame?.Invoke();   // for SceneManagerScript & others to use loaded info
        }
    }
}