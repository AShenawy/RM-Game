using UnityEngine;
using Methodyca.Core;


public class QuitButtonBehaviour : MonoBehaviour
{
    public void QuitGame()
    {
        SaveLoadManager.SaveGameAuto();
        
        // if game is reloaded at another save point, don't load old badge data
        SceneManagerScript.instance.minigamesWon.Clear();   
        SceneManagerScript.instance.GoToLevel("0.start screen", "");
    }
}
