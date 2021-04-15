#define PLAYTESTING
using Methodyca.Core;

namespace Methodyca.Minigames.Methodologies
{
    public class MethodologyStory : InkCharStory
    {
        public event System.Action OnDiscussionWon;
        public Core.Minigames minigameID;
        public bool wonDiscussion = false;

        protected override void CheckVariables() { }

        protected override void EndStory()
        {
            if ((bool)inkStory.variablesState["wonDiscussion"])
            {
                wonDiscussion = true;
                OnDiscussionWon?.Invoke();
            }

            base.EndStory();
        }

#if PLAYTESTING
        // button click action
        public void ForceCompleteStory()
        {
            wonDiscussion = true;
            OnDiscussionWon?.Invoke();
            base.EndStory();
        }
#endif
    }
}