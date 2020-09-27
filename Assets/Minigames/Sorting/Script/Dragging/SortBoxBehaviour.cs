using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace Methodyca.Minigames.SortGame
{
    // this script handles the QL/QN boxes and placement of items in them
    public class SortBoxBehaviour : MonoBehaviour, IDropHandler
    {
        // even will be called whenever a correct item is placed in the box
        public delegate void ItemDropped(int itemCount);
        public event ItemDropped onItemDropped;

        //public int points = 0;                  //******** better to move this to GameManager/SortingManager script and leave the box do box stuff
        //private int goal = 5;                   //***************** same as above
        public SortingManager gameManager;
        public SortBoxBehaviour otherSortBox;
        
        //public GameObject crystalStation;       //The charging station either pink or blue.
        //public Image crystalSprite;             //************** box shouldn't control crystal. moved to Crystal ***************
        public GameObject placementReference;      // the parent of the items placed in the box
        
        /************************************************
         * this listBox isn't needed because it's only used to count how many items are in it. The items in the list aren't accessed or used.
         * In that case, a number is all we need: requiredItemsInBox does that.
        public GameObject[] listBox;          //the box to show how its removed.        */
        //public int requiredItemsInBox = 5;          // Condition for how many items should be placed in box to win //********* not used. removed
        
        //public GameObject glow;                 //the halo effect   //************** box shouldn't control crystal. moved to Crystal ***************
        //private RectTransform anchored;         //the position of the snapping.       //************* unused. removed ***************

        //The levitation for the crytsals.      //************** box shouldn't control crystal. moved to CrystalCharger ***************
        //public RectTransform levitate;          //for the floating crystals.
        //public RectTransform shinnny;           //stored position for the rect transfrom.  
        //public Vector3 temPos;

        //inputs for the levitations            //************** box shouldn't control crystal. moved to CrystalCharger ***************
        //public float amp;

        //public Sprite[] crystalPhases;          //The array of crystals charging.   //************** box shouldn't control crystal. moved to CrystalCharger ***************

        public string acceptableItemTag;                  //The tag name for the boxes in the game either QA or QN.
        private int correctItemsInBoxCount = 0;    //****** how many correct items are currently in the box

        //bools for the game manager. 
        //public bool sorted;
        //public bool done;
        //public bool almost;
        
        public List <GameObject> inTheBox = new List<GameObject>();     //The list for items dropped.

        public Vector2 itemPlacementShift;       //the distance between the items in box,for spacing. 

        public AudioClip onItemPlacedSFX;
        SoundManager soundMan;
      

        private void Start()
        {
            //anchored = GetComponent<RectTransform>();     //********* unused. removed
            //glow.SetActive(false);                                    //******** functionality moved to Crystal
            //shinnny = crystalStation.GetComponent<RectTransform>();       //******** functionality moved to CrystalCharger
            soundMan = FindObjectOfType<SoundManager>();
        }

        void Update()
        {
            //if (points >= 1) { }
                // Rise();      //***** moved to CrystalCharger ****
        }

        //Mouse released. 
        public void OnDrop(PointerEventData eventData) 
        {
            // A check to see if the objects are placed in the box, represented with charging crystals and to snap items to the box.
            //if (crystalStation == null)           //********* since now crystal functionality is no longer controlled by box (moved to CrystalCharger), box doesn't need to do this check. CrystalCharger can check itself ********
            //    return;

            if (eventData.pointerDrag.GetComponent<Drag>() == null)
                return;

            //Sound Effect
            //soundMan.Imaging("paper_hit");
            //soundMan.Play("paper_hit");     //sound of the game.
            soundMan.PlaySFX(onItemPlacedSFX, true);


            //This is basically to define the things on the table and, make them snap to the box when clicked. 
            //RectTransform thingOnTheTable;      //Anchored position. 
            //thingOnTheTable = eventData.pointerDrag;
            GameObject droppedItem = eventData.pointerDrag; ;      //******* item dropped on the box. 2 steps above merged into one line ********

            if (droppedItem.GetComponent<Drag>().box != gameObject)
                otherSortBox.RemoveFromBox(droppedItem);

            //*********** Extra step that does nothing. a few lines down PlaceInBox() gets called which dictates final position of thingOnTable **********
            //thingOnTheTable.anchoredPosition = anchored.anchoredPosition;

            // do action of placing dropped item into the box 
            PlaceInBox(droppedItem);

            // Check if the item dropped has already been added to the box or not
            //if (!inTheBox.Contains(droppedItem))
            //    inTheBox.Add(droppedItem);

            // reverse above
            if (inTheBox.Contains(droppedItem))
                return;

            inTheBox.Add(droppedItem);

            //To compare with Tags (QN and QA), with the box and see which enters which. 
            if (droppedItem.CompareTag(acceptableItemTag))
            {
                correctItemsInBoxCount++;
            }

            // invoke event to tell if correct item was added
            onItemDropped?.Invoke(correctItemsInBoxCount);


                /*
                //A check to award points if the the right itea is placed in the box. 
                if (points < 5)     //Points is 5 because of the amount of the crystal variations.
                {
                    points++;
                    if (points == 1)
                    {
                        //Debug.Log("Float");
                        //done = true;        // this is linked to SoundManilpulator to play sound
                    }
                */
                    //This is basically calling the array created to add crystals to the dock. 
                    //crystalSprite.sprite = crystalPhases[points];         //********* moved to Crystal
                    //if (points == 5)
                    //{
                        //almost = true;                //********* redundant. removed
                        //glow.SetActive(true);         //********* moved to Crystal

                        /********
                         * this is hard-coded in the script that it plays the same file named 'static'
                         * for both QN & QL boxes, ending up with a single Imaging from one side only at a time.
                         * This is also moved to CrystalCharger script since it relates to the crystal
                         *******/
                        //soundMan.Imaging("static");         //********* moved to Crystal

                        //soundMan.Play("static");         //********* moved to Crystal
                        //Debug.Log(crystalStation.name+" charged");                            
                    //}
                    //Debug.Log(points);
                //}
            //CheckSorting();            
        }

        //public void Rise()      //The levitation of the crystals
        //{
        //    temPos = levitate.position;
        //    levitate.transform.Translate(Vector3.up * amp * Mathf.Sin(Time.fixedTime));
        //    shinnny.position = levitate.position;
        //}   
        
        void PlaceInBox(GameObject item)
        {
            item.GetComponent<Drag>().GoIntoBox(gameObject, placementReference, itemPlacementShift);

            // transform.GetComponent<Drag>().PutInBox(this.gameObject);        //************ moved that functionality to GoIntoBox ************
        }

        public void RemoveFromBox(GameObject itemInBox)    //The method to remove things in the box. 
        {
            if (!inTheBox.Contains(itemInBox))      //If the object isnt in the box it wouldnt remove.
                return;

            //A check to see if the tags are correct and if there is a point award to the box already.
            if (itemInBox.CompareTag(acceptableItemTag))
            {
                if (correctItemsInBoxCount > 0)
                    correctItemsInBoxCount--;
                
                /*
                if (points > 0)
                {
                    points--;
                    //crystalSprite.sprite = crystalPhases[points];         //********* moved to Crystal
                    //glow.SetActive(false);         //********* moved to Crystal
                }
                */
            }
            
            inTheBox.Remove(itemInBox);

            // rearrange items existing in the box to not sit on top of the new placed item
            ReshuffleBox();

            // invoke event to check if a correct item was removed
            onItemDropped?.Invoke(correctItemsInBoxCount);
            

            //CheckSorting();
        }

        void ReshuffleBox()
        {
            foreach (GameObject placedItem in inTheBox)
                PlaceInBox(placedItem);
        }

        public void EmptyBox()
        {
            // copy list to array since we're modifying original list by removing entries
            // This prevents the foreach enumerator from going through entries
            foreach (GameObject placedItem in inTheBox.ToArray())
            {
                RemoveFromBox(placedItem);
                placedItem.GetComponent<Drag>().ReturnOriginalLocation();
            }
        }

        /*
        public void CheckSorting()  //  Winning Condition for the game.     //******** functionality belongs to the game manager. moved to GameManager/SortingManager
        {
            if (inTheBox.Count == requiredItemsInBox)
            {
                if (points >= goal)
                    sorted = true;
            }
            else
                sorted = false;
        }
        */
    }
}
