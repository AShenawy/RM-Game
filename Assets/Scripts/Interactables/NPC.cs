using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : ObjectInteraction
{
    [TextArea] public string textDialogue;

    public override void InteractWithObject()
    {
        base.InteractWithObject();
        Talk();
    }

    void Talk()
    {
        print(textDialogue);
    }
}
   
