using UnityEngine;
using Methodyca.Minigames.Methodologies;
using System.Collections;

namespace Methodyca.Core
{
    public class StoryMinigameConnection : MinigameHub, ISaveable, ILoadable
    {
        [Header("Specific Script Parameters")]
        public Act2ProgressController progressController;
        public Minigames minigameID;
        public bool isRewardGiven;      // TODO make it private after debugging
        [Tooltip("If the minigame is accessible from both QL/QN nodes, then place the object from opposite node")]
        public StoryMinigameConnection linkedMinigameAccess;

        private  MethodologyStory story;


        private void OnEnable()
        {
            SceneManagerScript.onAdditiveSceneLoaded += SubscribeToStoryStatus;
        }

        private void OnDisable()
        {
            SceneManagerScript.onAdditiveSceneLoaded -= SubscribeToStoryStatus;
        }

        void SubscribeToStoryStatus()
        {
            // Only subscribe to matching minigame story to avoid subbing to other stories in same node
            story = GameObject.FindWithTag("MG Method Story").GetComponent<MethodologyStory>();
            if (story.minigameID == minigameID)
                story.OnDiscussionWon += EndGame;
            else
            {
                story.OnDiscussionWon -= EndGame;
                story = null;
            }
        }

        public override void Start()
        {
            base.Start();
            LoadState();

            if (isCompleted)
            {
                EndGame();

                // if there are 2 of the minigame accesses in QN/QL, then mirror access
                // should be ended to avoid giving 2 rewards per minigame.
                linkedMinigameAccess?.EndLinkedAccess();
            }
        }

        public override void EndGame()
        {
            base.EndGame();

            if (!isRewardGiven)
            {
                progressController.GiveMinigameReward(minigameID);
                isRewardGiven = true;
            }

            if (story)
            {
                story.OnDiscussionWon -= EndGame;
                story = null;
            }
            
            SaveState();
        }

        public void EndLinkedAccess()
        {
            isCompleted = true;
            isRewardGiven = true;
        }

        public void SaveState()
        {
            SaveLoadManager.SetInteractableState(name + "_completed", isCompleted ? 1 : 0);
            SaveLoadManager.SetInteractableState(name + "_rewardGiven", isRewardGiven ? 1 : 0);
        }

        public void LoadState()
        {
            if (SaveLoadManager.interactableStates.TryGetValue(name + "_completed", out int completedState))
                isCompleted = (completedState == 1) ? true : false;
            if (SaveLoadManager.interactableStates.TryGetValue(name + "_rewardGiven", out int rewardState))
                isRewardGiven = (rewardState == 1) ? true : false;
        }
    }
}