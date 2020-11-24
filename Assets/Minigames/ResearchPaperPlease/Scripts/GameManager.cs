﻿using System;
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
        public Feedback StudentReaction;
        public Feedback AuditorReaction;
        public char[] FixRequiredOptions;
        public Option[] Options;
    }
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] private int progressValueToWin;
        [SerializeField] private int qualityValueToWin;
        [SerializeField] private Feedback winFeedback;
        [SerializeField] private Feedback loseFeedback;
        [SerializeField] private LevelData[] data;

        public static event Action<bool> OnPaperDecided = delegate { };
        public static event Action<bool> OnFix = delegate { };
        public static event Action<int> OnProgressUpdated = delegate { };
        public static event Action<int> OnQualityUpdated = delegate { };
        public static event Action<string> OnLevelOver = delegate { };
        public static event Action<string, string> OnRulesUpdated = delegate { };
        public static event Action<bool, Feedback> OnGameOver = delegate { };
        public static event Action<Feedback> OnFeedbackInitiated = delegate { };
        public static event Action<LevelData> OnLevelInitiated = delegate { };
        public static event Action<ResearchPaperData> OnPaperUpdated = delegate { };
        public static event Action<Dictionary<char, bool>> OnOptionHighlighted = delegate { };

        public int TotalPaperCount { get; private set; }

        private LevelData _currentLevelData;
        private ResearchPaperData _currentResearchPaperData;
        private Queue<ResearchPaperData> _allResearchPaper = new Queue<ResearchPaperData>();
        private Dictionary<char, bool> _fixButtonPairs;
        private Dictionary<int, ResearchPaperData[]> _currentResearchPaperDataByLevel = new Dictionary<int, ResearchPaperData[]>();
        private List<ResearchPaperData> _acceptedPaperData = new List<ResearchPaperData>();
        private List<string> _rules;

        private int _rulePageIndex = -2;
        private int _qualityValue = 0;
        private int _progressValue = 0;
        private int _currentLevelIndex = 0;
        private bool _isFeedbackDisplayed;

        private const int _maxProgressionValueToWin = 20;

        public void InitiateNextLevel()
        {
            _allResearchPaper = GetResearchPaperByLevel(++_currentLevelIndex);

            if (_allResearchPaper == null) //All of research paper are completed (Game Over)
            {
                if (_progressValue > progressValueToWin && _progressValue <= _maxProgressionValueToWin && _qualityValue > qualityValueToWin)
                {
                    OnGameOver?.Invoke(true, winFeedback);
                }
                else if (_progressValue > progressValueToWin && _progressValue <= _maxProgressionValueToWin && _qualityValue <= qualityValueToWin)
                {
                    loseFeedback.Speech += " Too many papers were rejected for wrong reasons. I suggest you try again and be more careful. I suggest you try again and be more careful.";
                    OnGameOver?.Invoke(false, loseFeedback);
                }
                else if (_progressValue > _maxProgressionValueToWin)
                {
                    loseFeedback.Speech += " Too many low-quality research plans got accepted. I suggest you try again and be more careful.";
                    OnGameOver?.Invoke(false, loseFeedback);
                }
            }
            else //Display next paper
            {
                _currentLevelData = GetCurrentLevelData();
                _rules = new List<string>(_currentLevelData.LevelRules);
                NextRule();
                PreviousRule();

                InitiateFixButtons();
                OnLevelInitiated?.Invoke(_currentLevelData);
                HandleNextPaper();
            }
        }

        public void HandleNextPaper()
        {
            OnFeedbackInitiated?.Invoke(null);
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
                OnLevelOver?.Invoke($"<b>Level {_currentLevelIndex}</b> is completed.");

                if (_currentLevelData.AcceptedPaperTreshold > _acceptedPaperData.Count)
                {
                    OnFeedbackInitiated?.Invoke(_currentLevelData.PositiveLevelFeedback);
                }
                else
                {
                    OnFeedbackInitiated?.Invoke(_currentLevelData.NegativeLevelFeedback);
                }
            }
        }

        public void HandleDecision(bool isAccepted)
        {
            if (isAccepted)
            {
                _acceptedPaperData.Add(_currentResearchPaperData);

                if (_currentResearchPaperData.Quality == PaperQuality.High)
                {
                    OnProgressUpdated?.Invoke(++_progressValue);
                    OnQualityUpdated?.Invoke(++_qualityValue);
                }
                else if (_currentResearchPaperData.Quality == PaperQuality.Medium)
                {
                    OnProgressUpdated?.Invoke(++_progressValue);
                    OnQualityUpdated?.Invoke(++_qualityValue);
                    OnFeedbackInitiated?.Invoke(_currentResearchPaperData.AuditorReaction);
                }
                else
                {
                    OnProgressUpdated?.Invoke(++_progressValue);
                    OnQualityUpdated?.Invoke(--_qualityValue);
                    OnFeedbackInitiated?.Invoke(_currentResearchPaperData.AuditorReaction);
                }

                OnPaperDecided(true);
            }
            else
            {
                if (_currentResearchPaperData.Quality == PaperQuality.High)
                {
                    OnQualityUpdated?.Invoke(--_qualityValue);
                    OnPaperDecided(false);
                    OnFeedbackInitiated?.Invoke(_currentResearchPaperData.AuditorReaction);
                }
                else if (_currentResearchPaperData.Quality == PaperQuality.Medium | _currentResearchPaperData.Quality == PaperQuality.Low)
                {
                    foreach (var option in _currentResearchPaperData.FixRequiredOptions)
                    {
                        if (_fixButtonPairs[option])
                        {
                            //Student NPC will provide feedback
                            OnQualityUpdated?.Invoke(++_qualityValue);
                            OnProgressUpdated?.Invoke(++_progressValue);
                            OnFeedbackInitiated?.Invoke(_currentResearchPaperData.StudentReaction);
                            OnPaperDecided(false);
                            return;
                        }
                    }

                    OnFeedbackInitiated?.Invoke(_currentResearchPaperData.AuditorReaction);
                    OnPaperDecided(false);
                }
            }
        }

        public void HandleFixingOption(char optionIndex, bool isPressed)
        {
            _fixButtonPairs[optionIndex] = isPressed;

            OnOptionHighlighted?.Invoke(_fixButtonPairs);
            OnFix?.Invoke(_fixButtonPairs.ContainsValue(true));
        }

        public void NextRule()
        {
            if (_rules.Count == 1)
            {
                OnRulesUpdated?.Invoke(_rules[0], null);
                OnRulesUpdated?.Invoke(null, null);
                return;
            }
            else if (_rules.Count == 2)
            {
                OnRulesUpdated?.Invoke(_rules[0], _rules[1]);
                OnRulesUpdated?.Invoke(null, null);
                return;
            }

            if (_rulePageIndex + 2 >= _rules.Count)
            {
                OnRulesUpdated?.Invoke(_rules[_rules.Count - 2], null);
            }
            else
            {
                OnRulesUpdated?.Invoke(_rules[_rulePageIndex + 2], _rules[_rulePageIndex + 3]);
                _rulePageIndex += 2;

                if (_rulePageIndex + 2 >= _rules.Count)
                {
                    OnRulesUpdated?.Invoke(_rules[_rules.Count - 2], null);
                    _rulePageIndex = _rules.Count - 2;
                }
            }
        }

        public void PreviousRule()
        {
            if (_rules.Count == 1)
            {
                OnRulesUpdated?.Invoke(_rules[0], null);
                OnRulesUpdated?.Invoke(null, null);
                return;
            }
            else if (_rules.Count == 2)
            {
                OnRulesUpdated?.Invoke(_rules[0], _rules[1]);
                OnRulesUpdated?.Invoke(null, null);
                return;
            }

            if (_rulePageIndex - 2 < 0)
            {
                OnRulesUpdated?.Invoke(null, _rules[1]);
            }
            else
            {
                OnRulesUpdated?.Invoke(_rules[_rulePageIndex - 2], _rules[_rulePageIndex - 1]);
                _rulePageIndex -= 2;

                if (_rulePageIndex - 2 < 0)
                {
                    OnRulesUpdated?.Invoke(null, _rules[1]);
                    _rulePageIndex = 0;
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

        private void InitiateFixButtons()
        {
            _fixButtonPairs = new Dictionary<char, bool>();

            foreach (var item in _currentLevelData.ActiveOptionsToFix)
            {
                _fixButtonPairs.Add(item, false);
            }
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