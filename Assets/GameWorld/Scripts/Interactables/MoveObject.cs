using Methodyca.Core;
using UnityEngine;

// This script is for moving interactable objects
public class MoveObject : ObjectInteraction, ISaveable, ILoadable
{
    [Header("Specific Move Object Parameters")]
    public bool canMove;
    public Vector2 movementValue;
    public Sound SFX;
    private bool isMoved;


    protected override void Start()
    {
        LoadState();
        if (isMoved)
            Move();

        base.Start();
    }

    public override void InteractWithObject()
    {
        base.InteractWithObject();

        if(canMove)
            Move();
    }

    void Move()
    {
        transform.Translate(movementValue);
        SoundManager.instance.PlaySFX(SFX);
        ToggleInteraction(false);
        isMoved = true;
        SaveState();
    }

    public void LoadState()
    {
        if (SaveLoadManager.interactableStates.TryGetValue(name, out int moved))
            isMoved = (moved == 0) ? false : true;
    }

    public void SaveState()
    {
        SaveLoadManager.SetInteractableState(name, 1);
    }
}
