using UnityEngine;
using System.Collections;

namespace Methodyca.Core
{
    // this script is for the Map in Act2, QL World, N1
    public class Map : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer mapDisplayObject;
        [SerializeField] private Sprite[] mapCompletionLevels;


        private void OnEnable()
        {
            // need to sub to event in case returning from minigame and Start() precedes BadgeManager
            BadgeManager.instance.minigamesChanged += UpdateMap;
        }

        void Start()
        {
            UpdateMap();
        }

        // progress level depends on minigames complete. player enters Act 2 N1 with 1 minigame complete (sorting)
        // they can do up to 4 minigames in N1-QL which totals to 5 minigames. QL & QN games are the same
        void UpdateMap()
        {
            int minigamesDone = BadgeManager.instance.minigamesComplete.Count;
            switch (minigamesDone)
            {
                case 1:  // only sorting completed
                    SetMapLevel(0);
                    break;
                case 2:
                    SetMapLevel(1);
                    break;
                case 3:
                    SetMapLevel(2);
                    break;
                case 4:
                    SetMapLevel(3);
                    break;
                case int i when minigamesDone >= 5:  // all N1 minigames won or coming from N2/3 with more minigames won
                    SetMapLevel(4);
                    break;
                default:
                    Debug.LogWarning($"Wrong map display level {minigamesDone}");
                    goto case 1;
            }

            void SetMapLevel(int level)
            {
                mapDisplayObject.sprite = mapCompletionLevels[level];
            }
        }

        private void OnDisable()
        {
            BadgeManager.instance.minigamesChanged -= UpdateMap;
        }
    }
}