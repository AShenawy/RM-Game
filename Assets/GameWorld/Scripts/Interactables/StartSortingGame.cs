using Methodyca.Core;
using UnityEngine;
using UnityEngine.SceneManagement;


// This script handles initiation of a minigame when interacted with
public class StartSortingGame : ObjectInteraction
{
    [Header("Specific Script Parameters")]
    [SerializeField, Tooltip("The scene name of minigame to be loaded")]
    private string minigameSceneName;
    [SerializeField, Multiline, Tooltip("In-game text to be displayed if game unplayable yet")]
    private string responseForDisabled;

    private bool canStartGame;  // Check whether player can start sorting game or not

    public override void InteractWithObject()
    {
        base.InteractWithObject();

        if (canStartGame)
            SceneManagerScript.instance.LoadSceneAdditive(minigameSceneName);
        else
            DialogueHandler.instance.DisplayDialogue(responseForDisabled);

    }

    public override void UseWithHeldItem(Item item)
    {
        base.UseWithHeldItem(item);

        if (usedCorrectItem)
        {
            requiredItemsLeft--; // take down the required items count by 1
            InventoryManager.instance.Remove(item);
        }
        else
            return;

        // if all required items are used, then unlock
        if (requiredItemsLeft < 1)
            canStartGame = true;
    }
}
