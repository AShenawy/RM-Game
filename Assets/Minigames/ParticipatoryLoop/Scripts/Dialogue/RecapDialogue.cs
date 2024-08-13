using UnityEngine;

namespace Methodyca.Minigames.PartLoop
{
    public class RecapDialogue : MonoBehaviour
    {
        public DialogueBehaviour dialogueBehaviour;
        [Header("Client Uninvolved Discussion Pieces")]
        [TextArea] public string[] introNoClient;
        [TextArea] public string[] outroNoClient;

        [Header("Client Involved Discussion Pieces")]
        [TextArea] public string[] introWithClient;
        [TextArea] public string[] outroAngryClient;
        [TextArea] public string[] outroHappyClient;

        [Header("Ideas Presentation")]
        [TextArea] public string targetAudDebrief;
        [TextArea] public string storyDebrief;
        [TextArea] public string artDebrief;
        [TextArea] public string soundDebrief;
        public Idea ideation;
        public Idea targetAudience;
        public Idea story;
        public Idea art;
        public Idea sound;

        private void OnDisable()
        {
            ClearIdeas();
        }

        void ClearIdeas()
        {
            ideation.Clear();
            targetAudience.Clear();
            story.Clear();
            art.Clear();
            sound.Clear();
        }

        public void FillIdeas(DesignPlan plan)
        {
            ideation = plan.ideationIdea;
            targetAudience = plan.targetAudienceIdea;
            story = plan.storyIdea;
            art = plan.artIdea;
            sound = plan.soundIdea;
        }

        public void DisplayNoClientIntro() { dialogueBehaviour.DisplayDialogue(introNoClient); }

        public void DisplayWithClientIntro() { dialogueBehaviour.DisplayDialogue(introWithClient); }

        public void DisplayNoClientOutro() { dialogueBehaviour.DisplayDialogue(outroNoClient); }

        public void DisplayClientHappyOutro() { dialogueBehaviour.DisplayDialogue(outroHappyClient); }

        public void DisplayClientAngryOutro() { dialogueBehaviour.DisplayDialogue(outroAngryClient); }

        public void DisplayIdeationReview()
        {
            string[] ideationReview = new string[] { ideation.idea, ideation.clientResponse };
            dialogueBehaviour.DisplayDialogue(ideationReview);
        }

        public void DisplayTargetAudReview()
        {
            string[] taudReview = new string[] { targetAudDebrief, targetAudience.idea, targetAudience.clientResponse };
            dialogueBehaviour.DisplayDialogue(taudReview);
        }

        public void DisplayStoryReview()
        {
            string[] storyReview = new string[] { storyDebrief, story.idea, story.clientResponse };
            dialogueBehaviour.DisplayDialogue(storyReview);
        }

        public void DisplayArtReview()
        {
            string[] artReview = new string[] { artDebrief, art.idea, art.clientResponse };
            dialogueBehaviour.DisplayDialogue(artReview);
        }

        public void DisplaySoundReview()
        {
            string[] soundReview = new string[] { soundDebrief, sound.idea, sound.clientResponse };
            dialogueBehaviour.DisplayDialogue(soundReview);
        }
    }
}