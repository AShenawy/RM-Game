using UnityEngine;
using Methodyca.Core;

// This script handles general use of objects
public class Operate : ObjectInteraction
{
    [Header("Specific Operate Parameters")]
    [Tooltip("Can the player operate this object?")]
    public bool canOperate;
    public SpriteRenderer imageGameObject;
    public Sprite imageBefore, imageAfter;
    [Tooltip("Dialogue to display on successful operate")]
    public string onOperateSuccessText;
    [Tooltip("Dialogue to display on failed operate")]
    public string onOperateFailText;

    public override void InteractWithObject()
    {
        if (canOperate)
            Use();
        else
            DialogueHandler.instance.DisplayDialogue(onOperateFailText);
    }

    void Use()
    {
        canInteract = false;
        DisableInteractionCollider();
        imageGameObject.sprite = imageAfter;
        DialogueHandler.instance.DisplayDialogue(onOperateSuccessText);
    }
}
   
