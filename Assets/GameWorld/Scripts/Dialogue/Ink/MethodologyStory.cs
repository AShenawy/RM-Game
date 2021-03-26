using System.Collections;
using UnityEngine;

namespace Methodyca.Core
{
    public class MethodologyStory : InkCharStory, ISaveable, ILoadable
    {
        public bool isWon = false;
        public System.Action OnDiscussionWon;

        protected override void CheckVariables()
        {
            LoadState();
            if (isWon)
            {
                inkStory.variablesState["wonDiscussion"] = true;
                print("This methodology dialogue is already completed. Skipping.");
                EndStory();
                return;
            }
        }

        protected override void EndStory()
        {
            base.EndStory();

            if ((bool)inkStory.variablesState["wonDiscussion"])
            {
                isWon = true;
                OnDiscussionWon?.Invoke();
                SaveState();
            }
        }

        public void SaveState()
        {
            SaveLoadManager.SetInteractableState($"{name}_discussionWon", isWon == true ? 1 : 0);
        }

        public void LoadState()
        {
            if (SaveLoadManager.interactableStates.TryGetValue($"{name}_discussionWon", out int discussionState))
                isWon = (discussionState == 1) ? true : false;
        }
    }
}