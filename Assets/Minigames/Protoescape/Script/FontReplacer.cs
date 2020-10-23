using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Methodyca.Minigames.Protoescape
{
    public class FontReplacer : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private TMP_FontAsset fontAsset;

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

            if (_replaceable.GetComponent<IReplaceable<TMP_FontAsset>>() != null)
            {
                _replaceable.GetComponent<IReplaceable<TMP_FontAsset>>().Replace(fontAsset);
            }
        }
    }
}