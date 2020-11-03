using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System.Linq;
using UnityEngine.EventSystems;

namespace Methodyca.Minigames.Protoescape
{
    public class TextArea : BaseEntity, IReplaceable<TMP_FontAsset>, ICheckable
    {
        [SerializeField] protected string entityId;
        [SerializeField] private TextMeshProUGUI textField;
        [SerializeField] private int[] likableLocations;
        [SerializeField] private TMP_FontAsset[] likableFonts;

        public string EntityID { get => entityId; }
        public TMP_FontAsset CurrentFont { get => textField.font; }
        public bool IsChecked { get; set; }
        public int CurrentSiblingIndex { get => _rect.GetSiblingIndex(); }
        public string ScreenName { get => _screen.ScreenName; }

        public HashSet<CategoryType> Categories
        {
            get => new HashSet<CategoryType>()
                         {
                            { CategoryType.Position },
                            { CategoryType.Font }
                         };
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            GameManager_Protoescape.SelectedEntity = gameObject;
        }

        public Dictionary<CategoryType, dynamic> GetLikables()
        {
            var dict = new Dictionary<CategoryType, dynamic>();

            if (likableLocations.Contains(CurrentSiblingIndex))
            {
                dict.Add(CategoryType.Position, CurrentSiblingIndex);
            }

            if (likableFonts.Contains(CurrentFont))
            {
                dict.Add(CategoryType.Font, CurrentFont);
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
                    result += $"<b>{category}</b> of {EntityID} at {_screen.ScreenName} screen is <i>liked</i>\n";
                }
                else
                {
                    result += $"<b>{category}</b> of {EntityID} at {_screen.ScreenName} screen is <i>confused</i>\n";
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