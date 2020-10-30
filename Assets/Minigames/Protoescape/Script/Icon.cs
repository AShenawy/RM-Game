using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Methodyca.Minigames.Protoescape
{
    public class Icon : BaseEntity, IReplaceable<Sprite>, IReplaceable<Color>, IHighlightable, ICheckable
    {
        [SerializeField] protected string entityId;
        [SerializeField] private Image icon;
        [SerializeField] private GameObject highlight;
        [SerializeField] private bool shouldBeHighlighted;
        [SerializeField] private List<Sprite> likableSprites = new List<Sprite>();
        [SerializeField] private List<Color> likableColors = new List<Color>();
        [SerializeField] private List<int> likableLocations = new List<int>();

        public bool IsChecked { get; set; }
        public bool IsHighlighted { get; set; }
        public Color CurrentColor { get; private set; }
        public Sprite CurrentSprite { get; private set; }
        public string EntityID { get => entityId; }
        public int CurrentSiblingIndex { get => _rect.GetSiblingIndex(); }
        public string ScreenName { get => _screen.ScreenName; }

        public HashSet<CategoryType> Categories
        {
            get => new HashSet<CategoryType>()
                         {
                            { CategoryType.Position },
                            { CategoryType.Icon },
                            { CategoryType.Color },
                            { CategoryType.Highlight },
                         };
        }

        private void Start()
        {
            IsHighlighted = highlight.activeInHierarchy;
            _screen = GetComponentInParent<ScreenBox>();
        }

        public void Replace(Color value)
        {
            CurrentColor = value;
            icon.color = value;
        }

        public void Replace(Sprite value)
        {
            CurrentSprite = value;
            icon.sprite = value;
        }

        public void SetHighlight()
        {
            IsHighlighted = !IsHighlighted;
            highlight.SetActive(IsHighlighted);
        }

        public Dictionary<CategoryType, dynamic> GetLikables()
        {
            var dict = new Dictionary<CategoryType, dynamic>();

            if (likableLocations.Contains(CurrentSiblingIndex))
            {
                dict.Add(CategoryType.Position, CurrentSiblingIndex);
            }
            if (likableSprites.Contains(CurrentSprite))
            {
                dict.Add(CategoryType.Icon, CurrentSprite);
            }
            if (likableColors.Contains(CurrentColor))
            {
                dict.Add(CategoryType.Color, CurrentColor);
            }
            if (shouldBeHighlighted == IsHighlighted)
            {
                dict.Add(CategoryType.Highlight, IsHighlighted);
            }

            return dict;
        }

        public string GetNotebookLogData()
        {
            var likables = GetLikables();
            string result = "";

            foreach (var category in Categories)
            {
                if (likables.ContainsKey(category))
                {
                    result += $"<b>{category}</b> of {EntityID} at {_screen.ScreenName} screen is <u>liked</u>\n";
                }
                else
                {
                    result += $"<b>{category}</b> of {EntityID} at {_screen.ScreenName} screen is <u>confused</u>\n";
                }
            }

            return result;
        }

    }
}