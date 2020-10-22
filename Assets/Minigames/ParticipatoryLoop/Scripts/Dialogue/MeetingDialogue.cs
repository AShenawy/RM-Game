using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeetingDialogue : MonoBehaviour
{
    public DialogueBehaviour dialogueBehaviour;
    [TextArea] public string[] greetingDialogue;
    [TextArea] public string[] ideationDialogue;
    [TextArea] public string[] targetAudDialogue;
    [TextArea] public string[] storyDialogue;
    [TextArea] public string[] artDialogue;
    [TextArea] public string[] musicDialogue;
    [TextArea] public string[] conclusionNoClientDialogue;
    [TextArea] public string[] conclusionWithClientDialogue;

    public void DisplayGreeting()
    {
        dialogueBehaviour.DisplayDialogue(greetingDialogue);
    }

    public void DisplayIdeation()
    {
        dialogueBehaviour.DisplayDialogue(ideationDialogue);
    }

    public void DisplayTarget()
    {
        dialogueBehaviour.DisplayDialogue(targetAudDialogue);
    }

    public void DisplayStory()
    {
        dialogueBehaviour.DisplayDialogue(storyDialogue);
    }

    public void DisplayArt()
    {
        dialogueBehaviour.DisplayDialogue(artDialogue);
    }

    public void DisplaySound()
    {
        dialogueBehaviour.DisplayDialogue(musicDialogue);
    }

    public void DisplayConclusionNoClient()
    {
        dialogueBehaviour.DisplayDialogue(conclusionNoClientDialogue);
    }

    public void DisplayConclusionWithClient()
    {
        dialogueBehaviour.DisplayDialogue(conclusionWithClientDialogue);
    }
}
