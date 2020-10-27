using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Methodyca.Minigames.Protoescape
{
    public enum CategoryType { None, Location, Color, Sprite, Highlight, Font, Consistency }

    public class GameManager_Protoescape : Singleton<GameManager_Protoescape>
    {
        [SerializeField] private ScreenBox[] screenBoxes;

        public static event Action<bool> OnStackMove = delegate { };
        public static event Action OnPrototypeInitiated= delegate { };
        public static event Action OnPrototypeTestInitiated= delegate { };
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

        public IEnumerable<ICheckable> GetAllCheckables()
        {
            foreach (var box in screenBoxes)
            {
                foreach (var checkables in box.GetAllCheckables())
                {
                    yield return checkables;
                }
            }
        }

        internal void HandlePrototypeTesting()
        {
            OnPrototypeTestInitiated?.Invoke();
        }

        public void HandlePrototypeInitiation()
        {
            OnPrototypeInitiated?.Invoke();
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