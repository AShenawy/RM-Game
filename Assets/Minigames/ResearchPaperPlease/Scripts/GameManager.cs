using System;
using System.Collections.Generic;
using UnityEngine;

namespace Methodyca.Minigames.ResearchPaperPlease
{
    public enum PaperQuality { Low, Medium, High }

    [Serializable]
    public class Feedback
    {
        public Sprite Character;
        [TextArea(1, 3)] public string Speech;
    }
    [Serializable]
    public class Option
    {
        public char Index;
        public string Header;
        [TextArea(1, 4)] public string Text;
    }
    [Serializable]
    public class ResearchPaperData
    {
        public PaperQuality Quality;
        public char WrongOption;
        public Feedback StudentReaction;
        public Option[] Options;
    }
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] private LevelData[] data;

        public static event Action<LevelData> OnLevelInitiated = delegate { };
        public static event Action<Feedback> OnLevelOver = delegate { };
        public static event Action<bool, bool> OnPaperDecided = delegate { };
        public static event Action<ResearchPaperData> OnPaperUpdated = delegate { };
        public static event Action<Sprite, string> OnFeedbackInitiated = delegate { };
        public static event Action OnFix = delegate { };
        public static event Action<int> OnProgressUpdated = delegate { };
        public static event Action<int> OnQualityUpdated = delegate { };

        public int TotalPaperCount { get; private set; }

        private ResearchPaperData _currentResearchPaperData;
        private Queue<Feedback> reactions = new Queue<Feedback>();
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
            }
            else if (_isFeedbackDisplayed) //Next level is initiated
            {
                _isFeedbackDisplayed = false;
                InitiateNextLevel();
            }
            else //Level is over (Display level-end feedback)
            {
                _isFeedbackDisplayed = true;
                OnLevelOver?.Invoke(GetCurrentLevelFeedback());
            }
        }


        public void HandleDecision(bool isAccepted)
        {
            if (isAccepted)
            {
                if (_currentResearchPaperData.Quality == PaperQuality.High)
                {
                    OnProgressUpdated?.Invoke(++_progressValue);
                    OnQualityUpdated?.Invoke(++_qualityValue);
                }
                else if (_currentResearchPaperData.Quality == PaperQuality.Medium)
                {
                    OnProgressUpdated?.Invoke(++_progressValue);
                    OnQualityUpdated?.Invoke(++_qualityValue);
                    OnFeedbackInitiated?.Invoke(GetAuditor(), "could have been better");
                }
                else
                {
                    OnProgressUpdated?.Invoke(++_progressValue);
                    OnQualityUpdated?.Invoke(--_qualityValue);
                    OnFeedbackInitiated?.Invoke(GetAuditor(), "there is a mistake ...");
                }

                OnPaperDecided(true, false);
            }
            else
            {
                if (_currentResearchPaperData.Quality == PaperQuality.High)
                {
                    OnQualityUpdated?.Invoke(--_qualityValue);
                    OnPaperDecided(false, false);
                    OnFeedbackInitiated?.Invoke(GetAuditor(),"You just rejected a perfectly fine research proposal. Try to be more careful next time.");
                }
                else if (_currentResearchPaperData.Quality == PaperQuality.Medium | _currentResearchPaperData.Quality == PaperQuality.Low)
                {
                    OnPaperDecided(false, true);
                }
            }
        }

        public void HandleFixingOption(char optionIndex)
        {
            if (_currentResearchPaperData.WrongOption == optionIndex)
            {
                if (_currentResearchPaperData.Quality == PaperQuality.Medium)
                {
                    OnQualityUpdated?.Invoke(++_qualityValue);
                    OnFeedbackInitiated?.Invoke(GetAuditor(),"auditor");
                }
                else
                {
                    OnQualityUpdated?.Invoke(++_qualityValue);
                    OnFeedbackInitiated?.Invoke(_currentResearchPaperData.StudentReaction.Character, _currentResearchPaperData.StudentReaction.Speech);
                }
            }
            else
            {
                if (_currentResearchPaperData.Quality == PaperQuality.Medium)
                {
                    OnFeedbackInitiated?.Invoke(GetAuditor(),"auditor");
                }
                else
                {
                    OnFeedbackInitiated?.Invoke(GetAuditor(),"auditor");
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

        private Sprite GetAuditor()
        {
            for (int i = 0; i < data.Length; i++)
            {
                if (_currentLevelIndex == data[i].Level)
                {
                    return data[i].AuditorCharacter;
                }
            }

            return null;
        }

        private Feedback GetCurrentLevelFeedback()
        {
            for (int i = 0; i < data.Length; i++)
            {
                if (_currentLevelIndex == data[i].Level)
                {
                    return data[i].LevelFeedback;
                }
            }

            return null;
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