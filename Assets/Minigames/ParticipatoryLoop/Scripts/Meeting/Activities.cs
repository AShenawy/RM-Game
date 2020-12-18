using UnityEngine;

namespace Methodyca.Minigames.PartLoop
{
    public class Activities : MonoBehaviour
    {
        public Activity[] activities;

        private void Awake()
        {
            for (int i = 0; i < activities.Length; i++)
            {
                if (!activities[i].isHappening)
                {

                }
            }
        }
    }
}