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

        private RectTransform anchored;//the position of the snapping. 
         
        public Sprite[] crystalPhases;//The array of crystals charging. 

        public string boxType;//The tag name for the boxes in the game either QA or QN.
        
        public List <GameObject> inTheBox = new List<GameObject>();//The list for items dropped. 



        private void Start()
        {
            anchored = GetComponent<RectTransform>();
          

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
            //This is basically to define the things on the table and, make them snap to the box when clicked. 
            RectTransform thingOnTheTable; //Anchored position. 
            thingOnTheTable = eventData.pointerDrag.GetComponent<RectTransform>();
            thingOnTheTable.anchoredPosition = anchored.anchoredPosition;


            //To compare with Tags (QN and QA), with the box and see which enters which. 
            if(thingOnTheTable.CompareTag(boxType))
            {
                //A check to award points if the the right itea is placed in the box. 
                if (points < 5) //Points is 5 because of the amount of the crystal variations.
                {          
                    points++;

                    //This is basically calling the array created to add crystals to the dock. 
                    Sprite sprite = crystalPhases[points];
                    crystalStation.GetComponent<Image>().sprite = sprite;
                    Debug.Log("Charging");
                    Debug.Log(points);
                } 

                //To check if the itea dropped has being added to the box or not. 
                inTheBox.Add(thingOnTheTable.gameObject);//created a method of IntheBox
                Debug.Log("Addng to the box");
            }
            
            thingOnTheTable.GetComponent<Drag>().PutInBox(this.gameObject);
        }
        public void Remove(GameObject thingInTheBox)//The method to remove things in the box. 
        {   
            if (!inTheBox.Remove(thingInTheBox)) //If the object isnt in the box it wouldnt remove.
            return;
            Debug.Log("Removing");
           
            if(thingInTheBox.CompareTag(boxType))//A check to see if the tags are correct and if there is a point award to the box already.
            {
               if(points > 0) 
                points --;
                Sprite sprite = crystalPhases[points];
                crystalStation.GetComponent<Image>().sprite = sprite;
                Debug.Log(points);
            }

            
        }
    }
}
