using UnityEngine;
using Methodyca.Core;


public class StartButton : MonoBehaviour
{
    [SerializeField] private GameObject newgameConfirmScreen;

    public void CheckFirstRun()
    {
        if (SaveLoadManager.GetAutosaveInfo() != null)
        {
            newgameConfirmScreen.SetActive(true);
        }
        else
            StartGame();
    }

    public void StartGame()
    {
        SceneManagerScript.instance.GoToLevel("1.Act 1", "Act 1 Start Room");
    }
}
