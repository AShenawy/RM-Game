using UnityEngine;

namespace Methodyca.Core
{
    public class ReturnToMainGame : MonoBehaviour
    {
        public void ReturnToGame()
        {
            SceneManagerScript.instance.UnloadScene();
            SoundManager.instance.StopBGM();
            SoundManager.instance.StopAllSFX();
            Debug.Log("Leggo"); 
        }
    }
}