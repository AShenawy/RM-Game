using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Methodyca.Minigames.Protoescape
{
    public class BaseEntity : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IDropHandler
    {
        public bool IsEmptySpot;
        public bool IsVerticallySwapable;

        protected Transform _transform;
        protected ScreenBox _screen;

        protected virtual void Awake()
        {
            _transform = transform;
            _screen = GetComponentInParent<ScreenBox>();
        }

        public void OnDrag(PointerEventData eventData) { }

        public void OnDrop(PointerEventData eventData)
        {
            var dragged = eventData.pointerDrag;
            var draggedEntity = dragged.GetComponent<BaseEntity>();

            if (draggedEntity.IsEmptySpot || dragged == null || dragged == gameObject || GameManager_Protoescape.IsStacksMovable)
            {
                return;
            }

            var stack = GetComponentInParent<EntityStack>();
            var draggedStack = dragged.GetComponentInParent<EntityStack>();

            if (stack == draggedStack)
            {
                if (dragged.transform.IsSiblingOf(transform))
                {
                    var dropSiblingIndex = _transform.GetSiblingIndex();
                    var dragSiblingIndex = dragged.transform.GetSiblingIndex();

                    _transform.SetSiblingIndex(dragSiblingIndex);
                    dragged.transform.SetSiblingIndex(dropSiblingIndex);
                }
                else if (dragged.transform.parent.IsSiblingOf(_transform)) //Icon to text area
                {
                    var dropSiblingIndex = _transform.GetSiblingIndex();
                    var dragSiblingIndex = dragged.transform.parent.GetSiblingIndex();

                    _transform.SetSiblingIndex(dragSiblingIndex);
                    dragged.transform.parent.SetSiblingIndex(dropSiblingIndex);
                }
                else if (dragged.transform.IsSiblingOf(_transform.parent)) //Text area to icon
                {
                    var dropSiblingIndex = _transform.parent.GetSiblingIndex();
                    var dragSiblingIndex = dragged.transform.GetSiblingIndex();

                    _transform.parent.SetSiblingIndex(dragSiblingIndex);
                    dragged.transform.SetSiblingIndex(dropSiblingIndex);
                }

                dragged.transform.DOShakeScale(duration: 0.2f, strength: 0.5f, vibrato: 5, fadeOut: false);
            }
            else
            {
                if ((!draggedEntity.IsVerticallySwapable && !draggedEntity.IsEmptySpot) || (draggedEntity.IsVerticallySwapable && !IsVerticallySwapable && !IsEmptySpot))
                {
                    var dropSiblingIndex = stack.transform.GetSiblingIndex();
                    var dragSiblingIndex = draggedStack.transform.GetSiblingIndex();

                    stack.transform.SetSiblingIndex(dragSiblingIndex);
                    draggedStack.transform.SetSiblingIndex(dropSiblingIndex);
                }
                else if (draggedEntity.IsVerticallySwapable && (IsVerticallySwapable || IsEmptySpot))
                {
                    var dropParent = _transform.parent;
                    var dropSiblingIndex = _transform.GetSiblingIndex();

                    var dragParent = dragged.transform.parent;
                    var dragSiblingIndex = dragged.transform.GetSiblingIndex();

                    _transform.SetParent(dragParent);
                    _transform.SetSiblingIndex(dragSiblingIndex);

                    dragged.transform.SetParent(dropParent);
                    dragged.transform.SetSiblingIndex(dropSiblingIndex);

                    dragged.transform.DOShakeScale(duration: 0.2f, strength: 0.5f, vibrato: 5, fadeOut: false);
                }
            }
        }

        public virtual void OnPointerClick(PointerEventData eventData) { }

        public void OnBeginDrag(PointerEventData eventData)
        {
            GameManager_Protoescape.SelectedEntity = null;
        }
    }
}