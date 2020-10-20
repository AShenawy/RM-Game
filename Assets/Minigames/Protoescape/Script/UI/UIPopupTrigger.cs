using UnityEngine;
using UnityEngine.EventSystems;

namespace Methodyca.Minigames.Protoescape
{
    public class UIPopupTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IEndDragHandler
    {
        [SerializeField] private GameObject highlightedPanel;

        private void Start()
        {
            highlightedPanel.SetActive(false);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            highlightedPanel.SetActive(false);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (eventData.pointerDrag == gameObject)
            {
                highlightedPanel.SetActive(true);
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (eventData.dragging)
            {
                return;
            }

            highlightedPanel.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (eventData.pointerEnter != highlightedPanel || eventData.pointerEnter != gameObject)
            {
                highlightedPanel.SetActive(false);
            }
        }
    }
}