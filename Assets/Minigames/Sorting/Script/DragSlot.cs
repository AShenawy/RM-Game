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
        // accessor not explicitly defined in this class. inconsistent with Drag class. Need to choose style.
        int points = 0;
        public GameObject crystal_pink;


        //public Manager drag;

        // box position
        private RectTransform position;

        public Sprite[] crystal_phases_pink;



        private void Start()
        {
            crystal_pink = GameObject.Find("crystal_pink_station");
            position = GetComponent<RectTransform>();
           

        }

        public void OnDrop(PointerEventData eventData)
        {
            // It's better to move this statement to Start method than to do the check during gameplay.
            // Can also make the Dock Station GO a variable and assign the object in inspector.
            if (crystal_pink == null)
            {
                return;
            }

            if (eventData.pointerDrag == null)
            { 

                return;
            }

            Debug.Log("Dropped");
            // The anchoredPosition of the GO this script is on can be made into a variable
            // Place the item inside box
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = position.anchoredPosition;

            if (points < 5)
            {
                // since the points variable isn't initialised it will automatically start at 0.
                // However, it's better to explicitly initialise it at the top, since this can be confusing when reading the code
                // and only see points++ without a starting value.
                points++;
                // Better to replace this with Sprite array than load from Resources live. Then the if check will need to be <4 
                //Sprite sprite = Resources.Load<Sprite>("ChargingRates/charger_" + points.ToString());

                Sprite sprite = crystal_phases_pink[points];

                // Suggest replacing with battery.GetComponent<Image>().sprite for easy debug. Can see it in inspector
                //battery.GetComponent<Image>().overrideSprite = sprite;
                crystal_pink.GetComponent<Image>().sprite = sprite;
                Debug.Log("Change Image");
            }


        }


    }
}
