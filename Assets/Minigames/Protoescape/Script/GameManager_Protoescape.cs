using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Methodyca.Minigames.Protoescape
{
    public enum CategoryType { Position, Color, Icon, Highlight, Font, Consistency }

    public class GameManager_Protoescape : Singleton<GameManager_Protoescape>
    {
        [SerializeField] private ScreenBox[] screenBoxes;

        public static event Action OnGameStarted = delegate { };
        public static event Action<bool> OnStackMove = delegate { };
        public static event Action OnPrototypeInitiated = delegate { };
        public static event Action<GameObject> OnSelected = delegate { };

        public static GameObject SelectedEntity { get => _selectedEntity; set { _selectedEntity = value; OnSelected?.Invoke(value); } }
        private static GameObject _selectedEntity;

        public void HandleGameStart()
        {
            OnGameStarted?.Invoke();
        }

        public void HandlePrototypeInitiation()
        {
            OnPrototypeInitiated?.Invoke();
        }

        public List<ICheckable> GetRandomCheckablesBy(int total)
        {
            var likables = GetAllLikables();
            var confusings = GetAllConfusings();

            int half = Mathf.RoundToInt(total * 0.5f);

            var l = likables.Shuffle().Take(half).ToList();
            var c = confusings.Shuffle().Take(total - l.Count).ToList();

            if (c.Count < half)
                l = likables.Shuffle().Take(total - c.Count).ToList();

            return l.Concat(c).ToList();
        }

        public IEnumerable<ICheckable> GetAllLikables()
        {
            foreach (var item in GetAllCheckables())
            {
                if (item.GetLikedCategories().Count == item.Categories.Count)
                {
                    yield return item;
                }
            }
        }

        public IEnumerable<ICheckable> GetAllConfusings()
        {
            foreach (var item in GetAllCheckables())
            {
                if (item.GetLikedCategories().Count < item.Categories.Count)
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
    }
}