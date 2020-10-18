using UnityEngine;
using Methodyca.Core;

// This script handles general use of objects
[RequireComponent(typeof(SwitchImageDisplay))]
public class Operate : ObjectInteraction
{
    [Header("Specific Operate Parameters")]
    [Tooltip("Can the player operate this object?")]
    public bool canOperate;
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
        GetComponent<SwitchImageDisplay>().SwitchImage();
        ToggleInteraction(false);
        DialogueHandler.instance.DisplayDialogue(onOperateSuccessText);
    }
}
   
