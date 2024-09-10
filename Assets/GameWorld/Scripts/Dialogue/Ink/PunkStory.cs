namespace Methodyca.Core
{
    public class PunkStory : InkCharStory, ISaveable, ILoadable
    {
        public bool firstMeeting = true;
        public bool metMonster = false;
        public bool gotPunkBoard = false;
        //public MonsterStory monsterStory; //-- TODO remove as redundant
        public Act2ProgressController progressController;
        public SwitchImageDisplay gameBoard;


        private void Awake()
        {
            // switching image of game board when loading game
            if (SaveLoadManager.interactableStates.TryGetValue($"{name}_story_gotBoard", out int boardState))
            {
                gotPunkBoard = (boardState == 1) ? true : false;
                if (gotPunkBoard)
                    gameBoard.SwitchImage();
            }
        }

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

            
            // Give item on first time completion only
            if (System.Convert.ToBoolean(inkStory.variablesState["gotpunkboard"]) && !gotPunkBoard)
            {
                gotPunkBoard = true;
                GiveBoard();
            }

            SaveState();
        }

        public void SetMonsterMet()
        {
            metMonster = true;
            SaveState();
        }

        void GiveBoard()
        {
            // minigame can be any of the N2 minigames, since the reward is a single item
            progressController.GiveMinigameReward(Minigames.Survey);
            gameBoard.SwitchImage();
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