using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace Methodyca.Minigames.SortGame
{
    public class Drag : MonoBehaviour, IPointerDownHandler
    , IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerClickHandler
    {
        [SerializeField] private Canvas canvas;

        private RectTransform rectTransform;    //Transfrom of the item selected.
        //public RectTransform begin;
        private CanvasGroup canvasGroup;    //A component needed for the raycast.
        public GameObject box;      //The boxtag either QA or QN for on the table. 
        
        //public GameObject itemsHD;    //The clear vision of the items on the table. 
        
        //The switch
        public string host;     //the child of the box. 
        public GameObject tabledItems;  //the table. 
        public GameObject thisniccur;   //this game object 
        [HideInInspector]public Sprite ontable;
        [HideInInspector]public Sprite inbox;
        [HideInInspector]public Sprite swap;    //the operation that swaps. 

        //Resizer for the sprites both inside and outside. 
        [HideInInspector]public Vector2 inboxSizer;   
        [HideInInspector]public Vector2 onTableShazam;

        //Stored position for the gameobject;
        [HideInInspector]public Vector3 storedRectPos;

        // Sound manager
        SoundManager soundMan;


        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            canvasGroup = GetComponent<CanvasGroup>();
            canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            tabledItems = GameObject.Find("Draggable Items");
            storedRectPos = rectTransform.position;
            soundMan = FindObjectOfType<SoundManager>();
        }
        
        void Start()
        {     
            thisniccur = this.gameObject;
            swap = thisniccur.GetComponent<Image>().sprite;
            swap = ontable; 
        }

        void Update()
        {
            thisniccur.GetComponent<Image>().sprite = swap;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            //Debug.Log("StartDrag");
            canvasGroup.alpha = .7f;//reduced the opacity of the item when selected.
            canvasGroup.blocksRaycasts = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            //Debug.Log("WhileDrag");
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;//make the object retain its postion when selected, and retains the opacity of the item 
            //Debug.Log("Moving");
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            //Debug.Log("EndDrag");
            canvasGroup.alpha = 1f;//restores the opacity of the item after being dropped.
            canvasGroup.blocksRaycasts = true;

            //The return feature of the game items. 
            if(!this.gameObject == box)
            {
                Debug.Log("Back on the table");
                rectTransform.position = storedRectPos;
            }
        }

        // This is redundant - No functionality. Can remove or move OnBeginDrag functionality to it,
        // so as soon as item is clicked it is transparent
        public void OnPointerDown(PointerEventData eventData)
        {
            //Debug.Log("Clicked");  
            soundMan.Play("click");//sound of the game.
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

        }

        public void InsideBox(GameObject host, Vector3 shift)    // ------ how the image swithces inside the box
        {
            
            if(swap = ontable)
            swap = inbox; 
            transform.parent = host.transform;
            this.gameObject.GetComponent<RectTransform>().sizeDelta = inboxSizer;
        
            //this swtiches the images
            GameObject instance = thisniccur;
            Vector3 orderInBox = instance.transform.GetSiblingIndex() * shift;
            instance.GetComponent<RectTransform>().position += orderInBox;

        }

        public void OutsideBox(GameObject parent)   // ------- how the images switches outside the box 
        {
            if(swap = inbox)
            swap = ontable;
            transform.parent = tabledItems.transform;
            this.gameObject.GetComponent<RectTransform>().sizeDelta = onTableShazam;
        }
    }      
}