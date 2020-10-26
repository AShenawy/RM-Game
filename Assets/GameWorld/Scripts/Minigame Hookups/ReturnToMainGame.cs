using UnityEngine;

namespace Methodyca.Core
{
    public class ReturnToMainGame : MonoBehaviour
    {
        public void ReturnToGame()
        {
            SoundManager.instance.StopBGM();
            SoundManager.instance.StopAllSFX();
            SceneManagerScript.instance.UnloadScene();
        }
    }
}