using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Methodyca.Minigames.Protoescape
{
    public class FontReplacer : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private TMP_FontAsset fontAsset;
        [SerializeField] private string displayTag;
        [TextArea(1, 3), SerializeField] private string displayText;

        private TextMeshProUGUI _tmpText;
        private GameObject _replaceable;

        private void Awake()
        {
            _tmpText = GetComponent<TextMeshProUGUI>();
            _tmpText.font = fontAsset;
            _tmpText.UpdateFontAsset();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _replaceable = GameManager_Protoescape.SelectedEntity;

            var iReplaceable = _replaceable.GetComponent<IReplaceable<TMP_FontAsset>>();

            if (iReplaceable != null)
            {
                iReplaceable.Replace(fontAsset);

                if (_replaceable.GetComponent<ICheckable>().EntityID == "Name Tag")
                {
                    iReplaceable.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = displayTag;
                }
                else
                {
                    iReplaceable.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = displayText;
                }
            }
        }
    }
}