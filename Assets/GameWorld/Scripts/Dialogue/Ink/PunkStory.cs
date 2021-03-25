namespace Methodyca.Core
{
    public class PunkStory : InkCharStory, ISaveable, ILoadable
    {
        public bool firstMeeting = true;
        public bool metMonster = false;
        public bool gotPunkBoard = false;
        public MonsterStory monsterStory;


        protected override void CheckVariables()
        {
            LoadState();
            inkStory.variablesState["PunkEncountered"] = (firstMeeting == true) ? 1 : 0;
            inkStory.variablesState["monsterOfferedHelp"] = metMonster;
            inkStory.variablesState["gotpunkboard"] = (gotPunkBoard == true) ? 1 : 0;

            inkStory.variablesState["N2QlProgress"] = getCompletedMinigames();

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

            gotPunkBoard = System.Convert.ToBoolean(inkStory.variablesState["gotpunkboard"]);
            if (gotPunkBoard)
                monsterStory.GiveBoard();

            SaveState();
        }

        public void SetMonsterMet()
        {
            metMonster = true;
            SaveState();
        }

        public void SaveState()
        {
            SaveLoadManager.SetInteractableState($"{name}_story_firstMeeting", firstMeeting ? 1 : 0);
            SaveLoadManager.SetInteractableState($"{name}_story_metMonster", metMonster ? 1 : 0);
            SaveLoadManager.SetInteractableState($"{name}_story_gotBoard", gotPunkBoard ? 1 : 0);
        }

        public void LoadState()
        {
            if (SaveLoadManager.interactableStates.TryGetValue($"{name}_story_firstMeeting", out int meetingState))
                firstMeeting = (meetingState == 1) ? true : false;

            if (SaveLoadManager.interactableStates.TryGetValue($"{name}_story_metMonster", out int monsterState))
                metMonster = (monsterState == 1) ? true : false;

            if (SaveLoadManager.interactableStates.TryGetValue($"{name}_story_gotBoard", out int boardState))
                gotPunkBoard = (boardState == 1) ? true : false;
        }
    }
}