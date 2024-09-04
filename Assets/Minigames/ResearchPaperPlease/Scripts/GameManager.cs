using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        [SerializeField] private GameObject feedbackWindow; // Reference to the feedback window UI
        [SerializeField] private Text acceptedCorrectlyText;
        [SerializeField] private Text rejectedCorrectlyText;
        [SerializeField] private Text acceptedWronglyText;
        [SerializeField] private Text rejectedWronglyText;
        [SerializeField] private Feedback preLevel2Feedback;  // New serialized field for pre-level 2 feedback
        [SerializeField] private Feedback preLevel3Feedback;  // New serialized field for pre-level 3 feedback

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

        private int correctAcceptCount = 0;
        private int correctRejectCount = 0;
        private int incorrectAcceptCount = 0;
        private int incorrectRejectCount = 0;

        private const int _maxProgressionValueToWin = 20;

        private bool isPreLevelFeedbackShown = false; // Track if feedback for levels 2 or 3 has been shown

        public void InitiateNextLevel()
        {
            feedbackWindow.SetActive(false);

            // Display introductory speech first
            if (_introSpeech.Count > 0)
            {
                OnFeedbackInitiated?.Invoke(_introSpeech.Dequeue());
                return;
            }

            // Show feedback before levels 2 and 3, but ensure it's only shown once
            if ((_currentLevelIndex == 1 && !isPreLevelFeedbackShown))
            {
                OnFeedbackInitiated?.Invoke(preLevel2Feedback);
                isPreLevelFeedbackShown = true;
                return;
            }
            else if (_currentLevelIndex == 2 && !isPreLevelFeedbackShown)
            {
                OnFeedbackInitiated?.Invoke(preLevel3Feedback);
                isPreLevelFeedbackShown = true;
                return;
            }

            // Reset the flag for showing pre-level feedback after level 2
            isPreLevelFeedbackShown = false;

            _allResearchPaper = GetResearchPaperByLevel(++_currentLevelIndex);

            if (_allResearchPaper == null) // All research papers are completed (Game Over)
            {
                HandleGameOver();
            }
            else // Initiate the next level
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

                    ShowLevelFeedback(); // Display the feedback window
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
                    correctAcceptCount++;
                    OnProgressUpdated?.Invoke(++_progressValue);
                    OnQualityUpdated?.Invoke(++_qualityValue);
                }
                else if (_currentResearchPaperData.Quality == PaperQuality.Medium)
                {
                    correctAcceptCount++;
                    OnProgressUpdated?.Invoke(++_progressValue);
                    OnQualityUpdated?.Invoke(++_qualityValue);
                    OnFeedbackInitiated?.Invoke(_currentResearchPaperData.AuditorReaction);
                    OnOptionHighlighted?.Invoke(GetFixedRequiredOptionDictionary());
                }
                else
                {
                    incorrectAcceptCount++;
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
                    incorrectRejectCount++;
                    OnQualityUpdated?.Invoke(--_qualityValue);
                    OnPaperDecided?.Invoke(false);
                    OnFeedbackInitiated?.Invoke(_currentResearchPaperData.AuditorReaction);
                    OnOptionHighlighted?.Invoke(GetFixedRequiredOptionDictionary());
                }
                else if (_currentResearchPaperData.Quality == PaperQuality.Medium)
                {
                    bool wasFixed = false;
                    foreach (var option in _currentResearchPaperData.FixRequiredOptions)
                    {
                        if (_fixButtonPairs[option])
                        {
                            correctRejectCount++;
                            OnQualityUpdated?.Invoke(++_qualityValue);
                            wasFixed = true;
                            break;
                        }
                    }
                    if (!wasFixed) incorrectRejectCount++;

                    OnFeedbackInitiated?.Invoke(_currentResearchPaperData.AuditorReaction);
                    OnOptionHighlighted?.Invoke(GetFixedRequiredOptionDictionary());
                    OnPaperDecided?.Invoke(false);
                }
                else
                {
                    bool wasFixed = false;
                    foreach (var option in _currentResearchPaperData.FixRequiredOptions)
                    {
                        if (_fixButtonPairs[option])
                        {
                            correctRejectCount++;
                            OnQualityUpdated?.Invoke(++_qualityValue);
                            OnFeedbackInitiated?.Invoke(_currentResearchPaperData.StudentReaction);
                            wasFixed = true;
                            break;
                        }
                    }
                    if (!wasFixed) incorrectRejectCount++;

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

        private void HandleGameOver()
        {
            if (_progressValue > progressValueToWin && _progressValue <= _maxProgressionValueToWin && _qualityValue > qualityValueToWin)
            {
                OnGameOver?.Invoke(true);
                OnFeedbackInitiated?.Invoke(winFeedback);
            }
            else if (_progressValue > progressValueToWin && _progressValue <= _maxProgressionValueToWin && _qualityValue <= qualityValueToWin)
            {
                loseFeedback.Speech += " Too much paper was rejected for wrong reasons. I suggest you try again and be more careful.";
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
                loseFeedback.Speech += " Too much paper was rejected for wrong reasons, and many low-quality research plans got accepted. I suggest you try again and be more careful.";
                OnGameOver?.Invoke(false);
                OnFeedbackInitiated?.Invoke(loseFeedback);
            }
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

        private void ShowLevelFeedback()
        {
            feedbackWindow.SetActive(true);
            acceptedCorrectlyText.text = $"Accepted Correctly: {correctAcceptCount}";
            rejectedCorrectlyText.text = $"Rejected Correctly: {correctRejectCount}";
            acceptedWronglyText.text = $"Accepted Wrongly: {incorrectAcceptCount}";
            rejectedWronglyText.text = $"Rejected Wrongly: {incorrectRejectCount}";

            // Reset counters for next level
            correctAcceptCount = 0;
            correctRejectCount = 0;
            incorrectAcceptCount = 0;
            incorrectRejectCount = 0;
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
