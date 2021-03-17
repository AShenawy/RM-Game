using UnityEngine;

namespace Methodyca.Core
{
    // this script is for Student NPC character in Act 2, N1, QN
    public class StudentCharBehaviour : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer charDisplayObject;
        [SerializeField] private Sprite[] beerLevels;


        private void OnEnable()
        {
            // need to sub to event in case returning from minigame and Start() precedes BadgeManager
            BadgeManager.instance.minigamesChanged += UpdateBeer;
        }

        void Start()
        {
            UpdateBeer();
        }

        // changes the char images with beer levels
        void UpdateBeer()
        {
            int minigamesDone = BadgeManager.instance.minigamesComplete.Count;

            switch (minigamesDone)
            {
                case 1:  // only sorting completed
                    SetBeerLevel(0);
                    break;
                case 2:
                    SetBeerLevel(1);
                    break;
                case 3:
                    SetBeerLevel(2);
                    break;
                case 4:
                    SetBeerLevel(3);
                    break;
                case int i when minigamesDone >= 5:  // all N1 minigames won or coming from N2/3 with more minigames won
                    SetBeerLevel(4);
                    break;
                default:
                    Debug.LogWarning($"Wrong beer display level {minigamesDone}");
                    goto case 1;
            }

            void SetBeerLevel(int level)
            {
                charDisplayObject.sprite = beerLevels[level];
            }
        }

        private void OnDisable()
        {
            BadgeManager.instance.minigamesChanged -= UpdateBeer;
        }
    }
}