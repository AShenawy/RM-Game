using UnityEngine;
using Methodyca.Core;


public class PortalInteraction : ObjectInteraction
{
    [Header("Specific Script Parameters")]
    [Tooltip("Whether the player can use the portal or not")]
    public bool canUsePortal;
    [SerializeField, Multiline, Tooltip("In-game text to be displayed if Can Use Portal is set to false.")]
    private string responseForDisabled;

    private PortalController portalControl;


    protected override void Start()
    {
        base.Start();

        portalControl = GetComponent<PortalController>();
    }

    public override void InteractWithObject()
    {
        base.InteractWithObject();

        if(canUsePortal)
        {
            portalControl.GoToNextLevel();
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
        }
        else
            return;

        if (requiredItemsLeft < 1)
            canUsePortal = true;
    }
}
