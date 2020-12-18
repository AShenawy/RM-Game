using UnityEngine;

namespace Methodyca.Minigames.PartLoop
{
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
}