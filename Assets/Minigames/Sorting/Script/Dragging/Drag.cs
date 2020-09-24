using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace Methodyca.Minigames.SortGame
{
    // script handles the behaviour of draggable items on UI canvas
    public class Drag : MonoBehaviour, IPointerDownHandler,
                        IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        [SerializeField] private Canvas dragCanvas;

        //******* not the 'item selected' but the game object this script is on
        private RectTransform rectTransform;    //Transfrom of the item selected    

        /*************************************************************************************************************************
        *   CanvasGroup is redunant since this is useful for groups of parent/child relation. in this scenario the draggable items:
        *   1) don't have children
        *   2) can do same functionality from the already included Image component
        *
        private CanvasGroup canvasGroup;    //A component needed for the raycast.   ******/
        
        public GameObject box;      //The boxtag either QA or QN for on the table. 

        //public GameObject itemsHD;    //The clear vision of the items on the table.       //********** unused. removed *************

        //The switch
        //public string host;     //the child of the box.       //********** unused. removed *************

        [SerializeField]private GameObject onTableItemsContainer;  // Container holding items on table 

        /*************************************************************************************************************************
         *  no need to save a reference to the same game object the script is already on. Just use GetComponent()
        public GameObject thisniccur;   //this game object        //******** no longer needed ***********/

        /*************************************************************************************************************************
         * you shouldn't make a variable public, store a value in it, then hide it using [HideInInspector]
         * How are you going to know if there was a value assigned to it in inspector or from another class?
         * The purpose of [HideInInspector] is to make a variabel accesible through code from other scripts,
         * while not allowing it to be set in inspector, not to cover up your tracks and leave the next person wondering what's going on
        [HideInInspector]public Sprite ontable;          ******/
        
        public Sprite onTableSprite;
        public Sprite insideBoxSpriteLeft;      // inside box image pointing left
        public Sprite insideBoxSpriteRight;     // inside box image pointing right
        //public Sprite swap;    //the operation that swaps.        //******** no longer needed ***********
        
        [Range(0, 1)]public float onDragOpacity = 0.7f;     // Make object transparent while being dragged

        //Resizer for the sprites both inside and outside       //***** these dimensions should be set up automatically through code, and not by hand by trial-error. Check end of Awake()
        private Vector2 inBoxImageDimensions;       
        private Vector2 onTableImageDimensions;

        //Stored position for the gameobject;
        private Vector2 startPosOnTable;

        Image image;
        SoundManager soundMan;


        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            //canvasGroup = GetComponent<CanvasGroup>();    //******** not used anymore  ***********
            //canvas = GameObject.Find("Canvas").GetComponent<Canvas>();        //******* redundant since it's set in inspector *********
            //table = GameObject.Find("Draggable Items");       //********* same as above *********
            startPosOnTable = rectTransform.anchoredPosition;
            image = GetComponent<Image>();
            soundMan = FindObjectOfType<SoundManager>();

            // Set up dimensions based on texture sizes
            // ********* We take advantage that rect transform uses width and height just like the image information.
            // ********* And if set up correctly, image data can be used to resize the rect transform like so:
            onTableImageDimensions = new Vector2(onTableSprite.texture.width, onTableSprite.texture.height);
            inBoxImageDimensions = new Vector2(insideBoxSpriteLeft.texture.width, insideBoxSpriteLeft.texture.height);
        }
        
        void Start()
        {     
            //thisniccur = this.gameObject;           // *************  redundant  *******************
            
            /****************** can use GetComponent() right away without recursive referencing by usign thisniccur  *************
            swap = thisniccur.GetComponent<Image>().sprite;     
            swap = GetComponent<Image>().sprite;  ******** switched with direct reference to Image component *******/

            // reset object at game start
            ResetItemGraphic();
            ResetItemPosition();
        }

        void Update()
        {
            //********* simply use GetComponent(), and why do you get it from thisniccur in Start() only to keep reassigning it in update?? *********
            //thisniccur.GetComponent<Image>().sprite = swap;     //********* switched with direct reference to Image component **********
        }

        // This is redundant - No functionality. Can remove or move OnBeginDrag functionality to it,
        // so as soon as item is clicked it is transparent
        public void OnPointerDown(PointerEventData eventData)
        {
            //Debug.Log("Clicked");  
            //soundMan.Play("click");     //sound of the game.      //****** moved to OnEndDrag and another to new DisplayEnlarged script
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            //Debug.Log("StartDrag");
            //canvasGroup.alpha = .7f;        //reduced the opacity of the item when selected. *********** moved to Image component's alpha
            //canvasGroup.blocksRaycasts = false;     // ***************** moved to disabling RaycastTarget property on Image component

            // move object to drag canvas so it's viewed on top of other elements
            transform.parent = dragCanvas.transform;

            // change object opacity when starting to drag it
            image.color = new Color(image.color.r, image.color.g, image.color.b, onDragOpacity);
            
            // disable raycasting so that object is not blocking raycast of elements under it (e.g. box/table to drop on to)
            image.raycastTarget = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            //Debug.Log("WhileDrag");

            // ensure dragging is relative to current screen size so item doesn't 'drift' off from cursor 
            rectTransform.anchoredPosition += eventData.delta / dragCanvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            //Debug.Log("EndDrag");

            //restores the opacity of the item after being dropped. //********** canvas group is switched out with image.color and image.raycastTarget
            //canvasGroup.alpha = 1f;
            //canvasGroup.blocksRaycasts = true;

            //transform.parent = onTableItemsContainer.transform;

            // return opacity to full on dropping the object
            image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);

            // re-enable raycasting after object is dropped to be able to click it again
            image.raycastTarget = true;

            //The return feature of the game items. 
            //if (!this.gameObject == box)      //******** this doesn't work. Will always return true because this gameObject is and will never be the box game object  ************
            if (box == null)
            {
                // restore original parent so reset position works properly
                transform.parent = onTableItemsContainer.transform;
                ResetItemPosition();
            }

            if (soundMan)
                soundMan.Play("click");
        }

        public void ResetItemGraphic()
        {
            image.sprite = onTableSprite;
        }

        public void ResetItemPosition()
        {
            rectTransform.anchoredPosition = startPosOnTable;
        }

        //Method to see what is in dropped in the box.      //******* moved line to GoIntoBox
        //public void PutInBox(GameObject boxType)
        //{
        //    box = boxType;
        //}

        //Method to see what is taken out of the box.       //******* Unused method. Removed **********
        //public void TakeOutOfBox()
        //{
        //    if(box !=null)
        //    {
        //        box.GetComponent<DragSlot>().Remove(gameObject);
        //    }            
        //}

        public void GoIntoBox(GameObject boxType, GameObject boxPlaceRef, Vector2 shift)    // ------ how the image swithces inside the box
        {
            /******************************** were all 3 lines meant to be within if statement? Also, the condition test isn't properly typed
            if (swap = ontable)
            swap = inbox; 
            transform.parent = host.transform;
            this.gameObject.GetComponent<RectTransform>().sizeDelta = inboxSizer;
            */
            box = boxType;

            if (boxPlaceRef.CompareTag("Right Box"))
                image.sprite = insideBoxSpriteRight;
            else if (boxPlaceRef.CompareTag("Left Box"))
                image.sprite = insideBoxSpriteLeft;

            transform.parent = boxPlaceRef.transform;
            
            //gameObject.GetComponent<RectTransform>().sizeDelta = inboxSizer;  //******** just use the variable you already set *********
            rectTransform.sizeDelta = inBoxImageDimensions;
        
            //this swtiches the images
            //GameObject instance = thisniccur;     //***** don't need yet another reference to the same game object

            // set the positioning of the object in the box based on its place in hierarchy
            //Vector3 orderInBox = instance.transform.GetSiblingIndex() * shift;    //****** simply take the value directly from game object
            Vector2 inBoxPos = gameObject.transform.GetSiblingIndex() * shift;
            
            //rectTransform.position += orderInBox;     //******** using += adds to existing value. We want to set a new position value. This probably what cause object to fly all around the scene **************
            rectTransform.anchoredPosition = inBoxPos;
        }

        public void ReturnOriginalLocation(GameObject table)   // ------- how the images switches outside the box 
        {
            /******** were all 3 lines meant to be within if statement?
             * Also, the condition test isn't properly typed: if (swap = inbox) is not checking if the values are similar
             * It's checking 'can I set the value of swap to be inbox?' and actually does set it and gives true then goes into the if statement
             * So, it will always return true. There's no real conidition check
             * 
            if (swap = inbox)
                swap = ontable;
            
            transform.parent = tabledItems.transform;
            this.gameObject.GetComponent<RectTransform>().sizeDelta = onTableShazam;
            */
            image.sprite = onTableSprite;
            rectTransform.sizeDelta = onTableImageDimensions;

            transform.parent = table.transform;
            ResetItemPosition();            //***************************   this is necessary to have the item return to its original place ************
            box = null;                     //************************ should empty the variable again, return it to its original state, to avoid unwanted behaviour ***********
        }
    }      
}