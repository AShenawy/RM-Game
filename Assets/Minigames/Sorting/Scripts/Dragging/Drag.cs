using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace Methodyca.Minigames.SortGame
{
    // script handles the behaviour of draggable items on UI canvas
    public class Drag : MonoBehaviour, IBeginDragHandler,
                        IEndDragHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Canvas dragCanvas;

        private RectTransform rectTransform;    //Transfrom of the item selected    

        public GameObject box;      // the box the item is sitting in
        [Tooltip("How far up would the item slide up when the pointer hovers over it, while it's in a box"), Range(0, 100)]
        public int peekUpDistance = 15;

        [SerializeField]private GameObject onTableItemsContainer;  // Container holding items on table 
        
        public Sprite onTableSprite;
        public Sprite insideBoxSpriteLeft;      // inside box image pointing left
        public Sprite insideBoxSpriteRight;     // inside box image pointing right
        
        [Range(0, 1)]public float onDragOpacity = 0.7f;     // Make object transparent while being dragged
        public Sound dragSFX;

        private Vector2 inBoxImageDimensions;       
        private Vector2 onTableImageDimensions;

        //Stored position for the gameobject
        private Vector2 startPosOnTable;

        private Image image;
        private SortingManager sortMan;


        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            startPosOnTable = rectTransform.anchoredPosition;
            image = GetComponent<Image>();

            // Set up dimensions based on texture sizes
            // ********* We take advantage that rect transform uses width and height just like the image information.
            // ********* And if set up correctly, image data can be used to resize the rect transform like so:
            onTableImageDimensions = new Vector2(onTableSprite.texture.width, onTableSprite.texture.height);
            inBoxImageDimensions = new Vector2(insideBoxSpriteLeft.texture.width, insideBoxSpriteLeft.texture.height);
        }
        
        void Start()
        {
            sortMan = FindObjectOfType<SortingManager>();

            // reset object at game start
            ResetItemGraphic();
            ResetItemPosition();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (box)
                rectTransform.anchoredPosition += new Vector2(0, peekUpDistance);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (box)
                rectTransform.anchoredPosition -= new Vector2(0, peekUpDistance);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            // move object to drag canvas so it's viewed on top of other elements
            rectTransform.SetParent(dragCanvas.transform, true);

            // change object opacity when starting to drag it
            image.color = new Color(image.color.r, image.color.g, image.color.b, onDragOpacity);
            
            // disable raycasting so that object is not blocking raycast of elements under it (e.g. box/table to drop on to)
            image.raycastTarget = false;

            // prevent UI buttons from blocking raycasts, essentially making items float on them
            sortMan.EnableButtons(false);

            SoundManager.instance.PlaySFX(dragSFX);
        }

        public void OnDrag(PointerEventData eventData)
        {
            // ensure dragging is relative to current screen size so item doesn't 'drift' off from cursor 
            rectTransform.anchoredPosition += eventData.delta / dragCanvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            // return opacity to full on dropping the object
            image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);

            // re-enable raycasting after object is dropped to be able to click it again
            image.raycastTarget = true;

            

            // re-allow UI buttons to work again
            sortMan.EnableButtons(true);
        }

        void ResetItemGraphic()
        {
            image.sprite = onTableSprite;
        }

        void ResetItemPosition()
        {
            rectTransform.anchoredPosition = startPosOnTable;
        }

        public void GoIntoBox(GameObject sortingBox, GameObject boxPlaceRef, Vector2 shift)    // ------ how the image swithces inside the box
        {
            box = sortingBox;

            if (boxPlaceRef.CompareTag("Right Box"))
                image.sprite = insideBoxSpriteRight;
            else if (boxPlaceRef.CompareTag("Left Box"))
                image.sprite = insideBoxSpriteLeft;

            rectTransform.SetParent(boxPlaceRef.transform, false);
            
            rectTransform.sizeDelta = inBoxImageDimensions;
        
            // set the positioning of the object in the box based on its place in hierarchy
            Vector2 inBoxPos = gameObject.transform.GetSiblingIndex() * shift;
            
            rectTransform.anchoredPosition = inBoxPos;
        }

        public void ReturnOriginalLocation()   // ------- how the images switches outside the box 
        {
            ResetItemGraphic();
            rectTransform.sizeDelta = onTableImageDimensions;

            rectTransform.SetParent(onTableItemsContainer.transform, false);
            ResetItemPosition();
            box = null;
        }
    }      
}