using UnityEngine;
using System.Collections.Generic;
using TMPro;

namespace Methodyca.Minigames.Protoescape
{
    public class TextArea : BaseEntity, IReplaceable<TMP_FontAsset>, ICheckable
    {
        [SerializeField] protected string entityId;
        [SerializeField] private TextMeshProUGUI textField;
        [SerializeField] private List<TMP_FontAsset> likableFonts = new List<TMP_FontAsset>();
        [SerializeField] private List<EntityCoordinate> likableCoordinates = new List<EntityCoordinate>();

        public string EntityID { get => entityId; }
        public TMP_FontAsset CurrentFont { get => textField.font; }
        public EntityCoordinate CurrentCoordinate { get => new EntityCoordinate(_transform.GetSiblingIndex(), _stack.CurrentSiblingIndex); }

        public HashSet<CategoryType> Categories
        {
            get => new HashSet<CategoryType>()
                         {
                            { CategoryType.Position },
                            { CategoryType.Font }
                         };
        }
        public HashSet<object> GetCurrentData
        {
            get => new HashSet<object>()
                         {
                            { CurrentCoordinate },
                            { CurrentFont }
                         };
        }
        public Dictionary<CategoryType, object> GetLikables()
        {
            var dict = new Dictionary<CategoryType, object>();

            if (likableCoordinates.Contains(CurrentCoordinate))
            {
                dict.Add(CategoryType.Position, CurrentCoordinate);
            }

            if (likableFonts.Contains(CurrentFont))
            {
                dict.Add(CategoryType.Font, CurrentFont);
            }

            return dict;
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

        public void Replace(TMP_FontAsset value)
        {
            if (textField == null)
            {
                return;
            }

            textField.font = value;
            textField.UpdateFontAsset();
        }
    }
}