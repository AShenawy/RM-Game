using Methodyca.Core;

namespace Methodyca.Minigames.Methodologies
{
    public class MethodologyStory : InkCharStory
    {
        public static event System.Action OnDiscussionWon;

        protected override void CheckVariables() { }

        protected override void EndStory()
        {
            base.EndStory();
            if ((bool)inkStory.variablesState["wonDiscussion"])
                OnDiscussionWon?.Invoke();
        }
    }
}