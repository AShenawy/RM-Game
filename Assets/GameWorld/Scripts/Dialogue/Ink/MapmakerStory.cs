using System.Collections;
using UnityEngine;

namespace Methodyca.Core
{
    public class MapmakerStory : InkCharStory, ISaveable, ILoadable
    {
        public bool firstMeeting = true;
        public bool usedCrystal;
        public bool completedOneMinigame;
        public bool completedEnoughMinigames;


        protected override void CheckVariables()
        {
            LoadState();
            inkStory.variablesState["firstMeeting"] = firstMeeting;
            
            // check if player entered N1-QL using crystal (switching from QN) or not (started Act 2 in QL)
            usedCrystal = GameManager.instance.usedDimeSwitch;
            inkStory.variablesState["usedCrystal"] = usedCrystal;

            int minigamesCount = BadgeManager.instance.minigamesComplete.Count;
            // check if player got at least 1 coin or more from minigames in N1
            if (minigamesCount > 2)
                completedEnoughMinigames = true;
            else if (minigamesCount == 2)   // only 1 minigame completed besides sorting
                completedOneMinigame = true;

            inkStory.variablesState["completedOneMinigame"] = completedOneMinigame;
            inkStory.variablesState["gotStudentCoin"] = completedEnoughMinigames;
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