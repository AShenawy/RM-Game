using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Methodyca.Minigames.Protoescape
{
    public class EntityStack : MonoBehaviour, IDragHandler, IDropHandler, ICheckable
    {
        [SerializeField] private Image stackHighlight;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private int[] confusingLocations;

        private RectTransform _rect;
        private RectTransform _rectParent;
        private Image[] _childrenImages;

        private int _currentSiblingIndex { get => _rect.GetSiblingIndex(); }

        private void Awake()
        {
            _rect = GetComponent<RectTransform>();
            _rectParent = _rect.parent as RectTransform;
            _childrenImages = GetComponentsInChildren<Image>();
        }

        private void Start()
        {
            GameManager_Protoescape.OnStackMove += StackMoveHandler;
            stackHighlight.enabled = false;
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

        public Dictionary<ConfusionType, GameObject> GetConfusions()
        {
            var dict = new Dictionary<ConfusionType, GameObject>();

            foreach (var index in confusingLocations)
            {
                if (_currentSiblingIndex == index)
                {
                    dict.Add(ConfusionType.Location, gameObject);
                    break;
                }
            }

            return dict;
        }

        protected virtual void OnDestroy()
        {
            GameManager_Protoescape.OnStackMove -= StackMoveHandler;
        }
    }
}