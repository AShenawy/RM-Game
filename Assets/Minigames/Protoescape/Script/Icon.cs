using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Methodyca.Minigames.Protoescape
{
    public class Icon : BaseEntity, IReplaceable<Sprite>, IReplaceable<Color>, IHighlighted, ICheckable
    {
        [SerializeField] private Image icon;
        [SerializeField] private GameObject highlight;
        [SerializeField] private bool shouldBeHighlighted;
        [SerializeField] private Sprite[] confusingSprites;
        [SerializeField] private Color[] confusingColors;
        [SerializeField] private int[] confusingLocations;

        public Color GetColor { get => icon.color; }
        public Sprite GetSprite { get => icon.sprite; }
        public bool IsHighlighted { get; set; }
        public bool IsChecked { get; set; } = false;
        public int GetSiblingIndex { get => _rect.GetSiblingIndex(); }

        private void Start()
        {
            IsHighlighted = highlight.activeInHierarchy;
        }

        public void Replace(Color value)
        {
            icon.color = value;
        }

        public void Replace(Sprite value)
        {
            icon.sprite = value;
        }

        public void SetHighlight()
        {
            IsHighlighted = !IsHighlighted;
            highlight.SetActive(IsHighlighted);
        }

        public Dictionary<CategoryType, GameObject> GetConfusions()
        {
            var dict = new Dictionary<CategoryType, GameObject>();

            foreach (var index in confusingLocations)
            {
                if (CurrentSiblingIndex == index)
                {
                    dict.Add(CategoryType.Location, gameObject);
                    break;
                }
            }

            foreach (var sprite in confusingSprites)
            {
                if (icon.sprite == sprite)
                {
                    dict.Add(CategoryType.Sprite, gameObject);
                    break;
                }
            }

            foreach (var color in confusingColors)
            {
                if (icon.color == color)
                {
                    dict.Add(CategoryType.Color, gameObject);
                    break;
                }
            }

            if (shouldBeHighlighted != IsHighlighted)
            {
                dict.Add(CategoryType.Highlight, gameObject);
            }

            return dict;
        }
    }
}