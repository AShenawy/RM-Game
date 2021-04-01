using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Methodyca.Minigames.Protoescape
{
    public class Icon : BaseEntity, IReplaceable<Sprite>, IReplaceable<Color>, IHighlightable, ICheckable
    {
        [SerializeField] private string entityId;
        [SerializeField] private Image icon;
        [SerializeField] private GameObject highlight;
        [SerializeField] private bool shouldBeHighlighted;
        [SerializeField] private List<Sprite> likableSprites = new List<Sprite>();
        [SerializeField] private List<Color> likableColors = new List<Color>();
        [SerializeField] private List<EntityCoordinate> likableCoordinates = new List<EntityCoordinate>();

        public bool IsHighlighted { get; set; }
        public Color CurrentColor { get; private set; }
        public Sprite CurrentSprite { get; private set; }
        public string EntityID { get => entityId; }
        public EntityCoordinate CurrentCoordinate {
            get => new EntityCoordinate(_transform.GetSiblingIndex(), _stack.CurrentSiblingIndex);
            set { _transform.SetSiblingIndex(value.Horizontal); _stack.transform.SetSiblingIndex(value.Vertical); }}

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

        public HashSet<object> GetCurrentData
        {
            get => new HashSet<object>()
                         {
                            { CurrentSprite },
                            { CurrentColor }
                         };
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

        public void SetLikables()
        {
            highlight.SetActive(IsHighlighted = shouldBeHighlighted);
            CurrentCoordinate = likableCoordinates.GetRandomElement();
            Replace(likableColors[0]);
            Replace(likableSprites[0]);
        }

        public Dictionary<CategoryType, object> GetLikables()
        {
            var result = new Dictionary<CategoryType, object>();

            if (likableCoordinates.Contains(CurrentCoordinate))
                result.Add(CategoryType.Position, CurrentCoordinate);

            if (likableSprites.Contains(CurrentSprite))
                result.Add(CategoryType.Icon, CurrentSprite);

            if (likableColors.Contains(CurrentColor))
                result.Add(CategoryType.Color, CurrentColor);

            if (shouldBeHighlighted == IsHighlighted)
                result.Add(CategoryType.Highlight, IsHighlighted);


            return result;
        }

        public HashSet<CategoryType> GetLikedCategories()
        {
            return GetLikables().Keys.GetHashSet();
        }

        public HashSet<CategoryType> GetConfusedCategories()
        {
            var result = new HashSet<CategoryType>(Categories);
            result.ExceptWith(GetLikables().Keys);

            return result;
        }

        public string GetNotebookLogData()
        {
            var likables = GetLikables();
            string result = "";

            foreach (var category in Categories)
            {
                if (likables.ContainsKey(category))
                {
                    result += $"<b>{category}</b> of {EntityID} at {_stack.ScreenName} screen is <i>liked</i>\n";
                }
                else
                {
                    result += $"<b>{category}</b> of {EntityID} at {_stack.ScreenName} screen is <i>confused</i>\n";
                }
            }

            return result;
        }

        private void Start()
        {
            IsHighlighted = highlight.activeInHierarchy;
            CurrentColor = icon.color;
            CurrentSprite = icon.sprite;
        }
    }
}