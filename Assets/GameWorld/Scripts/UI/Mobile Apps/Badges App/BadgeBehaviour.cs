using UnityEngine;
using UnityEngine.UI;


namespace Methodyca.Core
{
    // script to be placed on badges in the badges app
    public class BadgeBehaviour : MonoBehaviour
    {
        public Minigames minigameID;
        private Text badgeText;


        // Start is called before the first frame update
        void Start()
        {
            badgeText = GetComponentInChildren<Text>();
        }
    }
}