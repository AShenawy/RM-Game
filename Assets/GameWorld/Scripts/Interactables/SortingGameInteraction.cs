﻿using UnityEngine;
using Methodyca.Core;


// this script handles entry to minigame scenes
public class SortingGameInteraction : ObjectInteraction
{
    [Header("Specific Script Parameters")]
    [SerializeField] private SortingGameHub gameHub;

    [Tooltip("Whether player can start sorting game or not")]
    public bool canStartGame;

    [SerializeField, Multiline, Tooltip("In-game text to be displayed if Can Start Game is set to false.")]
    private string responseForDisabled;


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
            gameHub.DisplayCrystalOnCharger(item.name);
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
