using UnityEngine;

namespace Methodyca.Core
{
    public class ReturnToMainGame : MonoBehaviour
    {
       
        //public BGM MainMenu;

        public void ReturnToGame()
        {
            SceneManagerScript.instance.UnloadScene();
            SoundManager.instance.StopBGM();
            SoundManager.instance.StopAllSFX();
            //this.gameObject.GetComponent<BGM>().BackToMain();
            //Debug.Log("Leggo");
        }
    }
}