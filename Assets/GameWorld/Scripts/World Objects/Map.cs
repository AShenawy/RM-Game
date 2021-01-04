using UnityEngine;
using System.Collections;

namespace Methodyca.Core
{
    // this script is for the Map in Act2, QL World, N1
    public class Map : MonoBehaviour, ISaveable, ILoadable
    {
        [SerializeField] private SpriteRenderer mapDisplayObject;
        [SerializeField] private Sprite[] mapCompletionLevels;

        private int currentMapLevel;

        // Use this for initialization
        void Start()
        {
            LoadState();
        }

        public void UpdateMap(int level)
        {
            currentMapLevel = Mathf.Clamp(level, 0, 4);
            mapDisplayObject.sprite = mapCompletionLevels[currentMapLevel];
            SaveState();
        }

        public void SaveState()
        {
            SaveLoadManager.SetInteractableState(name, currentMapLevel);
        }

        public void LoadState()
        {
            if (SaveLoadManager.interactableStates.TryGetValue(name, out int level))
                UpdateMap(level);
        }
    }
}