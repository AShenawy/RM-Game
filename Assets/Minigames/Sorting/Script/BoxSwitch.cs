using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace Methodyca.Minigames.SortGame
{   public class BoxSwitch : MonoBehaviour
    {
        //public SpriteRenderer firstImage;
        public Sprite ontable;
        public Sprite inbox;
        public Sprite falz;
        public GameObject item; 

        void Start()
        {
            item = this.gameObject;
            falz = item.GetComponent<Image>().sprite;
            falz = ontable;
        }
        // public void OnDrop(PointerEventData eventData)
        // {
                
            
        // }
        public void onTheTable()
        {
           if(Input.GetKeyDown(KeyCode.Q))
           {
               if(falz = inbox)
                falz =  ontable;
               Debug.Log("Your image is now on the table.");
           }
        }
        public void inTheBox()
        {
            if(Input.GetKeyDown(KeyCode.W))
            {
                if(falz = ontable)
                falz = inbox;
                Debug.Log("Your item is back on the table");
            }
        }
        void Update()
        {
            onTheTable();
            inTheBox();
            item.GetComponent<Image>().sprite = falz;
        }

        
    }

        
            
}   

