using UnityEngine;

namespace Methodyca.Core
{
    // This class handles badge progression and locked/unlocked badges in player phone
    public class BadgeManager : MonoBehaviour
    {
        #region Singleton
        public static BadgeManager instance;
        private void Awake()
        {
            if (instance == null)
                instance = this;
        }
        #endregion



    }
}