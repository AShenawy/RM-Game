namespace Methodyca.Core
{
    public class MonsterStory : InkCharStory, ISaveable, ILoadable
    {
        public bool firstMeeting = true;
        public bool completedEnoughMinigames = false;


        protected override void CheckVariables()
        {
            LoadState();
            inkStory.variablesState["firstMeeting"] = firstMeeting;

            if (getCompletedMinigames() >= 2)
                completedEnoughMinigames = true;

            inkStory.variablesState["completedMinigames"] = completedEnoughMinigames;

            int getCompletedMinigames()
            {
                int completed = 0;
                foreach (int game in BadgeManager.instance.minigamesComplete)
                    if (game == 10 || game == 11 || game == 12)
                        completed++;

                return completed;
            }
        }

        protected override void EndStory()
        {
            base.EndStory();

            if (firstMeeting)
                firstMeeting = false;

            SaveState();
        }

        public void SaveState()
        {
            SaveLoadManager.SetInteractableState($"{name}_story_firstMeeting", firstMeeting ? 1 : 0);
        }

        public void LoadState()
        {
            if (SaveLoadManager.interactableStates.TryGetValue($"{name}_story_firstMeeting", out int meetingState))
                firstMeeting = (meetingState == 1) ? true : false;
        }
    }
}