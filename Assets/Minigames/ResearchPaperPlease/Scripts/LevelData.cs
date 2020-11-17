using UnityEngine;

namespace Methodyca.Minigames.ResearchPaperPlease
{
    public class LevelData : MonoBehaviour
    {
        [SerializeField] private int level;
        [SerializeField, TextArea(1, 3)] private string levelRules;
        [SerializeField] private Feedback levelFeedback;
        [SerializeField] private Sprite auditorCharacter;
        [SerializeField] private char[] effectiveOptionsToFix;
        [SerializeField] private ResearchPaperData[] researches;

        public int Level { get => level; }
        public string LevelRules { get => levelRules; }
        public Feedback LevelFeedback { get => levelFeedback; }
        public Sprite AuditorCharacter { get => auditorCharacter; }
        public char[] ActiveOptionsToFix { get => effectiveOptionsToFix; }
        public ResearchPaperData[] Researches { get => researches; }
    }
}