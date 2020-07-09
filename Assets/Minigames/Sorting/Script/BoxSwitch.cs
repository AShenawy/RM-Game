using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace Methodyca.Minigames.SortGame
{   public class BoxSwitch : MonoBehaviour 
    {
        //public GameObject this_box;

        //public bool activation;

        public RectTransform boxSpace;
    
    
        // For the boxes to switch layers when after drop has been triggered. 
        void Start()
        {
            boxSpace = GetComponent<RectTransform>(); 
            Debug.Log(boxSpace.rect);
        }
        // Update is called once per frame
        void Update()
        {
            //swtiching the boxes
            //this_box.SetActive(activation);
        }
    }
        
            
}   

