using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace Methodyca.Minigames.SortGame
{   public class BoxSwitch : MonoBehaviour
    {
        public GameObject this_box;

        bool activation;
    
    
        // For the boxes to switch layers when after drop has been triggered. 
        void Start()
        { 
            //swtiching the boxes
            if (this_box == true)
            {
                this_box.SetActive(true);
            }
            else 
            {
                this_box.SetActive(false);
            }

        }

    // Update is called once per frame
         void Update()
            {
                
            }
        
            
    }   
}

