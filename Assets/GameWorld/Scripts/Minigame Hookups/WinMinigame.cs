﻿using UnityEngine;
using Methodyca.Core;


namespace Methodyca.Minigames.SortGame
{
    // this script tells main game that minigame is won
    // Currently only works for Additive loaded mingame scenes (since minigame hub script is in main game scene files)
    public class WinMinigame : MonoBehaviour
    {
        public Core.Minigames minigameID;
        [SerializeField, Tooltip("What is the tag of the Game Object that starts minigame?")]
        private string minigameHubTag;
        private MinigameHub gameHub;

        private void Start()
        {
            gameHub = GameObject.FindGameObjectWithTag(minigameHubTag).GetComponent<MinigameHub>();
        }

        public void CompleteMinigame()
        {
            BadgeManager.instance.SetMinigameComplete(minigameID);
            gameHub.EndGame();
        }
    }
}