using UnityEngine;
using Methodyca.Core;

// This script is for non-playable characters (NPCs) which the player will interact with
public class NPC : ObjectInteraction
{
    [Header("Specific NPC paramters")]
    [TextArea] public string[] textDialogues;

    public override void InteractWithObject()
    {
        base.InteractWithObject();
        Talk();
    }

    void Talk()
    {
        // feed text dialogues to dialogue output box
        DialogueHandler.instance.DisplayDialogue(textDialogues);
    }
}
   
