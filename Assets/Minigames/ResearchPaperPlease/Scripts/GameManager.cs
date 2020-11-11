using System;
using System.Collections.Generic;
using UnityEngine;

namespace Methodyca.Minigames.ResearchPaperPlease
{
    [Serializable]
    public class ResearchPaper
    {
        public string Title;
        public string Author;
        public string Supervisor;
        public string ResearchGoal;
        public string ResearchMethodology;
        public string ResearchMethods;
        [TextArea(1, 4)] public string ResearchQuestions;
    }
    public class GameManager : Singleton<GameManager>
    {
        public static event Action<ResearchPaper> OnPaperAccepted = delegate { };
        public static event Action<ResearchPaper> OnPaperRejected = delegate { };
        public static event Action OnNextPaper = delegate { };

        LinkedList<ResearchPaper> researchPapers;

        Dictionary<char, string> keyValuePairs = new Dictionary<char, string>();
        ResearchPaper _current;

        public void HandleNextPaper()
        {
            OnNextPaper?.Invoke();
        }

        public void HandleAcceptedPaper()
        {
            //OnPaperAccepted?.Invoke();
        }

        public void HandleRejectedPaper()
        {
           // OnPaperRejected?.Invoke();
        }

        private void Start()
        {
            keyValuePairs = new Dictionary<char, string>()
            {
                { 'a', _current.Title },
                { 'b', _current.Author },
                { 'c', _current.Supervisor },
                { 'd', _current.ResearchGoal },
                { 'e', _current.ResearchMethodology },
                { 'f', _current.ResearchMethods },
                { 'g', _current.ResearchQuestions }
            };
        }
    }
}