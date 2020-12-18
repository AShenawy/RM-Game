using UnityEngine;

namespace Methodyca.Minigames.PartLoop
{
    public class MeetingBehaviour : MonoBehaviour
    {
        public Activities activities;
        public int currentActivity;
        public MeetingStages meetingStage;
        public MeetingDialogue meetingDialogue;
        public DialogueBehaviour dialogueBehaviour;

        public delegate void StageChanged(MeetingStages newStage);
        public event StageChanged onStageChanged;

        private void OnEnable()
        {
            currentActivity = 0;
            meetingStage = MeetingStages.Greeting;
            AdvanceMeeting(meetingStage);
        }

        public void onDialogueEnded()
        {
            if (meetingStage == MeetingStages.Greeting)
            {
                meetingStage = MeetingStages.Ideation;
                AdvanceMeeting(meetingStage);
            }
            else if (meetingStage == MeetingStages.Conclusion)
            {
                GameManager.instance.GoToRecap();
            }
        }

        public void AdvanceMeeting(MeetingStages stage)
        {
            switch (stage)
            {
                case MeetingStages.Greeting:
                    meetingStage = MeetingStages.Greeting;
                    meetingDialogue.DisplayGreeting();
                    break;

                case MeetingStages.Ideation:
                    meetingStage = MeetingStages.Ideation;
                    currentActivity = 0;
                    meetingDialogue.DisplayIdeation();
                    break;

                case MeetingStages.TargetAudience:
                    meetingStage = MeetingStages.TargetAudience;
                    currentActivity = 1;
                    meetingDialogue.DisplayTarget();
                    break;

                case MeetingStages.Story:
                    meetingStage = MeetingStages.Story;
                    currentActivity = 2;
                    meetingDialogue.DisplayStory();
                    break;

                case MeetingStages.Art:
                    meetingStage = MeetingStages.Art;
                    currentActivity = 3;
                    meetingDialogue.DisplayArt();
                    break;

                case MeetingStages.Music:
                    meetingStage = MeetingStages.Music;
                    currentActivity = 4;
                    meetingDialogue.DisplaySound();
                    break;

                case MeetingStages.Conclusion:
                    meetingStage = MeetingStages.Conclusion;
                    if (GameManager.instance.currentTurn < 3)
                    {
                        meetingDialogue.DisplayConclusionNoClient();
                    }
                    else
                    {
                        meetingDialogue.DisplayConclusionWithClient();
                    }
                    break;
            }

            onStageChanged?.Invoke(stage);
        }
    }

    public enum MeetingStages { Greeting, Ideation, TargetAudience, Story, Art, Music, Conclusion }
}