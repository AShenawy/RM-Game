using UnityEngine;
using Methodyca.Core;

// script for starting a new playthrough
public class StartButton : MonoBehaviour
{
    [SerializeField] private GameObject newgameConfirmScreen;
    public GameObject menu;

    public void CheckFirstRun()
    {
        menu.SetActive(false);
        if (SaveLoadManager.GetAutosaveInfo() != null)
        {
            newgameConfirmScreen.SetActive(true);
        }
        else
            StartGame();
    }

    public void StartGame()
    {
        // clear all data in SaveLoadManager for a fresh playthrough
        SaveLoadManager.ClearInventoryItems();
        SaveLoadManager.ClearMinigamesComplete();

        // interactions should be removed as this is a fresh start
        SceneManagerScript.instance.GoToLevel("1.Act 1", "Act 1 Start Room", false);
    }

    public void TurningMenuOn()
    {
        menu.SetActive(true);
    }
}
