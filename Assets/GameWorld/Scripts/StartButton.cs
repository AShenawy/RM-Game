using UnityEngine;
using Methodyca.Core;


public class StartButton : MonoBehaviour
{
    public Sound clickSFX;

    public void LoadLevel(string sceneName)
    {
        SoundManager.instance.PlaySFXOneShot(clickSFX);
        //UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        SceneManagerScript.instance.GoToLevel(sceneName);
    }
}
