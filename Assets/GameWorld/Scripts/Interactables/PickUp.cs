using UnityEngine;
using Methodyca.Core;

// This script if for picking up interactable objects
public class PickUp : ObjectInteraction
{
    [Header("Specific Pick Up Parameters")]
    [Tooltip("Reference to the item scriptable object which will be picked up")]
    public Item item;
    public Sound SFX;

    public override void InteractWithObject()
    {
        base.InteractWithObject();
        PickUpObject();
        
    }

    public override void PickUpObject()
    {
        base.PickUpObject();

        if (canPickUp)
        {
            InventoryManager.instance.Add(item);
            SoundManager.instance.PlaySFX(SFX);
            Destroy(gameObject);    // destroy object in game world after it's moved to inventory
        }
    }
}
   
