using UnityEngine;
using UnityEngine.EventSystems;

namespace Methodyca.Minigames.Protoescape
{
    public class UIPopupTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IEndDragHandler
    {
        [SerializeField] private bool setAsLastSiblingOnPointerEnter;
        [SerializeField] private GameObject popup;

        private void Start()
        {
            popup.SetActive(false);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            popup.SetActive(false);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (eventData.pointerEnter == gameObject)
            {
                popup.SetActive(true);
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (eventData.dragging)
            {
                return;
            }
            if (setAsLastSiblingOnPointerEnter)
            {
                transform.SetAsLastSibling();
            }

            popup.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (eventData.pointerEnter != popup || eventData.pointerEnter != gameObject)
            {
                popup.SetActive(false);
            }
        }

        private void OnDisable()
        {
            popup.SetActive(false);
        }
    }
}