using UnityEngine;
using TMPro;

namespace Methodyca.Minigames.Protoescape
{
    public class TextArea : BaseEntity, IReplaceable<TMP_FontAsset>
    {
        [SerializeField] private TextMeshProUGUI textField;

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