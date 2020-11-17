using UnityEngine;

namespace Methodyca.Core
{
    // script for Continue button in main menu for loading autosave file
    public class ContinueGameBehaviour : MonoBehaviour
    {
        public event System.Action onLoadGame;

        private void Start()
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
            
           
            System.Action onLoadComplete = SaveLoadManager.LoadGameAuto();
            onLoadComplete += OnGameStateLoaded;
            onLoadComplete();
            onLoadComplete -= OnGameStateLoaded;
        }

        void OnGameStateLoaded()
        {
            onLoadGame?.Invoke();   // for SceneManagerScript & others to use loaded info
        }
    }
}