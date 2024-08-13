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
            // load latest state saved before entering the minigame scene for scene and room names info to be updated
            SaveLoadManager.LoadGameAuto();
            
            //TODO remove this line if redundant
            //SceneManagerScript.instance.GoToLevel(SaveLoadManager.currentScene, SaveLoadManager.currentRoomName, true, true);
            SceneManagerScript.instance.GoToLevel(SaveLoadManager.currentScene, SaveLoadManager.currentRoomName);
        }

        public void ResetAudio()
        {
            SoundManager.instance.StopBGM();
            SoundManager.instance.StopAllSFX();
            SoundManager.instance.PlayMainBGM();
        }
    }
}