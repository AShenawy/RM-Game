using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Methodyca.Minigames.Protoescape
{
    public enum CategoryType { None, Position, Color, Icon, Highlight, Font, Consistency }

    public class GameManager_Protoescape : Singleton<GameManager_Protoescape>
    {
        [SerializeField] private ScreenBox[] screenBoxes;

        public static event Action<bool> OnStackMove = delegate { };
        public static event Action OnPrototypeInitiated = delegate { };
        public static event Action<GameObject> OnSelected = delegate { };

        public static GameObject SelectedEntity { get => _selectedEntity; set { _selectedEntity = value; OnSelected?.Invoke(value); } }
        private static GameObject _selectedEntity;

        public static bool IsStacksMovable { get => _isStacksMovable; set { _isStacksMovable = value; OnStackMove?.Invoke(value); } }
        static bool _isStacksMovable;

        public List<ICheckable> GetRandomCheckablesBy(int total)
        {
            var likables = new List<ICheckable>(GetAllLikables());
            var confusings = new List<ICheckable>(GetAllConfusings());

            int half = Mathf.RoundToInt(total * 0.5f);

            var l = likables.Take(half).ToList();
            var c = confusings.Take(total - l.Count);

            return l.Concat(c).ToList();
        }

        public IEnumerable<ICheckable> GetAllLikables()
        {
            var all = GetAllCheckables().ToList();

            foreach (var item in all)
            {
                if (!(item.GetLikables().Count > 0))
                {
                    yield return item;
                }
            }
        }

        public IEnumerable<ICheckable> GetAllConfusings()
        {
            var all = GetAllCheckables().ToList();

            foreach (var item in all)
            {
                if (item.GetLikables().Count > 0)
                {
                    yield return item;
                }
            }
        }

        public IEnumerable<ICheckable> GetAllCheckables()
        {
            foreach (var box in screenBoxes)
            {
                foreach (var checkable in box.GetAllCheckables())
                {
                    if (!string.IsNullOrEmpty(checkable.EntityID))
                    {
                        yield return checkable;
                    }
                }
            }
        }

        public void HandlePrototypeInitiation()
        {
            OnPrototypeInitiated?.Invoke();
        }

        private void Start()
        {
            IsStacksMovable = false;
        }
    }
}