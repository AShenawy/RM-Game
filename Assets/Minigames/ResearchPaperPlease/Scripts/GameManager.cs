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
        [SerializeField] private ResearchPaper[] data;

        public static event Action<bool, ResearchPaperData> OnPaperDecided = delegate { };
        public static event Action<ResearchPaperData> OnPaperUpdated = delegate { };
        public static event Action<string> OnFix = delegate { };
        public static event Action<int> OnProgressUpdated = delegate { };
        public static event Action<int> OnQualityUpdated = delegate { };

        public int TotalPaperCount { get; private set; }

        private ResearchPaperData _currentResearchPaperData;
        private Queue<ResearchPaperData> _allResearchPaper = new Queue<ResearchPaperData>();
        private Dictionary<char, string> _researchPaperOptionPairs = new Dictionary<char, string>();
        private Dictionary<int, ResearchPaperData[]> _currentResearchPaperDataByLevel = new Dictionary<int, ResearchPaperData[]>();

        private int _currentLevelIndex = 0;
        private int _qualityValue = 0;
        private int _progressValue = 0;

        public void InitiateNextLevel()
        {
            _allResearchPaper = GetResearchDataByLevel(++_currentLevelIndex);

            if (_allResearchPaper == null)
            {
                //Game Over
            }
            else
            {
                HandleNextPaper();
            }
        }

        public void HandleNextPaper()
        {
            if (_allResearchPaper != null && _allResearchPaper.Count > 0)
            {
                _currentResearchPaperData = _allResearchPaper.Dequeue();
                OnPaperUpdated?.Invoke(_currentResearchPaperData);
            }
            else
            {
                InitiateNextLevel();
            }

            OnProgressUpdated?.Invoke(_progressValue);
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
            if (_currentResearchPaperData.IsCorrect)
            {
                //Students yells

            }
            else
            {
                //Feedback from Academic auditor - Grethe Whiny

            }

            _progressValue++;
            OnPaperDecided?.Invoke(false, _currentResearchPaperData);
        }

        public void HandleFixOptions(char optionIndex)
        {
            if (_currentResearchPaperData.WrongOption == optionIndex)
            {
                //Correct Answer
            }
            else
            {
                //Wrong Answer
            }

            OnFix?.Invoke(_currentResearchPaperData.Feedback);
        }

        private void Start()
        {
            _currentResearchPaperDataByLevel = GetResearchPaperDataByLevel();
            InitiateNextLevel();

            TotalPaperCount = GetTotalResearchPaperCount();

            _researchPaperOptionPairs = new Dictionary<char, string>()
            {
                { 'a', _currentResearchPaperData.Title },
                { 'b', _currentResearchPaperData.Author },
                { 'c', _currentResearchPaperData.ResearchGoal },
                { 'd', _currentResearchPaperData.ResearchMethodology },
                { 'e', _currentResearchPaperData.ResearchMethods },
                { 'f', _currentResearchPaperData.ResearchQuestions }
            };
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

        private Dictionary<int, ResearchPaperData[]> GetResearchPaperDataByLevel()
        {
            var keyValuePairs = new Dictionary<int, ResearchPaperData[]>();

            for (int i = 0; i < data.Length; i++)
            {
                keyValuePairs.Add(data[i].Level, data[i].Researches);
            }

            return keyValuePairs;
        }

        private Queue<ResearchPaperData> GetResearchDataByLevel(int level)
        {
            if (_currentResearchPaperDataByLevel.TryGetValue(level, out ResearchPaperData[] researchPaperData))
            {
                return new Queue<ResearchPaperData>(researchPaperData);
            }

            return null;
        }
    }
}