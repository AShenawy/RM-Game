using UnityEngine;
using System.Collections.Generic;
using TMPro;

namespace Methodyca.Minigames.Protoescape
{
    public class TextArea : BaseEntity, IReplaceable<TMP_FontAsset>, ICheckable
    {
        [SerializeField] private TextMeshProUGUI textField;
        [SerializeField] private int[] confusingLocations;
        [SerializeField] private TMP_FontAsset[] confusingFonts;

        public TMP_FontAsset GetFont { get => textField.font; }
        public bool IsChecked { get; set; } = false;
        public int GetSiblingIndex { get => _rect.GetSiblingIndex(); }

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

            foreach (var font in confusingFonts)
            {
                if (textField.font == font)
                {
                    dict.Add(CategoryType.Font, gameObject);
                    break;
                }
            }

            return dict;
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