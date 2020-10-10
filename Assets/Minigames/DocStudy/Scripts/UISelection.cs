using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using TMPro;

namespace Methodyca.Minigames.DocStudy
{
    public class UISelection : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        private const float _scaleDuration = 0.25f;

        [SerializeField] private Image bubble;
        [SerializeField] private TextMeshProUGUI message;

        private UIPost _uiPost;
        private Selection _selection;

        public void Initialize(Selection selection)
        {
            _selection = selection;
            message.text = selection.Text;
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
            bubble.color = Color.yellow;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            bubble.color = Color.white;
        }

        private void Awake()
        {
            _uiPost = GetComponentInParent<UIPost>();
        }
    }
}