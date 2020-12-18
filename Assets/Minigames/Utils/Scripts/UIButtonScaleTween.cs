using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

namespace Methodyca.Minigames.Utils
{
    public class UIButtonScaleTween : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private float tweenDuration = 0.15f;
        [SerializeField] private float minScaleSize = 0.95f;
        [SerializeField] private float maxScaleSize = 1.05f;

        private RectTransform _transform;

        private void Awake()
        {
            _transform = GetComponent<RectTransform>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _transform.DOScale(minScaleSize, tweenDuration);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _transform.DOScale(1, tweenDuration);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _transform.DOScale(maxScaleSize, tweenDuration);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _transform.DOScale(1, tweenDuration);
        }
    }
}