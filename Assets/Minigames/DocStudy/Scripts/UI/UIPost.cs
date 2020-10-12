using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using TMPro;

namespace Methodyca.Minigames.DocStudy
{
    public class UIPost : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        private const float _scaleDuration = 0.1f;

        [SerializeField] private RectTransform messagePanel;
        [SerializeField] private TextMeshProUGUI message;

        private Image _panel;
        private Post _selection;
        private UIPostPanel _uiPost;
        private readonly Vector2 _messagePanelPaddingSize = new Vector2(0, 10);

        public void Initialize(Post selection)
        {
            _selection = selection;

            message.text = $"<b>{selection.Name}: </b>{selection.Message}";
            message.ForceMeshUpdate();

            Vector2 textSize = message.GetRenderedValues();
            messagePanel.sizeDelta = new Vector2(messagePanel.sizeDelta.x, textSize.y) + _messagePanelPaddingSize;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_selection.IsSelected)
            {
                DOTween.Sequence().Append(transform.DOScale(0, _scaleDuration))
                                  .AppendCallback(() => _uiPost.Discard(this))
                                  .Append(transform.DOScale(1, _scaleDuration));
            }
            else
            {
                DOTween.Sequence().Append(transform.DOScale(0, _scaleDuration))
                                  .AppendCallback(() => _uiPost.Select(this))
                                  .Append(transform.DOScale(1, _scaleDuration));
            }

            _selection.IsSelected = !_selection.IsSelected;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _panel.color = Color.yellow;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _panel.color = Color.white;
        }

        private void Awake()
        {
            _panel = messagePanel.GetComponent<Image>();
            _uiPost = GetComponentInParent<UIPostPanel>();
        }
    }
}