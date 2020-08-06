using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


//namespace
namespace Methodyca.Minigames.SortGame
{
    // class naming
    public class DragSlot : MonoBehaviour, IDropHandler
    {
        private int points = 0;
        public GameObject crystalStation;//The charging station either pink or blue.
        public GameObject placementParent; // the parent of the items placed in the box
        // public GameObject boxQuali;//for the sound
        // public GameObject boxQuanti;//for the sound. 
        private RectTransform anchored;//the position of the snapping. 
        private RectTransform levitate;//for the floating crystals. 
         
        public Sprite[] crystalPhases;//The array of crystals charging. 

        public string boxType;//The tag name for the boxes in the game either QA or QN.
        
        public List <GameObject> inTheBox = new List<GameObject>();//The list for items dropped.

        public Vector3 shift;

        //inputs for the levitations
        float degreePerSecond =20f;
        float amp = 7f;
        float freq = 1f;

        //storing transform values 
        Vector3 posOffset = new Vector3();
        Vector3 temPos = new Vector3();

        // sound manager
        SoundManager soundMan;

        private void Start()
        {
            anchored = GetComponent<RectTransform>();
            
            levitate = crystalStation.GetComponent<RectTransform>();
            posOffset = levitate.position;

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

            Debug.Log("Dropped");

            //Sound Effect
            soundMan.Imaging("paper_hit");
            soundMan.Play("paper_hit");//sound of the game.



            //This is basically to define the things on the table and, make them snap to the box when clicked. 
            RectTransform thingOnTheTable; //Anchored position. 
            thingOnTheTable = eventData.pointerDrag.GetComponent<RectTransform>();
            thingOnTheTable.anchoredPosition = anchored.anchoredPosition;


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

                        soundMan.Imaging("battery");
                        soundMan.Bounce("battery");
                        soundMan.Play("battery");

                        Debug.Log("Charging");
                        Debug.Log(points);
                    }
                }

                Debug.Log("Addng to the box");
            }           
            
            PlaceInBox(thingOnTheTable);//instatioation 
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

            inTheBox.Remove(thingInTheBox);

            Debug.Log("Removing");
           
            if(thingInTheBox.CompareTag(boxType))//A check to see if the tags are correct and if there is a point award to the box already.
            {
               if(points > 0) 
                points --;
                crystalStation.GetComponent<Image>().sprite = crystalPhases[points];
                Debug.Log(points);
            }
        }
        void Update()
        {
            //The levitation of the crystals
            temPos = posOffset;
            temPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * freq * points) * amp;
            levitate.position = temPos;
        }
    }
}
