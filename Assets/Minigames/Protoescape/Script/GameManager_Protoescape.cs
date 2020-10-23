using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Methodyca.Minigames.Protoescape
{
    public enum ConfusionType { None, Location, Color, Sprite, Highlight, Font }

    public class GameManager_Protoescape : Singleton<GameManager_Protoescape>
    {
        [SerializeField] private ScreenBox[] screenBoxes;

        public static event Action<bool> OnStackMove = delegate { };
        public static event Action<GameObject> OnSelected = delegate { };

        public static GameObject SelectedEntity { get => _selectedEntity; set { _selectedEntity = value; OnSelected?.Invoke(value); } }
        private static GameObject _selectedEntity;

        public static bool IsStacksMovable { get => _isStacksMovable; set { _isStacksMovable = value; OnStackMove?.Invoke(value); } }
        static bool _isStacksMovable;

        private List<ScreenBox> _allScreenBoxes;

        public IEnumerable<ICheckable> GetAllSelectedCheckablesToTest()
        {
            while (_allScreenBoxes.Count > 0)
            {
                var selections = GetRandomScreenBox().GetRandomCheckablesBy();

                foreach (var item in selections)
                {
                    yield return item;
                }
            }
        }

        private void Start()
        {
            IsStacksMovable = false;
            _allScreenBoxes = new List<ScreenBox>(screenBoxes);
        }

        private ScreenBox GetRandomScreenBox()
        {
            if (_allScreenBoxes.Count <= 0)
            {
                return null;
            }

            var rndBox = screenBoxes[Random.Range(0, _allScreenBoxes.Count)];
            _allScreenBoxes.Remove(rndBox);

            return rndBox;
        }
    }
}