using Methodyca.Core;
using UnityEngine;

// This script is for moving interactable objects
public class MoveObject : ObjectInteraction
{
    [Header("Specific Move Object Parameters")]
    public bool canMove;
    public Vector2 movementValue;

    public override void InteractWithObject()
    {
        base.InteractWithObject();

        if(canMove)
            Move();
    }

    void Move()
    {
        transform.Translate(movementValue.x, movementValue.y, 0);
        ToggleInteraction(false);
    }
}
