namespace Methodyca.Core
{
    public class MonsterStory : InkCharStory, ISaveable, ILoadable
    {
        public bool firstMeeting = true;
        public bool completedEnoughMinigames = false;
        public bool gotPunkBoard = false;
        public PunkStory punkStory;
        public bool monsterCompleted = false;
        public Door linkedDoorQN;
        public Door linkedDoorQL;
        public NPC monsterNPC;
        public SwitchImageDisplay gameBoard;


        private void OnEnable()
        {
            monsterNPC.onGivenAllItems += GiveBoard;
        }

        private void OnDisable()
        {
            monsterNPC.onGivenAllItems -= GiveBoard;
        }

        private void Awake()
        {
            // This is to make sure door is 'unlocked' when loading a won game.
            // Otherwise, door will actually be unlocked but still have the locked image set on it.
            if (SaveLoadManager.interactableStates.TryGetValue($"{name}_story_completed", out int completeState))
            {
                monsterCompleted = (completeState == 1) ? true : false;
                if (monsterCompleted)
                {
                    linkedDoorQN.Unlock();
                    linkedDoorQL.Unlock();
                }
            }

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
            inkStory.variablesState["firstMeeting"] = firstMeeting;
            inkStory.variablesState["gotpunkboard"] = gotPunkBoard;


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
            {
                firstMeeting = false;
                punkStory.SetMonsterMet();
            }

            // unlock door on first time completion only. Then door will save its state and remain unlocked
            if (System.Convert.ToBoolean(inkStory.variablesState["monsterCompleted"]) && !monsterCompleted)
            {
                linkedDoorQN.Unlock();
                linkedDoorQL.Unlock();
                monsterCompleted = true;
            }

            SaveState();
        }

        public void GiveBoard()
        {
            gotPunkBoard = true;
            gameBoard.SwitchImage();
            SaveState();
        }

        public void SaveState()
        {
            SaveLoadManager.SetInteractableState($"{name}_story_firstMeeting", firstMeeting ? 1 : 0);
            SaveLoadManager.SetInteractableState($"{name}_story_gotBoard", gotPunkBoard ? 1 : 0);
            SaveLoadManager.SetInteractableState($"{name}_story_completed", monsterCompleted ? 1 : 0);
        }

        public void LoadState()
        {
            if (SaveLoadManager.interactableStates.TryGetValue($"{name}_story_firstMeeting", out int meetingState))
                firstMeeting = (meetingState == 1) ? true : false;

            if (SaveLoadManager.interactableStates.TryGetValue($"{name}_story_gotBoard", out int boardState))
                gotPunkBoard = (boardState == 1) ? true : false;

            if (SaveLoadManager.interactableStates.TryGetValue($"{name}_story_completed", out int completeState))
                monsterCompleted = (completeState == 1) ? true : false;
        }
    }
}