using System;
using System.Collections.Generic;
using UnityEngine;

namespace Methodyca.Minigames.ResearchPaperPlease
{
    [Serializable]
    public class ResearchPaperData
    {
        public bool IsCorrect;
        public char WrongOption;
        public string Field;
        public string Title;
        public string Author;
        public string Supervisor;
        public string ResearchGoal;
        public string ResearchMethodology;
        public string ResearchMethods;
        [TextArea(1, 4)] public string ResearchQuestions;
        [TextArea(1, 3)] public string Feedback;
    }
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] private LevelData[] data;

        public static event Action<bool, ResearchPaperData> OnPaperDecided = delegate { };
        public static event Action<ResearchPaperData> OnPaperUpdated = delegate { };
        public static event Action<LevelData> OnLevelInitiated = delegate { };
        public static event Action<string> OnLevelOver = delegate { };
        public static event Action<string> OnFix = delegate { };
        public static event Action<int> OnProgressUpdated = delegate { };
        public static event Action<int> OnQualityUpdated = delegate { };

        public int TotalPaperCount { get; private set; }

        private ResearchPaperData _currentResearchPaperData;
        private Queue<ResearchPaperData> _allResearchPaper = new Queue<ResearchPaperData>();
        private Dictionary<int, ResearchPaperData[]> _currentResearchPaperDataByLevel = new Dictionary<int, ResearchPaperData[]>();

        private bool _isFeedbackDisplayed;
        private int _qualityValue = 0;
        private int _progressValue = 0;
        private int _currentLevelIndex = 0;

        public void InitiateNextLevel()
        {
            _allResearchPaper = GetResearchPaperByLevel(++_currentLevelIndex);

            if (_allResearchPaper == null) //All of research paper are completed (Game Over)
            {

            }
            else //Display next paper
            {
                OnLevelInitiated?.Invoke(GetCurrentLevelData());
                HandleNextPaper();
            }
        }

        public void HandleNextPaper()
        {
            if (_allResearchPaper != null && _allResearchPaper.Count > 0) //Level is progressing
            {
                _currentResearchPaperData = _allResearchPaper.Dequeue();
                OnPaperUpdated?.Invoke(_currentResearchPaperData);
                OnProgressUpdated?.Invoke(_progressValue);
            }
            else if (_isFeedbackDisplayed) //Next level is initiated
            {
                _isFeedbackDisplayed = false;
                InitiateNextLevel();
            }
            else //Level is over (Display level-end feedback)
            {
                _isFeedbackDisplayed = true;
                OnProgressUpdated?.Invoke(_progressValue);
                OnLevelOver?.Invoke(GetCurrentLevelFeedback());
            }
        }

        public void HandleAcceptedPaper()
        {
            if (_currentResearchPaperData.IsCorrect)
            {
                OnQualityUpdated?.Invoke(++_qualityValue);
            }
            else
            {
                OnQualityUpdated?.Invoke(--_qualityValue);
            }

            _progressValue++;
            OnPaperDecided?.Invoke(true, _currentResearchPaperData);
        }

        public void HandleRejectedPaper()
        {
            _progressValue++;
            OnPaperDecided?.Invoke(false, _currentResearchPaperData);
        }

        public void HandleFixingOption(char optionIndex)
        {
            if (_currentResearchPaperData.IsCorrect)
            {
                //Students yell
                OnFix?.Invoke("Student yell");
            }
            else
            {
                if (_currentResearchPaperData.WrongOption == optionIndex)
                {
                    //Correct Answer
                    OnFix?.Invoke("Do nothing");
                }
                else
                {
                    //Wrong Answer - Auditor speaks
                    OnFix?.Invoke("Auditor speaks");
                }
            }
        }

        private void Start()
        {
            _currentResearchPaperDataByLevel = GetResearchPaperDataByLevel();
            TotalPaperCount = GetTotalResearchPaperCount();
            InitiateNextLevel();
        }

        private int GetTotalResearchPaperCount()
        {
            int count = 0;

            foreach (var item in _currentResearchPaperDataByLevel)
            {
                count += item.Value.Length;
            }

            return count;
        }

        private string GetCurrentLevelFeedback()
        {
            for (int i = 0; i < data.Length; i++)
            {
                if (_currentLevelIndex == data[i].Level)
                {
                    return data[i].LevelFeedback;
                }
            }

            return "";
        }

        private LevelData GetCurrentLevelData()
        {
            for (int i = 0; i < data.Length; i++)
            {
                if (_currentLevelIndex == data[i].Level)
                {
                    return data[i];
                }
            }

            return null;
        }

        private Dictionary<int, ResearchPaperData[]> GetResearchPaperDataByLevel()
        {
            var keyValuePairs = new Dictionary<int, ResearchPaperData[]>();

            for (int i = 0; i < data.Length; i++)
            {
                keyValuePairs.Add(data[i].Level, data[i].Researches);
            }

            return keyValuePairs;
        }

        private Queue<ResearchPaperData> GetResearchPaperByLevel(int level)
        {
            if (_currentResearchPaperDataByLevel.TryGetValue(level, out ResearchPaperData[] researchPaperData))
            {
                return new Queue<ResearchPaperData>(researchPaperData);
            }

            return null;
        }
    }
}