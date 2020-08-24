using UnityEngine;


namespace Methodyca.Core
{
    // this script tells main game that minigame is won 
    public class WinMinigame : MonoBehaviour
    {
        [SerializeField, Tooltip("What is the tag of the Game Object that starts minigame?")]
        private string minigameHubTag;
        private Object minigameAccessObject;

        private void Start()
        {
            minigameAccessObject = GameObject.FindGameObjectWithTag(minigameHubTag).GetComponent<SortingGameInteraction>();
        }

        public void CompleteMinigame()
        {

        }
    }
}