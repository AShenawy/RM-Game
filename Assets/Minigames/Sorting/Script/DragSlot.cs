using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;


//namespace
namespace Methodyca.Minigames.SortGame
{
    // class naming
    public class DragSlot : MonoBehaviour, IDropHandler
    {
        [HideInInspector]public int points = 0;
        private int goal = 5;
        //public float stun;
        public GameObject crystalStation;//The charging station either pink or blue.
        public GameObject placementParent; // the parent of the items placed in the box
        [HideInInspector]public GameObject[] listBox ;//the box to show how its removed.
        [HideInInspector]public GameObject glow;//the halo effect 
        private RectTransform anchored;//the position of the snapping. 

        //The levitation for the crytsals. 
        [HideInInspector]public RectTransform levitate;//for the floating crystals.
        [HideInInspector]public RectTransform shinnny;//stored position for the rect transfrom.  
        [HideInInspector]public Vector3 temPos;
      
        //inputs for the levitations
        float degreePerSecond =20f;
        public float amp;
        private float temp;

        [HideInInspector]public Sprite[] crystalPhases;//The array of crystals charging. 

        public string boxType;//The tag name for the boxes in the game either QA or QN.
        [HideInInspector]public string box;

        //bools for the game manager. 
        [HideInInspector]public bool sorted;
        [HideInInspector]public bool done;
        [HideInInspector]public bool almost;
        
        public List <GameObject> inTheBox = new List<GameObject>();//The list for items dropped.
      
        [HideInInspector]public Vector3 shift;//the distance between the items in box,for spacing. 



        // sound manager
        SoundManager soundMan;
      

        private void Start()
        {
            anchored = GetComponent<RectTransform>();
            glow.SetActive(false);
            shinnny = crystalStation.GetComponent<RectTransform>();
            //temp = shinnny.position.y;
            //temPos = levitate.position;
            //degreePerSecond = levitate.position.y;
            soundMan = FindObjectOfType<SoundManager>();
                       
        
            
        }
        public void OnDrop(PointerEventData eventData) //Mouse released. 
        {
            // A check to see if the objects are placed in the box, represented with charging crystals and to snap items to the box.
            if (crystalStation == null)
            {
                return;
            }

            if (eventData.pointerDrag == null)
            {
                return;
            }

            

            //Sound Effect
            soundMan.Imaging("paper_hit");
            soundMan.Play("paper_hit");//sound of the game.
            //Debug.Log("placing");



            //This is basically to define the things on the table and, make them snap to the box when clicked. 
            RectTransform thingOnTheTable; //Anchored position. 
            thingOnTheTable = eventData.pointerDrag.GetComponent<RectTransform>();
            thingOnTheTable.anchoredPosition = anchored.anchoredPosition;

            PlaceInBox(thingOnTheTable);//instatioation 

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
            
                FileArranged();            
        }

        public void Rise()//The levitation of the crystals
        {
            
            temPos = levitate.position;
            levitate.transform.Translate(Vector3.up * amp * Mathf.Sin(Time.fixedTime));
            shinnny.position = levitate.position;
            
        }     
        void PlaceInBox(RectTransform transform)
        {
            transform.GetComponent<Drag>().InsideBox(placementParent, shift);//prefab instaniation. 

            transform.GetComponent<Drag>().PutInBox(this.gameObject);
        }

        public void Remove(GameObject thingInTheBox)//The method to remove things in the box. 
        {   
            if (!inTheBox.Contains(thingInTheBox)) //If the object isnt in the box it wouldnt remove.
            
                return;
            
                //Debug.Log("Hello");
                inTheBox.Remove(thingInTheBox);


            if(thingInTheBox.CompareTag(boxType))//A check to see if the tags are correct and if there is a point award to the box already.
            {
                //Debug.Log("ISEA");
                if(points > 0)
                     
                    //Debug.Log("Deducting Points");
                    points --; 
                    crystalStation.GetComponent<Image>().sprite = crystalPhases[points];
                    glow.SetActive(false);
                    //stun = freq - 0.5f;    
                    //Debug.Log("Discharging");
                    //Debug.Log(points);
                    
            }
            //Debug.Log("Takeout");
            
        }
        public void FileArranged() //Winning Condition for the game. 
        {
            if(inTheBox.Where(x => listBox.Contains(x)).ToList().Count == inTheBox.Count)// list is equal to the array
            {
                
                    
                    {
                        if(points >= goal)
                        {
                            if(sorted = this.gameObject.CompareTag(box))
                            {
                                
                                
                            }
                            //Debug.Log("Finally"); 
                            //levelman.Complete();
                            
                        } 
                        
                    } 
                        
                
                
            }

        }

        void Update()
        {
            if(points >= 1)
            {
                Rise();
            }
            
        }
    }

}
