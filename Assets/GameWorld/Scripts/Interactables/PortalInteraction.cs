using UnityEngine;
using Methodyca.Core;


public class PortalInteraction : ObjectInteraction, ILoadable
{
    [Header("Specific Script Parameters")]
    [Tooltip("Whether the player can use the portal or not")]
    public bool canUsePortal;
    [SerializeField, Multiline, Tooltip("In-game text to be displayed if Can Use Portal is set to false.")]
    private string responseForDisabled;

    private PortalController portalControl;


    protected override void Start()
    {
        portalControl = GetComponent<PortalController>();
        base.Start();

        // we load the state after the above to initialise all variables for LoadState() to work properly
        LoadState();
    }

    public override void InteractWithObject()
    {
        base.InteractWithObject();

        if(canUsePortal)
        {
            portalControl.PlayTransition();
        } 
        else
            DialogueHandler.instance.DisplayDialogue(responseForDisabled);
    }

    public override void UseWithHeldItem(Item item)
    {
        base.UseWithHeldItem(item);

        if (usedCorrectItem)
        {
            portalControl.OnItemPlacement(item);
            requiredItemsLeft--;
            InventoryManager.instance.Remove(item);
            SaveState(item);
        }
        else
            return;

        if (requiredItemsLeft < 1)
            canUsePortal = true;
    }

    void RefreshPortalState(Item item)
    {
        portalControl.OnItemPlacement(item);
        requiredItemsLeft--;
        
        if (requiredItemsLeft < 1)
            canUsePortal = true;
    }

    // ISaveable not implemented due to method argument
    public void SaveState(Item item)
    {
        // Save which correct crystal was placed on portal relative to it's index in the required items list
        // Start from 1 instead of 0, since 0 will mean no crystal placed on portal. Portal requires 2 crystals, so value will be 1 or 2
        int itemIndex = requiredItems.FindIndex(x => x.itemName == item.itemName) + 1;
        
        // Check if a crystal was already placed or not
        SaveLoadManager.interactableStates.TryGetValue(name, out int savedState);
        if (savedState == 0)    // No crystal was placed before and this is the first one, so save its index
            SaveLoadManager.SetInteractableState(name, itemIndex);
        else if (savedState == itemIndex)   // Crystal was already placed before. Do nothing
            return;
        else        // Other crystal already placed. the crystal added now means both crystals are now on portal, so save the value 3
            SaveLoadManager.SetInteractableState(name, 3);
    }

    public void LoadState()
    {
        SaveLoadManager.interactableStates.TryGetValue(name, out int savedState);
        
        // if saved value isn't 0 this means a crystal (or both) were placed and we need to re-place them on the portal
        if (savedState != 0)
        {
            if (savedState == 3)    // 3 means both crystals were already placed on the portal
            {
                // re-do the placement action of both crystals
                RefreshPortalState(requiredItems[0]);
                RefreshPortalState(requiredItems[1]);
            }
            // any other number means a single crystal was placed
            else
                RefreshPortalState(requiredItems[savedState - 1]); // subtract the 1 added in SaveState() method
        }
    }
}
