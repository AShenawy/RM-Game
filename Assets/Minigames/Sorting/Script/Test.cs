using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;

namespace Methodyca.Minigames.SortGame{

    public class Test : MonoBehaviour  //IPointerDownHandler 
    {   
        //the inputs.
        //public float degreePerSecond = 20.0f;
        public float amp ;
        
        //public float freq = 1f;

        //Storing positios
        //Vector3 posOffset = new Vector3();
        //Vector3 temPos = new Vector3();

       


        //Start is called before the first frame update
        // void Awake()
        // { 
            
        // }

        // Update is called once per frame
        void Update()
        {
            //float r = Input.GetAxis("Mouse X");
            //float xPos = r*amp;
            if (Input.GetKeyDown(KeyCode.K))
            {
                Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Debug.Log(pos.x);
            }
            
        }
    }
}