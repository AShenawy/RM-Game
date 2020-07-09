using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Create namespace for each minigame
namespace Methodyca.Minigames.SortGame
{
    public class Drag : MonoBehaviour, IPointerDownHandler
    , IBeginDragHandler, IEndDragHandler, IDragHandler
    {

        [SerializeField] private Canvas canvas;

        private RectTransform rectTransform;//Transfrom of the item selected.
        private CanvasGroup canvasGroup;//A component needed for the raycast.
        public GameObject box;//The boxtag either QA or QN for on the table. 

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();     
            canvasGroup = GetComponent<CanvasGroup>();
        }
    
        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log("StartDrag");
            canvasGroup.alpha = .7f;//reduced the opacity of the item when selected.
            canvasGroup.blocksRaycasts = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            //Debug.Log("WhileDrag");
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;//make the object retain its postion when selected, and retains the opacity of the item 

        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Debug.Log("EndDrag");
            canvasGroup.alpha = 1f;//restores the opacity of the item after being dropped.
            canvasGroup.blocksRaycasts = true;

            //TakeOutOfBox();

        }

        // This is redundant - No functionality. Can remove or move OnBeginDrag functionality to it,
        // so as soon as item is clicked it is transparent
        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("ClickedDragged");            
        }

        //Method to see what is in dropped in the box.
        public void PutInBox(GameObject boxType)
        {
            box = boxType;
        }

        //Method to see what is taken out of the box.
        public void TakeOutOfBox()
        {
            if(box !=null)
            {
                box.GetComponent<DragSlot>().Remove(gameObject);
            }            
        }
    }      
}