using UnityEngine;

namespace Methodyca.Core
{
    // script for Continue button in main menu for loading autosave file
    public class ContinueGameBehaviour : MonoBehaviour
    {
        //public event System.Action onLoadGame;
        private int latestSaveSlot;

        private void OnEnable()
        {
            latestSaveSlot = SaveLoadManager.GetLatestSaveSlot();
            if (latestSaveSlot < 0)     // in case of fresh game or no save game found
                gameObject.SetActive(false);

            //SceneManagerScript.instance.SubscribeToOnLoadEvent(onLoadGame);
        }

        //private void OnDisable()
        //{
        //    SceneManagerScript.instance.UnSubscribeFromOnLoadEvent(onLoadGame);
        //}

        // button click action
        public void ContinueGame()
        {
            if (latestSaveSlot == 0)
                SaveLoadManager.LoadGameAuto();
            else
                SaveLoadManager.LoadGameState(latestSaveSlot);

            //onLoadGame?.Invoke();
            SceneManagerScript.instance.GoToLevel(SaveLoadManager.currentScene, SaveLoadManager.currentRoomName, true);
        }
    }
}