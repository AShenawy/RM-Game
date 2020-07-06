using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Create namespace for each minigame
namespace Methodyca.Minigames.SortGame
{
    // Fix class name typo and capitalisation
    public class Drag : MonoBehaviour, IPointerDownHandler
    , IBeginDragHandler, IEndDragHandler, IDragHandler
    {

        [SerializeField] private Canvas canvas;

        private RectTransform rectTransform;
        private CanvasGroup canvasGroup;

        //public GameObject this_box.set;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            canvasGroup = GetComponent<CanvasGroup>();
            //this_box = GameObject.SetActive(false);
        }


        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log("StartDrag");
            canvasGroup.alpha = .7f;
            canvasGroup.blocksRaycasts = false;

        }

        public void OnDrag(PointerEventData eventData)
        {
            Debug.Log("WhileDrag");
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;

        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Debug.Log("EndDrag");
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;

        }

        // This is redundant - No functionality. Can remove or move OnBeginDrag functionality to it,
        // so as soon as item is clicked it is transparent
        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("ClickedDragged");
            
            

        }

    }
}