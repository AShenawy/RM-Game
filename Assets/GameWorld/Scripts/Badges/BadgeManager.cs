using UnityEngine;
using System.Collections.Generic;

namespace Methodyca.Core
{
    // This class handles badge progression and locked/unlocked badges in player phone
    public class BadgeManager : MonoBehaviour, ISaveable, ILoadable
    {
        #region Singleton
        public static BadgeManager instance;
        private void Awake()
        {
            if (instance == null)
                instance = this;
        }
        #endregion

        public BadgeUI badgeUI;
        public List<int> badgesEarned = new List<int>();


        public void SetMinigameComplete(Minigames id)
        {
            badgeUI.DisplayBagdeInApp(id);
        }

        public void SaveState()
        {
            throw new System.NotImplementedException();
        }

        public void LoadState()
        {
            throw new System.NotImplementedException();
        }
    }

    // a list of all available minigames
    public enum Minigames { Sorting, DocStudy, Participatory, Prototyping, Questionnaire, Research }
}