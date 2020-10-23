using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Methodyca.Minigames.Protoescape
{
    [RequireComponent(typeof(Image))]
    public class Icon : BaseEntity, IReplaceable<Sprite>, IReplaceable<Color>, IHighlighted, ICheckable
    {
        [SerializeField] private GameObject highlight;
        [SerializeField] private bool isHighlightable = true;
        [SerializeField] private Sprite[] confusingSprites;
        [SerializeField] private Color[] confusingColors;
        [SerializeField] private int[] confusingLocations;

        private Image _image;

        public bool IsHighlighted { get; set; } = false;

        protected override void Awake()
        {
            base.Awake();
            _image = GetComponent<Image>();
        }

        public void Replace(Color value)
        {
            _image.color = value;
        }

        public void Replace(Sprite value)
        {
            _image.sprite = value;
        }

        public void SetHighlight()
        {
            IsHighlighted = !IsHighlighted;
            highlight.SetActive(IsHighlighted);
        }

        public Dictionary<ConfusionType, GameObject> GetConfusions()
        {
            var dict = new Dictionary<ConfusionType, GameObject>();

            foreach (var index in confusingLocations)
            {
                if (CurrentSiblingIndex == index)
                {
                    dict.Add(ConfusionType.Location, gameObject);
                    break;
                }
            }

            foreach (var sprite in confusingSprites)
            {
                if (_image.sprite == sprite)
                {
                    dict.Add(ConfusionType.Sprite, gameObject);
                    break;
                }
            }

            foreach (var color in confusingColors)
            {
                if (_image.color == color)
                {
                    dict.Add(ConfusionType.Color, gameObject);
                    break;
                }
            }

            if (isHighlightable != IsHighlighted)
            {
                dict.Add(ConfusionType.Highlight, gameObject);
            }

            return dict;
        }
    }
}