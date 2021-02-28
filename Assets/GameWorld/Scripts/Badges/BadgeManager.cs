#define TESTING
using UnityEngine;
using System.Collections.Generic;

namespace Methodyca.Core
{
    // This class handles badge progression and locked/unlocked badges in player phone
    public class BadgeManager : MonoBehaviour, ISaveable, ILoadable
    {
        #region Singleton
        public static BadgeManager instance;
        private void Awake()
        {
            if (instance == null)
                instance = this;
        }
        #endregion

        public event System.Action minigamesChanged;
        public List<int> minigamesComplete;


        private void Start()
        {
            LoadState();
        }

        private void Update()
        {
#if TESTING
            if (Input.GetKeyDown(KeyCode.Alpha1))
                SetMinigameComplete((int)Minigames.Sorting);
            if (Input.GetKeyDown(KeyCode.Alpha2))
                SetMinigameComplete((int)Minigames.DocStudy);
            if (Input.GetKeyDown(KeyCode.Alpha3))
                SetMinigameComplete((int)Minigames.Participatory);
            if (Input.GetKeyDown(KeyCode.Alpha4))
                SetMinigameComplete((int)Minigames.Prototyping);
            if (Input.GetKeyDown(KeyCode.Alpha5))
                SetMinigameComplete((int)Minigames.Questionnaire);
            if (Input.GetKeyDown(KeyCode.Alpha6))
                SetMinigameComplete((int)Minigames.Research);
            if (Input.GetKeyDown(KeyCode.Alpha7))
                SetMinigameComplete((int)Minigames.Interview);
            if (Input.GetKeyDown(KeyCode.Alpha8))
                SetMinigameComplete((int)Minigames.Observation);
#endif
        }

        public void SetMinigameComplete(int minigameId)
        {
            // only add the minigame to the list if it doesn't contain it
            if (minigamesComplete.Contains(minigameId))
                return;

            minigamesComplete.Add(minigameId);
            minigamesChanged?.Invoke();
            SaveState();
        }

        public void SaveState()
        {
            SaveLoadManager.SetCompletedMinigamesList(minigamesComplete);
        }

        public void LoadState()
        {
            minigamesComplete = new List<int>(SaveLoadManager.completedMinigamesIDs);
            CheckSceneManager();
            minigamesChanged?.Invoke();
            SaveState();    // Update SaveLoadManager with completed minigames from SceneManagerScript

            void CheckSceneManager()
            {
                List<int> tempList = new List<int>(SceneManagerScript.instance.minigamesWon);
                foreach (int minigame in tempList)
                {
                    // ensure minigamesComplete has no duplicate values
                    if (!minigamesComplete.Contains(minigame))
                        minigamesComplete.Add(minigame);
                }
            }
        }
    }

    // a list of all available minigames
    public enum Minigames { Blank, Sorting, DocStudy, Participatory, Prototyping, Questionnaire, Research, Interview, Observation }
}