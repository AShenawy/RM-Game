using Methodyca.Core;
using UnityEngine;


// This script handles initiation of a minigame when interacted with
public class StartSortingGame : ObjectInteraction
{
    [Header("Specific Script Parameters")]
    [SerializeField] private bool canStartGame;


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
