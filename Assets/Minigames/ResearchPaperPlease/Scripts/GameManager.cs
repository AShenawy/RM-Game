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
        public string Name;
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
        [SerializeField] private Feedback[] introSpeech;

        // Main game connection
        [SerializeField] private GameObject winGameButton;

        public static event Action<bool> OnFix = delegate { };
        public static event Action<bool> OnPaperDecided = delegate { };
        public static event Action<int> OnProgressUpdated = delegate { };
        public static event Action<int> OnQualityUpdated = delegate { };
        public static event Action<string> OnLevelOver = delegate { };
        public static event Action<int, int> OnPageUpdated = delegate { };
        public static event Action<string, string> OnRulesUpdated = delegate { };
        public static event Action<bool> OnGameOver = delegate { };
        public static event Action<Feedback> OnFeedbackInitiated = delegate { };
        public static event Action<LevelData> OnLevelInitiated = delegate { };
        public static event Action<ResearchPaperData> OnPaperUpdated = delegate { };
        public static event Action<Dictionary<char, bool>> OnOptionHighlighted = delegate { };

        public int TotalPaperCount { get; private set; }
        public int ProgressValueToWin { get => progressValueToWin; }
        public int QualityValueToWin { get => qualityValueToWin; }

        private LevelData _currentLevelData;
        private ResearchPaperData _currentResearchPaperData;
        private Queue<Feedback> _introSpeech;
        private Queue<ResearchPaperData> _allResearchPaper;
        private Dictionary<char, bool> _fixButtonPairs;
        private Dictionary<int, ResearchPaperData[]> _currentResearchPaperDataByLevel = new Dictionary<int, ResearchPaperData[]>();
        private List<ResearchPaperData> _acceptedPaperData = new List<ResearchPaperData>();
        private List<string> _rules;

        private int _rulePageIndex = -2;
        private int _qualityValue = 0;
        private int _progressValue = 0;
        private int _currentLevelIndex = 0;
        private int _initialTotalPaperCountPerLevel;

        private const int _maxProgressionValueToWin = 20;


        // Main Game Connection
        public void DisplayWinGameButton()
        {
            winGameButton.SetActive(true);
        }

        public void InitiateNextLevel()
        {
            if (_introSpeech.Count > 0)
            {
                OnFeedbackInitiated?.Invoke(_introSpeech.Dequeue());
                return;
            }

            _allResearchPaper = GetResearchPaperByLevel(++_currentLevelIndex);

            if (_allResearchPaper == null) //All of research paper are completed (Game Over)
            {
                if (_progressValue > progressValueToWin && _progressValue <= _maxProgressionValueToWin && _qualityValue > qualityValueToWin)
                {
                    OnGameOver?.Invoke(true);
                    OnFeedbackInitiated?.Invoke(winFeedback);
                }
                else if (_progressValue > progressValueToWin && _progressValue <= _maxProgressionValueToWin && _qualityValue <= qualityValueToWin)
                {
                    loseFeedback.Speech += " Too many papers were rejected for wrong reasons. I suggest you try again and be more careful.";
                    OnGameOver?.Invoke(false);
                    OnFeedbackInitiated?.Invoke(loseFeedback);
                }
                else if (_progressValue > _maxProgressionValueToWin)
                {
                    loseFeedback.Speech += " Too many low-quality research plans got accepted. I suggest you try again and be more careful.";
                    OnGameOver?.Invoke(false);
                    OnFeedbackInitiated?.Invoke(loseFeedback);
                }
                else
                {
                    loseFeedback.Speech += " Too many papers were rejected for wrong reasons, and many low-quality research plans got accepted. I suggest you try again and be more careful.";
                    OnGameOver?.Invoke(false);
                    OnFeedbackInitiated?.Invoke(loseFeedback);
                }
            }
            else //Initiate next level if there is any
            {
                _initialTotalPaperCountPerLevel = _allResearchPaper.Count;
                _acceptedPaperData = new List<ResearchPaperData>();
                _currentLevelData = GetCurrentLevelData();
                _rules = new List<string>(_currentLevelData.LevelRules);

                NextRule();
                PreviousRule();
                InitiateFixButtons();
                OnLevelInitiated?.Invoke(_currentLevelData);
            }
        }

        public void HandleNextPaper()
        {
            OnFeedbackInitiated?.Invoke(null);

            if (_allResearchPaper != null)
            {
                if (_allResearchPaper.Count > 0)
                {
                    _currentResearchPaperData = _allResearchPaper.Dequeue();
                    OnPaperUpdated?.Invoke(_currentResearchPaperData);
                    OnPageUpdated?.Invoke(_initialTotalPaperCountPerLevel - _allResearchPaper.Count, _initialTotalPaperCountPerLevel);
                }
                else
                {
                    _allResearchPaper = null;
                    OnLevelOver?.Invoke($"<b>LEVEL {_currentLevelIndex}</b> is completed.");

                    if (_acceptedPaperData.Count > _currentLevelData.AcceptedPaperTreshold)
                    {
                        OnFeedbackInitiated?.Invoke(_currentLevelData.PositiveLevelFeedback);
                    }
                    else
                    {
                        OnFeedbackInitiated?.Invoke(_currentLevelData.NegativeLevelFeedback);
                    }
                }
            }
            else
            {
                InitiateNextLevel();
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
                    OnOptionHighlighted?.Invoke(GetFixedRequiredOptionDictionary());
                }
                else
                {
                    OnProgressUpdated?.Invoke(++_progressValue);
                    OnQualityUpdated?.Invoke(--_qualityValue);
                    OnFeedbackInitiated?.Invoke(_currentResearchPaperData.AuditorReaction);
                    OnOptionHighlighted?.Invoke(GetFixedRequiredOptionDictionary());
                }

                OnPaperDecided?.Invoke(true);
            }
            else
            {
                if (_currentResearchPaperData.Quality == PaperQuality.High)
                {
                    OnQualityUpdated?.Invoke(--_qualityValue);
                    OnPaperDecided?.Invoke(false);
                    OnFeedbackInitiated?.Invoke(_currentResearchPaperData.AuditorReaction);
                    OnOptionHighlighted?.Invoke(GetFixedRequiredOptionDictionary());
                }
                else if (_currentResearchPaperData.Quality == PaperQuality.Medium)
                {
                    foreach (var option in _currentResearchPaperData.FixRequiredOptions)
                    {
                        if (_fixButtonPairs[option])
                        {
                            OnQualityUpdated?.Invoke(++_qualityValue);
                            break;
                        }
                    }

                    OnFeedbackInitiated?.Invoke(_currentResearchPaperData.AuditorReaction);
                    OnOptionHighlighted?.Invoke(GetFixedRequiredOptionDictionary());
                    OnPaperDecided?.Invoke(false);
                }
                else
                {
                    foreach (var option in _currentResearchPaperData.FixRequiredOptions)
                    {
                        if (_fixButtonPairs[option])
                        {
                            OnQualityUpdated?.Invoke(++_qualityValue);
                            OnFeedbackInitiated?.Invoke(_currentResearchPaperData.StudentReaction);
                            OnPaperDecided?.Invoke(false);
                            return;
                        }
                    }

                    OnFeedbackInitiated?.Invoke(_currentResearchPaperData.AuditorReaction);
                    OnOptionHighlighted?.Invoke(GetFixedRequiredOptionDictionary());
                    OnPaperDecided?.Invoke(false);
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

        public void HandleRestartGame()
        {
            _rulePageIndex = -2;
            _qualityValue = 0;
            _progressValue = 0;
            _currentLevelIndex = 0;
            _currentResearchPaperDataByLevel = GetResearchPaperDataByLevel();

            OnProgressUpdated?.Invoke(_progressValue);
            OnQualityUpdated?.Invoke(_qualityValue);

            TotalPaperCount = GetTotalResearchPaperCount();
            InitiateNextLevel();
        }

        private void Start()
        {
            _currentResearchPaperDataByLevel = GetResearchPaperDataByLevel();
            _introSpeech = new Queue<Feedback>(introSpeech);
            TotalPaperCount = GetTotalResearchPaperCount();
            InitiateNextLevel();
        }

        private Dictionary<char, bool> GetFixedRequiredOptionDictionary()
        {
            var result = new Dictionary<char, bool>(_fixButtonPairs);

            foreach (var item in _currentResearchPaperData.FixRequiredOptions)
            {
                result[item] = true;
            }

            return result;
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
                return new Queue<ResearchPaperData>(researchPaperData.Shuffle());
            }

            return null;
        }
    }
}