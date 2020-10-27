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
        public static event Action<int, int> OnPrototypeTested = delegate { };
        public static event Action<CategoryType, GameObject> OnSelectionPointed = delegate { };

        private List<ICheckable> _selections;

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(1);
            _selections = new List<ICheckable>(GameManager_Protoescape.Instance.GetAllCheckables());
        }

        public void Test()
        {
            var result = GetConfusedCategoryCount();

            OnPrototypeTested?.Invoke(result.current, result.total);
        }

        public void PointSelectedCheckable()
        {
            var checkable = _selections[Random.Range(0, _selections.Count)];
            var confusions = checkable.GetConfusions();

            if (confusions.Count > 0)
            {
                var index = Random.Range(0, confusions.Count);
                var key = confusions.Keys.ElementAt(index);
                var value = confusions.Values.ElementAt(index);

                OnSelectionPointed?.Invoke(key, value);
            }
            else
            {
                OnSelectionPointed?.Invoke(CategoryType.None, checkable.gameObject);
            }

            _selections.Remove(checkable);
        }

        private Dictionary<CategoryType, bool> GetConfusionChecklist()
        {
            var confusionChecklist = new Dictionary<CategoryType, bool>()
            {
                { CategoryType.Color, false },
                { CategoryType.Sprite, false },
                { CategoryType.Location, false },
                { CategoryType.Font, false },
                { CategoryType.Highlight, false },
            };

            foreach (var item in _selections)
            {
                var results = item.GetConfusions();

                foreach (var i in results)
                {
                    if (confusionChecklist.ContainsKey(i.Key))
                    {
                        confusionChecklist[i.Key] = true;
                    }
                }
            }

            return confusionChecklist;
        }

        private (int current, int total) GetConfusedCategoryCount()
        {
            int confusionCount = 0;
            var checklist = GetConfusionChecklist();

            foreach (var pair in checklist)
            {
                if (pair.Value)
                {
                    confusionCount++;
                }
            }

            if (!IsConsistent())
            {
                confusionCount++;
            }

            return (confusionCount, checklist.Count + 1);
        }

        private bool IsConsistent()
        {
            var all = _selections;

            foreach (var item in all)
            {
                item.IsChecked = false;
            }

            for (int i = 0; i < all.Count; i++)
            {
                if (all[i].IsChecked)
                {
                    continue;
                }

                var icon = new List<Sprite>();
                var color = new List<Color>();
                var location = new List<int>();
                var font = new List<TMPro.TMP_FontAsset>();

                foreach (var item in all)
                {
                    if (all[i] == item)
                    {
                        continue;
                    }

                    if (all[i].EntityID == item.EntityID)
                    {
                        //color
                        if (all[i].gameObject.GetComponent<IReplaceable<Color>>() != null)
                        {
                            if (color.Count == 0)
                            {
                                color.Add(all[i].gameObject.GetComponent<Icon>().GetColor);
                            }
                            color.Add(item.gameObject.GetComponent<Icon>().GetColor);
                        }
                        //icon
                        if (all[i].gameObject.GetComponent<IReplaceable<Sprite>>() != null)
                        {
                            if (icon.Count == 0)
                            {
                                icon.Add(all[i].gameObject.GetComponent<Icon>().GetSprite);
                            }
                            icon.Add(item.gameObject.GetComponent<Icon>().GetSprite);
                        }
                        //font
                        if (all[i].gameObject.GetComponent<IReplaceable<TMPro.TMP_FontAsset>>() != null)
                        {
                            if (font.Count == 0)
                            {
                                font.Add(all[i].gameObject.GetComponent<TextArea>().GetFont);
                            }
                            font.Add(item.gameObject.GetComponent<TextArea>().GetFont);
                        }
                        //location
                        if (location.Count == 0)
                        {
                            location.Add(all[i].GetSiblingIndex);
                        }
                        location.Add(item.GetSiblingIndex);

                        item.IsChecked = true;
                    }
                }
                all[i].IsChecked = true;

                if (color.GroupBy(x => x).Skip(1).Any() ||
                    icon.GroupBy(x => x).Skip(1).Any() ||
                    font.GroupBy(x => x).Skip(1).Any() ||
                    location.GroupBy(x => x).Skip(1).Any())
                {
                    return false;
                }
            }
            return true;
        }
    }
}