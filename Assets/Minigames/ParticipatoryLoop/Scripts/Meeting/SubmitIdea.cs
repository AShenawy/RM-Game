using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmitIdea : MonoBehaviour
{
    public MeetingBehaviour meetingBehaviour;
    public MeetingStages nextStage;

    public void UpdateNextStage(MeetingStages stage)
    {
        nextStage = stage;
    }

    public void SubmitAndAdvance()
    {
        meetingBehaviour.AdvanceMeeting(nextStage);
    }
}
