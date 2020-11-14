using UnityEngine;

namespace Methodyca.Minigames.ResearchPaperPlease
{
    public class LevelData : MonoBehaviour
    {
        [SerializeField] private int level;
        [SerializeField, TextArea(1, 3)] private string levelFeedback;
        [SerializeField, TextArea(1, 3)] private string levelRules;
        [SerializeField] private char[] effectiveOptionsToFix;
        [SerializeField] private ResearchPaperData[] researches;

        public int Level { get => level; }
        public string LevelFeedback { get => levelFeedback; }
        public string LevelRules { get => levelRules; }
        public char[] ActiveOptionsToFix { get => effectiveOptionsToFix; }
        public ResearchPaperData[] Researches { get => researches; }
    }
}