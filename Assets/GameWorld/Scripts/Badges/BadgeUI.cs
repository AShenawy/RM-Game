using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


namespace Methodyca.Core
{
    // This script is responsible for displaying badges in the badges app
    public class BadgeUI : MonoBehaviour
    {
        public BadgeManager badgeManager;
        public List<BadgeBehaviour> badgeSlots;

        [Header("Badge Information Displat")]
        public Text titleDisplay, descriptionDisplay;


        private void OnEnable()
        {
            badgeManager.minigamesChanged += UpdateUI;

            foreach (BadgeBehaviour badge in badgeSlots)
            {
                if (!badge.isBadgeWon)
                    badge.SetBadgeActive(false);
            }
        }

        // Use this for initialization
        void Start()
        {
            ClearTextDisplays();

            foreach (BadgeBehaviour badge in badgeSlots)
                badge.cursorOverBadge += DisplayBadgeInfo;

        }

        void ClearTextDisplays()
        {
            titleDisplay.text = "";
            descriptionDisplay.text = "";
        }

        public void DisplayBagdeInApp(Minigames id)
        {
            BadgeBehaviour badge = badgeSlots.Find(x => x.minigameID == id);
            badge.isBadgeWon = true;
            badge.SetBadgeActive(true);
        }

        private void UpdateUI()
        {
            foreach (int gameID in badgeManager.minigamesComplete)
                DisplayBagdeInApp((Minigames)gameID);
        }

        public void DisplayBadgeInfo(BadgeBehaviour badge)
        {
            if (badge.isBadgeWon)
            {
                titleDisplay.text = badge.badgeTitle;
                descriptionDisplay.text = badge.badgeDescription;
            }
            else
            {
                titleDisplay.text = "Badge Locked";
                descriptionDisplay.text = "Complete " + badge.minigameName + " minigame to unlock this badge";
            }
        }

        private void OnDestroy()
        {
            badgeManager.minigamesChanged -= UpdateUI;

            foreach (BadgeBehaviour badge in badgeSlots)
                badge.cursorOverBadge -= DisplayBadgeInfo;
        }
    }
}