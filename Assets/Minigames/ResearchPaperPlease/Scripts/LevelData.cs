using UnityEngine;

namespace Methodyca.Minigames.ResearchPaperPlease
{
    public class LevelData : MonoBehaviour
    {
        [SerializeField] private int level;
        [SerializeField] private int acceptedPaperTreshold;
        [SerializeField, TextArea(1, 3)] private string levelInitiatedMessage;
        [SerializeField] private Feedback positiveLevelFeedback;
        [SerializeField] private Feedback negativeLevelFeedback;
        [SerializeField] private char[] effectiveOptionsToFix;
        [SerializeField, TextArea(1, 3)] private string[] levelRules;
        [SerializeField] private ResearchPaperData[] researches;

        public int Level { get => level; }
        public int AcceptedPaperTreshold { get => acceptedPaperTreshold; }
        public string LevelInitiatedMessage { get => levelInitiatedMessage; }
        public string[] LevelRules { get => levelRules; }
        public Feedback PositiveLevelFeedback { get => positiveLevelFeedback; }
        public Feedback NegativeLevelFeedback { get => negativeLevelFeedback; }
        public char[] ActiveOptionsToFix { get => effectiveOptionsToFix; }
        public ResearchPaperData[] Researches { get => researches; }
    }
}