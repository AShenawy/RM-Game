using UnityEngine;
using Methodyca.Core;


namespace Methodyca.Minigames.SortGame
{
    // this script tells main game that minigame is won
    // Currently only works for Additive loaded mingame scenes (since minigame hub script is in main game scene files)
    public class WinMinigame : MonoBehaviour
    {
        public Core.Minigames minigameID;
        [SerializeField, Header("Optional"), Tooltip("For additive-loaded type minigames.\nWhat is the tag of the Game Object that starts minigame?")]
        private string minigameHubTag;
        private MinigameHub gameHub;

        private void Start()
        {
            if (minigameHubTag != "")
                gameHub = GameObject.FindGameObjectWithTag(minigameHubTag).GetComponent<MinigameHub>();
        }

        // button click action
        // for additive-loaded minigames
        public void CompleteMinigame()
        {
            BadgeManager.instance.SetMinigameComplete((int)minigameID);
            gameHub.EndGame();
        }


        // button click action
        // for single-loaded minigames
        public void CompleteSingleLoadedMinigame()
        {
            SceneManagerScript.instance.minigamesWon.Add((int)minigameID);
        }
    }
}