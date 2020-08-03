using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Threading;

// Create namespace for each minigame
namespace Methodyca.Minigames.SortGame
{
    public class Drag : MonoBehaviour, IPointerDownHandler
    , IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerClickHandler
    {

        [SerializeField] private Canvas canvas;

        private RectTransform rectTransform;//Transfrom of the item selected.
        private CanvasGroup canvasGroup;//A component needed for the raycast.
        public GameObject box;//The boxtag either QA or QN for on the table. 
        //public GameObject itemsHD;//The clear vision of the items on the table. 

        public GameObject prefab;
        public GameObject parent;
        bool turnOn;// for the iteas to turn on. 

        //Float created for the double click function. 
        private float lastClickTime;
        private const float doubleClickTime = .25f;

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
            //Debug.Log("Clicked");  
            FindObjectOfType<SoundManager>().Play("click");//sound of the game.
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
        //Method On Pointer click to add double click function.
        public void OnPointerClick(PointerEventData eventData)
        {
            // if (eventData.pointerEnter)
            // {
            //     float timeSince = Time.time - lastClickTime;
            //     if (timeSince <= doubleClickTime)
            //     {
            //        //itemsHD.SetActive(!turnOn);//double click.
            //        Debug.Log("Image Turning On - Double Click");
                   
            //     }           
            //     else
            //     {
            //        //itemsHD.SetActive(turnOn);//turn on the image.
            //        Debug.Log("Off Image");
            //     }          
            //     lastClickTime= Time.time;

            // }

        }
        public void insideBox(GameObject boxType)
        {
            //this swtiches the images
            Instantiate(prefab,parent.transform);
            
            //prefab.GetComponent<CanvasGroup>()=canvasGroup;
            this.gameObject.SetActive(turnOn);
            Debug.Log("Appear inside the box");
        }
        
    }      
}