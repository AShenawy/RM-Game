using UnityEngine;
using Methodyca.Core;


// this script handles entry to minigame scenes
public class MinigameInteraction : ObjectInteraction, ISaveable, ILoadable
{
    [Header("Specific Script Parameters")]
    [SerializeField] private MinigameHub gameHub;

    [Tooltip("Whether player can start the minigame or not")]
    public bool canStartGame;

    [SerializeField, Multiline, Tooltip("In-game text to be displayed if Can Start Game is set to false.")]
    private string responseForDisabled;

    private void OnEnable()
    {
        gameHub.isGamePlayable += ToggleInteraction;    // event drives interactability with minigame hub
    }

    protected override void Start()
    {
        base.Start();

        // we load the state after base to initialise all variables for LoadState() to work properly
        LoadState();
    }

    public override void InteractWithObject()
    {
        base.InteractWithObject();

        if (canStartGame)
            gameHub.LoadMinigame();
        else
            DialogueHandler.instance.DisplayDialogue(responseForDisabled);

    }

    public override void UseWithHeldItem(Item item)
    {
        base.UseWithHeldItem(item);

        if (usedCorrectItem)
        {
            gameHub.OnItemPlacement(item); // Do action (if any) on game hub script when correct item is used
            requiredItemsLeft--; // take down the required items count by 1
            InventoryManager.instance.Remove(item);
            SaveState(item);
        }
        else
            return;

        // if all required items are used, then unlock
        if (requiredItemsLeft < 1)
            canStartGame = true;
    }

    private void OnDisable()
    {
        gameHub.isGamePlayable -= ToggleInteraction;
    }

    void RefreshMinigameState(Item item)
    {
        gameHub.OnItemPlacement(item);
        requiredItemsLeft--;

        if (requiredItemsLeft < 1)
            canStartGame = true;
    }

    public void SaveState() { }

    void SaveState(Item item)
    {
        // Save which correct crystal was placed on desk relative to it's index in the required items list
        // Start from 1 instead of 0, since 0 will mean no crystal placed on desk. Desk requires 2 crystals, so value will be 1 or 2
        int itemIndex = requiredItems.FindIndex(x => x.itemName == item.itemName) + 1;

        // Check if a crystal was already placed or not
        SaveLoadManager.interactableStates.TryGetValue(name + "_interaction", out int savedState);
        if (savedState == 0)    // No crystal was placed before and this is the first one, so save its index
            SaveLoadManager.SetInteractableState(name + "_interaction", itemIndex);
        else if (savedState == itemIndex)   // Crystal was already placed before. Do nothing
            return;
        else        // Other crystal already placed. the crystal added now means both crystals are now on desk, so save the value 3
            SaveLoadManager.SetInteractableState(name + "_interaction", 3);
    }

    public void LoadState()
    {
        SaveLoadManager.interactableStates.TryGetValue(name + "_interaction", out int savedState);

        // if saved value isn't 0 this means a crystal (or both) were placed and we need to re-place them on desk
        if (savedState != 0)
        {
            if (savedState == 3)    // 3 means both crystals were already placed on desk
            {
                // re-do the placement action of both crystals
                RefreshMinigameState(requiredItems[0]);
                RefreshMinigameState(requiredItems[1]);
            }
            // any other number means a single crystal was placed
            else
                RefreshMinigameState(requiredItems[savedState - 1]); // subtract the 1 added in SaveState() method
        }
    }
}
