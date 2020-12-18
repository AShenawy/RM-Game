using UnityEngine;

namespace Methodyca.Minigames.PartLoop
{
    public class RecapBehaviour : MonoBehaviour
    {
        public DesignPlan meetingDesignPlan;
        public RecapDialogue recapDialogue;
        public ReviewStages reviewStage;
        public bool clientLikesIdeation;
        public bool clientLikesTargetAud;
        public bool clientLikesStory;
        public bool clientLikesArt;
        public bool clientLikesSound;

        private int totalLikes;

        private void OnEnable()
        {
            reviewStage = ReviewStages.Intro;

            PopulateDialogue();
            totalLikes = 0;
            UpdateClientLikes();

            AdvanceReview(reviewStage);
        }

        void PopulateDialogue()
        {
            recapDialogue.FillIdeas(meetingDesignPlan);
        }

        void UpdateClientLikes()
        {
            clientLikesIdeation = meetingDesignPlan.ideationIdea.appealsToClient;
            if (clientLikesIdeation)
                totalLikes++;

            clientLikesTargetAud = meetingDesignPlan.targetAudienceIdea.appealsToClient;
            if (clientLikesTargetAud)
                totalLikes++;

            clientLikesStory = meetingDesignPlan.storyIdea.appealsToClient;
            if (clientLikesStory)
                totalLikes++;

            clientLikesArt = meetingDesignPlan.artIdea.appealsToClient;
            if (clientLikesArt)
                totalLikes++;

            clientLikesSound = meetingDesignPlan.soundIdea.appealsToClient;
            if (clientLikesSound)
                totalLikes++;
        }

        public void OnRecapEnded()
        {
            if (reviewStage == ReviewStages.Outro && !ClientSatisfied())
                GameManager.instance.GoToIntro();
            else if (reviewStage == ReviewStages.Outro && ClientSatisfied())
                GameManager.instance.GoToEnd();
        }

        public void AdvanceReview(ReviewStages stage)
        {
            switch (stage)
            {
                case ReviewStages.Intro:
                    reviewStage = ReviewStages.Intro;
                    if (GameManager.instance.currentTurn < 3)
                        recapDialogue.DisplayNoClientIntro();
                    else
                        recapDialogue.DisplayWithClientIntro();
                    break;

                case ReviewStages.Ideation:
                    reviewStage = ReviewStages.Ideation;
                    recapDialogue.DisplayIdeationReview();
                    break;

                case ReviewStages.TargetAudience:
                    reviewStage = ReviewStages.TargetAudience;
                    recapDialogue.DisplayTargetAudReview();
                    break;

                case ReviewStages.Story:
                    reviewStage = ReviewStages.Story;
                    recapDialogue.DisplayStoryReview();
                    break;

                case ReviewStages.Art:
                    reviewStage = ReviewStages.Art;
                    recapDialogue.DisplayArtReview();
                    break;

                case ReviewStages.Sound:
                    reviewStage = ReviewStages.Sound;
                    recapDialogue.DisplaySoundReview();
                    break;

                case ReviewStages.Outro:
                    reviewStage = ReviewStages.Outro;
                    if (GameManager.instance.currentTurn < 3)
                        recapDialogue.DisplayNoClientOutro();
                    else if (ClientSatisfied())
                        recapDialogue.DisplayClientHappyOutro();
                    else
                        recapDialogue.DisplayClientAngryOutro();
                    break;

                default:
                    Debug.LogWarning("No review stage provided");
                    break;
            }
        }

        bool ClientSatisfied()
        {
            return totalLikes > 4;
        }
    }

    public enum ReviewStages { Intro, Ideation, TargetAudience, Story, Art, Sound, Outro }
}