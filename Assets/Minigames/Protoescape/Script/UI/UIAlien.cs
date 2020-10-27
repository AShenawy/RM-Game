using UnityEngine;
using UnityEngine.EventSystems;

namespace Methodyca.Minigames.Protoescape
{
    public class UIAlien : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            GameManager_Protoescape.Instance.HandlePrototypeTesting();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {

        }

        public void OnPointerExit(PointerEventData eventData)
        {

        }
    }
}