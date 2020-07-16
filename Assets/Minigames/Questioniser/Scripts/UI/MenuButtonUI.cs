using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Methodyca.Minigames.Questioniser
{
    public class MenuButtonUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] Image buttonImage;
        [SerializeField] Image buttonTextImage;
        [SerializeField] Sprite defaultButtonSprite;
        [SerializeField] Sprite clickedButtonSprite;
        [SerializeField] Sprite regularTextSprite;
        [SerializeField] Sprite hoverTextSprite;

        public void OnPointerDown(PointerEventData eventData)
        {
            buttonImage.sprite = clickedButtonSprite;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            buttonImage.sprite = defaultButtonSprite;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            buttonTextImage.sprite = hoverTextSprite;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            buttonTextImage.sprite = regularTextSprite;
        }
    }
}