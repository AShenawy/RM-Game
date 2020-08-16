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
        private int points = 0;
        private int goal = 5;
        public float stun;
        public GameObject crystalStation;//The charging station either pink or blue.
        public GameObject placementParent; // the parent of the items placed in the box
        public GameObject[] listBox ;//the box to show how its removed.

        
        

        // public GameObject boxQuali;//for the sound
        // public GameObject boxQuanti;//for the sound. 
        private RectTransform anchored;//the position of the snapping. 
        public RectTransform levitate;//for the floating crystals.
        public RectTransform shinnny; 

         
        public Sprite[] crystalPhases;//The array of crystals charging. 

        public string boxType;//The tag name for the boxes in the game either QA or QN.
        public string box;
        

        

        public bool sorted;
        //public bool done;
        
        public List <GameObject> inTheBox = new List<GameObject>();//The list for items dropped.
        //public List <GameObject> listBoxi;
        public Vector3 shift;

        //inputs for the levitations
        float degreePerSecond =20f;
        public float amp = 2f;
        public float freq = 0f;

        //storing transform values 
        //Vector3 posOffset = new Vector3();
        public Vector3 temPos = new Vector3();

        // sound manager
        SoundManager soundMan;
        //GameManager levelman;

        private void Start()
        {
            anchored = GetComponent<RectTransform>();
            
            levitate = crystalStation.GetComponent<RectTransform>();
            //posOffset = levitate.position;
            //stun = freq + 0.5f;
            soundMan = FindObjectOfType<SoundManager>();
            //levelman = FindObjectOfType<GameManager>();            
        
            
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
                        //This is basically calling the array created to add crystals to the dock. 
                        crystalStation.GetComponent<Image>().sprite = crystalPhases[points];
                        stun = freq + 0.5f;
                        soundMan.Imaging("battery");
                        soundMan.Bounce("battery");
                        soundMan.Play("battery");
                        
                        //Debug.Log("Charging");
                        Debug.Log(points);
                    }
                }

                //Debug.Log("Addng to the box");
            }           
            
                FileArranged();            
        }

        void Rise()
        {
            //The levitation of the crystals
            temPos = shinnny.position;
            //temPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * freq *stun) * amp;
            // shinnny.Translate();
            shinnny.transform.Translate(Vector3.up * amp * Mathf.Sin(Time.timeSinceLevelLoad * points));
    
            levitate.position = shinnny.position;
            
            //Debug.Log(temPos.y);
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
                    //stun = freq - 0.5f;    
                    //Debug.Log("Discharging");
                    //Debug.Log(points);
                    
            }
            Debug.Log("Takeout");
            
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

        public void Check()
        {   
           
        }
        void Update()
        {
            Rise();
            
        }
    }

}
