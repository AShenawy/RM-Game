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
        [SerializeField] private UIEntitySelector selector;
        [SerializeField, TextArea(1, 3)] private string negativeFeedback;
        [SerializeField, TextArea(1, 3)] private string positiveFeedback;

        public static event Action<string> OnPrototypeTestCompleted = delegate { };
        public static event Action OnPrototypeTestInitiated = delegate { };
        public static event Action<ICheckable> OnSelectionPointed = delegate { };

        private List<ICheckable> _allCheckables = new List<ICheckable>();
        private List<ICheckable> _checkablesToTest = new List<ICheckable>();
        private readonly int _categorySize = Enum.GetNames(typeof(CategoryType)).Length;

        private IEnumerator Start()
        {
            yield return null;
            _allCheckables = new List<ICheckable>(GameManager_Protoescape.Instance.GetAllCheckables());
        }

        /// <summary>
        /// Called in the editor. Click Alien event.
        /// </summary>
        public void InitiatePrototypeTesting()
        {
            if (_allCheckables.Count <= 0)
            {
                return;
            }
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
                selector.Select(null);
                OnSelectionPointed?.Invoke(null);
                return;
            }

            var checkable = _checkablesToTest[Random.Range(0, _checkablesToTest.Count)];
            OnSelectionPointed?.Invoke(checkable);
            selector.Select(checkable.gameObject);
            _checkablesToTest.Remove(checkable);
        }

        /// <summary>
        /// Called in the editor. Click event of "Skip/Complete"
        /// </summary>
        public void CompletePrototypeTesting()
        {
            var result = GetLikedCategoryRate();
            var ratio = result.current / (float)result.total;

            Debug.Log("Result: " + result.current + "/" + result.total);

            if (ratio < 0.1f) //confused
            {
                OnPrototypeTestCompleted?.Invoke(negativeFeedback);
            }
            else if (ratio >= 0.1f && ratio <= 0.8f)
            {
                string liked = string.Empty;
                string confused = string.Empty;

                foreach (var item in GetCategoryChecklistByStatus())
                {
                    if (item.Value)
                    {
                        if (string.IsNullOrEmpty(liked))
                        {
                            liked = $"{item.Key}";
                        }
                        else
                        {
                            liked += $", {item.Key}";
                        }
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(confused))
                        {
                            confused = $"{item.Key}";
                        }
                        else
                        {
                            confused += $", {item.Key}";
                        }
                    }
                }

                string feedback = $"Looks like they enjoyed <b>{liked}</b>. Unfortunately, you’ll still need to work on <b>{confused}</b>. " +
                                   "Make sure you get them right this time! I got grandkids waiting at home.";

                OnPrototypeTestCompleted?.Invoke(feedback);
            }
            else //liked
            {
                OnPrototypeTestCompleted?.Invoke(positiveFeedback);
            }
        }

        private (int current, int total) GetLikedCategoryRate()
        {
            int likeCount = GetCategoryChecklistByStatus().Count(i => i.Value == true);

            if (IsAllConsistent())
            {
                likeCount++;
            }

            return (likeCount, _categorySize);
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

            foreach (var checkable in _allCheckables)
            {
                var differences = checkable.Categories.Except(checkable.GetLikables().Keys);

                foreach (var difference in differences)
                {
                    checklist[difference] = false;
                }
            }

            return checklist;
        }

        private bool IsAllConsistent()
        {
            var all = _allCheckables;
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