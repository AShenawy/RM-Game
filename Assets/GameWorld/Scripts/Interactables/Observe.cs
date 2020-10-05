using UnityEngine;
using Methodyca.Core;

// This script for observing objects in the game world without any special interaction with them
public class Observe : ObjectInteraction
{
    [Header("Specific Observe Paramters")]
    [TextArea, Tooltip("Dialogue to display when interacting with object")]
    public string[] inspectDialogue;
    public Sound OberservSFX;
    

    public override void InteractWithObject()
    {
        DialogueHandler.instance.DisplayDialogue(inspectDialogue);
        SoundManager.instance.PlaySFX(OberservSFX);
    }
}
