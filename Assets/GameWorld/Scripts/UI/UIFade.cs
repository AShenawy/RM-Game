using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


namespace Methodyca.Core
{
    // Fades in and out the UI interface on screen
    public class UIFade : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public Image imageToFade;

        public void OnPointerEnter(PointerEventData eventData)
        {
            StartCoroutine(Fader.FadeIn(imageToFade, 0f, 0.4f));
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            StartCoroutine(Fader.FadeOut(imageToFade, 0.4f));
        }
    }
}