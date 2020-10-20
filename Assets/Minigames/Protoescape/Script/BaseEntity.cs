using UnityEngine;
using UnityEngine.EventSystems;

namespace Methodyca.Minigames.Protoescape
{
    public class BaseStack : MonoBehaviour, IDragHandler, IDropHandler
    {
        protected Canvas _canvas;
        protected RectTransform _rect;
        protected RectTransform _rectParent;

        protected virtual void Awake()
        {
            _rect = GetComponent<RectTransform>();
            _canvas = GetComponentInParent<Canvas>();
            _rectParent = _rect.parent as RectTransform;
        }

        protected virtual void Start()
        {
            GameManager_Protoescape.OnStackMove += StackMoveHandler;
        }

        private void StackMoveHandler(bool value)
        {
            // On stack Move
        }

        public void OnDrag(PointerEventData eventData) { }

        public void OnDrop(PointerEventData eventData)
        {
            var dragged = eventData.pointerDrag;

            if (dragged == null || dragged == gameObject || GameManager_Protoescape.IsStacksMovable)
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

    public class BaseEntity : MonoBehaviour, IPointerClickHandler, IDragHandler, IDropHandler
    {
        protected Canvas _canvas;
        protected RectTransform _rect;
        protected RectTransform _rectParent;

        protected virtual void Awake()
        {
            _rect = GetComponent<RectTransform>();
            _canvas = GetComponentInParent<Canvas>();
            _rectParent = _rect.parent as RectTransform;
        }

        public void OnDrag(PointerEventData eventData) { }

        public void OnDrop(PointerEventData eventData)
        {
            var dragged = eventData.pointerDrag;

            if (dragged == null || dragged == gameObject || GameManager_Protoescape.IsStacksMovable)
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

        public void OnPointerClick(PointerEventData eventData)
        {
            GameManager_Protoescape.SelectedEntity = gameObject;
        }
    }
}