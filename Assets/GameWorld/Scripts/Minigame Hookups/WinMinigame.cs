using UnityEngine;


namespace Methodyca.Core
{
    // this script tells main game that minigame is won
    // Currently only works for Additive loaded mingame scenes (since minigame hub script is in main game scene files)
    public class WinMinigame : MonoBehaviour
    {
        [SerializeField, Tooltip("What is the tag of the Game Object that starts minigame?")]
        private string minigameHubTag;
        private MinigameHub gameHub;

        private void Start()
        {
            gameHub = GameObject.FindGameObjectWithTag(minigameHubTag).GetComponent<MinigameHub>();
        }

        public void CompleteMinigame()
        {
            gameHub.EndGame();
        }
    }
}