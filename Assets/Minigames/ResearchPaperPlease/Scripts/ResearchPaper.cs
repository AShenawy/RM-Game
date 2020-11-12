using UnityEngine;

namespace Methodyca.Minigames.ResearchPaperPlease
{
    public class ResearchPaper : MonoBehaviour
    {
        [SerializeField] private int level;
        [SerializeField] private ResearchPaperData[] researches;

        public int Level { get => level; }
        public ResearchPaperData[] Researches { get => researches; }
    }
}