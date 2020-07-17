using Methodyca.Core;
using UnityEngine;

public class MoveObject : ObjectInteraction
{
    public Vector2 movementValue;

    public override void InteractWithObject()
    {
        base.InteractWithObject();

        if(canInteract)
            Move();
    }

    void Move()
    {
        transform.Translate(movementValue.x, movementValue.y, 0);
        canInteract = false;
    }
}
