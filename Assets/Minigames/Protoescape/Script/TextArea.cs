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

            foreach (var font in confusingFonts)
            {
                if (textField.font == font)
                {
                    dict.Add(ConfusionType.Font, gameObject);
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