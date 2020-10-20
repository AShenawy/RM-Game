using UnityEngine;
using UnityEngine.EventSystems;

namespace Methodyca.Minigames.Protoescape
{
    public class UIHighlightTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private GameObject highlightedPanel;

        public void OnPointerEnter(PointerEventData eventData)
        {
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