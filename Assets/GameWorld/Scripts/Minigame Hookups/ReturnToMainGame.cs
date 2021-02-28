using UnityEngine;

namespace Methodyca.Core
{
    // this script is for quitting minigames
    public class ReturnToMainGame : MonoBehaviour
    {
        // button click action
        // for additive-type loaded scenes
        public void ReturnToGame()
        {
            SceneManagerScript.instance.UnloadScene();
        }

        // button click action
        // for single-type loaded scenes
        public void QuitMinigame()
        {
            // load latest state saved before entering the minigame scene
            SaveLoadManager.LoadGameAuto();
            SceneManagerScript.instance.GoToLevel(SaveLoadManager.currentScene, SaveLoadManager.currentRoomName, true);
        }

        public void ResetAudio()
        {
            SoundManager.instance.StopBGM();
            SoundManager.instance.StopAllSFX();
            SoundManager.instance.PlayMainBGM();
        }
    }
}