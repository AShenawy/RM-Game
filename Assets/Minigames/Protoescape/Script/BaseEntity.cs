using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Methodyca.Minigames.Protoescape
{
    public class BaseEntity : MonoBehaviour, IPointerClickHandler, IDragHandler, IDropHandler
    {
        [SerializeField] protected string entityId;

        protected RectTransform _rect;
        protected RectTransform _rectParent;

        public string EntityID { get => entityId; }
        public int CurrentSiblingIndex { get => _rect.GetSiblingIndex(); }

        protected virtual void Awake()
        {
            _rect = GetComponent<RectTransform>();
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

            int tempIndex = 0;

            if (dragged.transform.IsSiblingOf(_rect))
            {
                tempIndex = _rect.GetSiblingIndex();

                _rect.SetSiblingIndex(dragged.transform.GetSiblingIndex());
                dragged.transform.SetSiblingIndex(tempIndex);

                dragged.transform.localScale = Vector3.zero;
                dragged.transform.DOScale(1, 0.15f);
            }
            else if (dragged.transform.parent.IsSiblingOf(_rect))
            {
                tempIndex = _rect.GetSiblingIndex();

                _rect.SetSiblingIndex(dragged.transform.parent.GetSiblingIndex());
                dragged.transform.parent.SetSiblingIndex(tempIndex);

                dragged.transform.localScale = Vector3.zero;
                dragged.transform.DOScale(1, 0.15f);
            }
            else if (dragged.transform.IsSiblingOf(_rectParent))
            {
                tempIndex = _rectParent.GetSiblingIndex();

                _rectParent.SetSiblingIndex(dragged.transform.GetSiblingIndex());
                dragged.transform.SetSiblingIndex(tempIndex);

                dragged.transform.localScale = Vector3.zero;
                dragged.transform.DOScale(1, 0.15f);
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            GameManager_Protoescape.SelectedEntity = gameObject;
        }
    }
}