using UnityEngine;
using Methodyca.Core;

// script for the quit game button in the game menu
public class QuitButtonBehaviour : MonoBehaviour
{
    public void QuitGame()
    {
        SaveLoadManager.SaveGameAuto();
        
        // if game is reloaded at another save point, don't load old badge data
        SceneManagerScript.instance.minigamesWon.Clear();   
        // remove saved interactions. They will be updated by next load
        SceneManagerScript.instance.GoToLevel("0.start screen", "", false);
    }
}
