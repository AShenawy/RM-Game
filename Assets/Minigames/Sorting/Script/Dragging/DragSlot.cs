using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace Methodyca.Minigames.SortGame
{
    public class DragSlot : MonoBehaviour, IDropHandler
    {
        public int points = 0;
        private int goal = 5;
        public GameObject crystalStation;       //The charging station either pink or blue.
        public GameObject placementParent;      // the parent of the items placed in the box
        public int requiredItemsInBox;          // condition for how many items should be placed in box to win
        //public GameObject[] listBox;            //the box to show how its removed.
        public GameObject glow;                 //the halo effect 
        private RectTransform anchored;         //the position of the snapping. 

        //The levitation for the crytsals. 
        public RectTransform levitate;          //for the floating crystals.
        public RectTransform shinnny;           //stored position for the rect transfrom.  
        public Vector3 temPos;
      
        //inputs for the levitations
        public float amp;

        public Sprite[] crystalPhases;          //The array of crystals charging. 

        public string boxType;                  //The tag name for the boxes in the game either QA or QN.

        //bools for the game manager. 
        public bool sorted;
        public bool done;
        public bool almost;
        
        public List <GameObject> inTheBox = new List<GameObject>();     //The list for items dropped.

        public Vector3 shift;       //the distance between the items in box,for spacing. 
        
        SoundManager soundMan;
      

        private void Start()
        {
            anchored = GetComponent<RectTransform>();
            glow.SetActive(false);
            shinnny = crystalStation.GetComponent<RectTransform>();
            soundMan = FindObjectOfType<SoundManager>();
        }

        void Update()
        {
            if (points >= 1)
                Rise();
        }

        //Mouse released. 
        public void OnDrop(PointerEventData eventData) 
        {
            // A check to see if the objects are placed in the box, represented with charging crystals and to snap items to the box.
            if (crystalStation == null)
                return;

            if (eventData.pointerDrag == null)
                return;

            //Sound Effect
            soundMan.Imaging("paper_hit");
            soundMan.Play("paper_hit");     //sound of the game.


            //This is basically to define the things on the table and, make them snap to the box when clicked. 
            RectTransform thingOnTheTable;      //Anchored position. 
            thingOnTheTable = eventData.pointerDrag.GetComponent<RectTransform>();
            thingOnTheTable.anchoredPosition = anchored.anchoredPosition;

            PlaceInBox(thingOnTheTable);        //instatioation 

            //To check if the item dropped has been added to the box or not. 
            if (!inTheBox.Contains(thingOnTheTable.gameObject))
            {
                inTheBox.Add(thingOnTheTable.gameObject);//created a method of IntheBox
                
                //To compare with Tags (QN and QA), with the box and see which enters which. 
                if (thingOnTheTable.CompareTag(boxType))
                {
                    //A check to award points if the the right itea is placed in the box. 
                    if (points < 5) //Points is 5 because of the amount of the crystal variations.
                    {
                        points++;
                        if(points == 1)
                        {
                            //Debug.Log("Float");
                            done = true;
                        }
                        //This is basically calling the array created to add crystals to the dock. 
                        crystalStation.GetComponent<Image>().sprite = crystalPhases[points];
                        if(points == 5)
                        {
                            almost = true;
                            glow.SetActive(true);
                            soundMan.Imaging("static");
                            soundMan.Play("static");
                            //Debug.Log(crystalStation.name+" charged");                            
                        }
                        
                        Debug.Log(points);
                    }
                }
                
                //Debug.Log("Addng to the box");
            }           
            
                CheckSorting();            
        }

        public void Rise()      //The levitation of the crystals
        {
            temPos = levitate.position;
            levitate.transform.Translate(Vector3.up * amp * Mathf.Sin(Time.fixedTime));
            shinnny.position = levitate.position;
        }   
        
        void PlaceInBox(RectTransform transform)
        {
            transform.GetComponent<Drag>().InsideBox(placementParent, shift);       //prefab instaniation. 

            transform.GetComponent<Drag>().PutInBox(this.gameObject);
        }

        public void Remove(GameObject thingInTheBox)    //The method to remove things in the box. 
        {   
            if (!inTheBox.Contains(thingInTheBox))      //If the object isnt in the box it wouldnt remove.
                return;
            
            inTheBox.Remove(thingInTheBox);

            //A check to see if the tags are correct and if there is a point award to the box already.
            if(thingInTheBox.CompareTag(boxType))
            {
                if (points > 0)
                {
                    points--;
                    crystalStation.GetComponent<Image>().sprite = crystalPhases[points];
                    glow.SetActive(false);
                }  
            }
            
            CheckSorting();
        }

        public void CheckSorting()  //Winning Condition for the game. 
        {
            if (inTheBox.Count == requiredItemsInBox)
            {
                if (points >= goal)
                    sorted = true;
            }
            else
                sorted = false;
        }
    }
}
