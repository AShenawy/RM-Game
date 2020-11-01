using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Methodyca.Minigames.Protoescape
{
    public class PrototypeTester : MonoBehaviour
    {
        [SerializeField] private int selectionCountToPointAt;

        public static event Action<int, int> OnPrototypeTestCompleted = delegate { };
        public static event Action OnPrototypeTestInitiated = delegate { };
        public static event Action<ICheckable> OnSelectionPointed = delegate { };

        private List<ICheckable> _allCheckables;
        private List<ICheckable> _checkablesToTest;

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(1);
            _allCheckables = new List<ICheckable>(GameManager_Protoescape.Instance.GetAllCheckables());
        }

        /// <summary>
        /// Called in the editor. Click Alien event.
        /// </summary>
        public void InitiatePrototypeTesting()
        {
            _checkablesToTest = new List<ICheckable>(GameManager_Protoescape.Instance.GetRandomCheckablesBy(Mathf.Abs(selectionCountToPointAt)));
            OnPrototypeTestInitiated?.Invoke();
            PointSelectedCheckable();
        }

        /// <summary>
        /// Called in the editor. Click "like" or "confuse" buttons.
        /// </summary>
        public void PointSelectedCheckable()
        {
            if (_checkablesToTest.Count <= 0)
            {
                OnSelectionPointed?.Invoke(null);
                return;
            }

            var checkable = _checkablesToTest[Random.Range(0, _checkablesToTest.Count)];
            OnSelectionPointed?.Invoke(checkable);
            _checkablesToTest.Remove(checkable);
        }

        /// <summary>
        /// Called in the editor. Click event of "Skip"
        /// </summary>
        public void CompletePrototypeTesting()
        {
            var result = GetLikedCategoryRate();
            OnPrototypeTestCompleted?.Invoke(result.current, result.total);
        }

        private (int current, int total) GetLikedCategoryRate()
        {
            int likeCount = GetCategoryChecklistByStatus().Count(i => i.Value);

            if (IsAllConsistent())
            {
                likeCount++;
            }

            return (likeCount, 6);
        }

        private Dictionary<CategoryType, bool> GetCategoryChecklistByStatus()
        {
            var checklist = new Dictionary<CategoryType, bool>()
            {
                { CategoryType.Color, true },
                { CategoryType.Icon, true },
                { CategoryType.Position, true },
                { CategoryType.Font, true },
                { CategoryType.Highlight, true }
            };

            foreach (var item in _allCheckables)
            {
                var results = item.GetLikables();

                foreach (var i in results)
                {
                    if (!item.Categories.Contains(i.Key))
                    {
                        checklist[i.Key] = false;
                    }
                }
            }

            return checklist;
        }

        private bool IsAllConsistent()
        {
            var all = new List<ICheckable>(_allCheckables);
            var checklist = new List<bool>();

            foreach (var item in all)
            {
                item.IsChecked = false;
            }

            for (int i = 0; i < all.Count - 1; i++)
            {
                if (all[i].IsChecked)
                {
                    continue;
                }

                for (int j = i + 1; j < all.Count; j++)
                {
                    if (all[i].EntityID == all[j].EntityID)
                    {
                        checklist.Add(all[i].GetLikables().Values.All(v => all[j].GetLikables().ContainsValue(v)));
                    }
                }
                all[i].IsChecked = true;
            }

            if (checklist.GroupBy(x => x).Skip(1).Any())
            {
                return false;
            }

            return true;
        }
    }
}