using UnityEngine;

namespace Methodyca.Core
{
    // script for Continue button in main menu for loading autosave file
    public class ContinueGameBehaviour : MonoBehaviour
    {
        public event System.Action onLoadGame;
        private int latestSaveSlot;

        private void OnEnable()
        {
            //if (SaveLoadManager.GetAutosaveInfo() == null)
            //    gameObject.SetActive(false);

            latestSaveSlot = SaveLoadManager.GetLatestSaveSlot();
            if (latestSaveSlot < 0)     // in case of fresh game or no save game found
                gameObject.SetActive(false);

            print("Found save slot " + latestSaveSlot + " as latest save game");

            SceneManagerScript.instance.SubscribeToOnLoadEvent(this);
        }

        private void OnDisable()
        {
            SceneManagerScript.instance.UnSubscribeFromOnLoadEvent(this);
        }

        // button click action
        public void ContinueGame()
        {
            if (latestSaveSlot == 0)
                SaveLoadManager.LoadGameAuto();
            else
                SaveLoadManager.LoadGameState(latestSaveSlot);

            onLoadGame?.Invoke();   // for SceneManagerScript & others to use loaded info
        }
    }
}