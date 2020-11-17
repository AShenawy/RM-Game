using UnityEngine;
using Methodyca.Core;


public class StartButton : MonoBehaviour
{
    public Sound clickSFX;

    public void StartNewGame()
    {
        SoundManager.instance.PlaySFXOneShot(clickSFX);
        SceneManagerScript.instance.GoToLevel("1.Act 1", "Act 1 Start Room");
    }
}
