using UnityEngine;


namespace Methodyca.Minigames.PartLoop
{
    [System.Serializable]
    public struct Idea
    {
        [TextArea(5, 7)]
        public string idea;
        public string designDocEntry;
        public bool appealsToClient;
        [TextArea(5, 7)]
        public string clientResponse;

        public void Clear()
        {
            idea = null;
            designDocEntry = null;
            appealsToClient = false;
            clientResponse = null;
        }
    }
}