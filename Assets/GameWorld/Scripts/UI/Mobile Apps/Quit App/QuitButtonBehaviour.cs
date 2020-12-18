using UnityEngine;
using Methodyca.Core;


public class QuitButtonBehaviour : MonoBehaviour
{
    public void QuitGame()
    {
        SceneManagerScript.instance.GoToLevel("0.start screen", "");
    }
}
