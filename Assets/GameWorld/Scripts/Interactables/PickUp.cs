using UnityEngine;
using Methodyca.Core;

// This script if for picking up interactable objects
public class PickUp : ObjectInteraction, ISaveable, ILoadable
{
    [Header("Specific Pick Up Parameters")]
    [Tooltip("Reference to the item scriptable object which will be picked up")]
    public Item item;
    public Sound SFX;
    public event System.Action<Item> onPickUp;
    private bool isPicked;

    protected override void Start()
    {
        LoadState();
        if (isPicked)
            Destroy(gameObject);

        base.Start(); 
    }

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
            isPicked = true;
            onPickUp?.Invoke(item);
            SaveState();
            Destroy(gameObject);    // destroy object in game world after it's moved to inventory
        }
    }

    public void SaveState()
    {
        SaveLoadManager.SetInteractableState(name, 1);
    }

    public void LoadState()
    {
        // check if item already picked in save file
        if (SaveLoadManager.interactableStates.TryGetValue(name, out int interactionState))
            isPicked = (interactionState == 0) ? false : true;
    }
}
   
