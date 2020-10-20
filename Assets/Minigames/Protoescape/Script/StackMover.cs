using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Methodyca.Minigames.Protoescape
{
    public class StackMover : MonoBehaviour, IDragHandler, IDropHandler, IEntity
    {
        [SerializeField] private Image stackHighlight;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private int correctSiblingIndex = 0;

        protected Canvas _canvas;
        protected RectTransform _rect;
        protected RectTransform _rectParent;
        protected Image[] _childrenImages;

        public int CurrentSiblingIndex { get => _rect.GetSiblingIndex(); }
        public int CorrectSiblingIndex { get => correctSiblingIndex; }

        protected virtual void Awake()
        {
            _rect = GetComponent<RectTransform>();
            _canvas = GetComponentInParent<Canvas>();
            _rectParent = _rect.parent as RectTransform;
            _childrenImages = GetComponentsInChildren<Image>();
        }

        protected virtual void Start()
        {
            GameManager_Protoescape.OnStackMove += StackMoveHandler;
        }

        private void StackMoveHandler(bool value)
        {
            foreach (var item in _childrenImages)
            {
                item.raycastTarget = !value;
            }

            if (value)
            {
                canvasGroup.alpha = 0.5f;
            }
            else
            {
                canvasGroup.alpha = 1;
            }

            stackHighlight.enabled = stackHighlight.raycastTarget = value;
        }

        public void OnDrag(PointerEventData eventData) { }

        public void OnDrop(PointerEventData eventData)
        {
            var dragged = eventData.pointerDrag;

            if (dragged == null || dragged == gameObject || !GameManager_Protoescape.IsStacksMovable)
            {
                return;
            }

            if (dragged.transform.IsChildOf(_rectParent))
            {
                int index = _rect.GetSiblingIndex();

                _rect.SetSiblingIndex(dragged.transform.GetSiblingIndex());
                dragged.transform.SetSiblingIndex(index);
            }
        }

        protected virtual void OnDestroy()
        {
            GameManager_Protoescape.OnStackMove -= StackMoveHandler;
        }
    }
}