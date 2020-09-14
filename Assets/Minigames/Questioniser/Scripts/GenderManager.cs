using UnityEngine;

namespace Methodyca.Minigames.Questioniser
{
    public class GenderManager : MonoBehaviour
    {
        public static GenderManager Current;

        public bool IsMale { get; set; } = false;

        private void Awake()
        {
            if (Current == null)
            {
                Current = this;
            }
            else
            {
                Destroy(this);
            }

            DontDestroyOnLoad(gameObject);
        }
    }
}