using UnityEngine;

public class IntroDialogue : MonoBehaviour
{
    public DialogueBehaviour dialogueBehaviour;
    [TextArea] public string[] firstTimeDialogue;
    [TextArea] public string[] repeatedDialogue;
    [TextArea] public string[] willAttendDialgue;

    private void OnEnable()
    {
        if (GameManager.instance.currentTurn < 1)
        {
            PlayFirstTimeDialogue();
        }
        else if (GameManager.instance.currentTurn < 3)
        {
            PlayRepeatedDialogue();
        }
        else
        {
            PlayWillAttendDialogue();
        }
    }

    public void PlayFirstTimeDialogue()
    {
        dialogueBehaviour.DisplayDialogue(firstTimeDialogue);
    }

    public void PlayRepeatedDialogue()
    {
        dialogueBehaviour.DisplayDialogue(repeatedDialogue);
    }

    public void PlayWillAttendDialogue()
    {
        dialogueBehaviour.DisplayDialogue(willAttendDialgue);
    }
}
