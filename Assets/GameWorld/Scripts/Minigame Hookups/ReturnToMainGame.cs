using UnityEngine;

namespace Methodyca.Core
{
    public class ReturnToMainGame : MonoBehaviour
    {
        public void ReturnToGame()
        {
            SceneManagerScript.instance.UnloadScene();
        }

        public void ResetAudio()
        {
            SoundManager.instance.StopBGM();
            SoundManager.instance.StopAllSFX();
            SoundManager.instance.PlayMainBGM();
        }
    }
}