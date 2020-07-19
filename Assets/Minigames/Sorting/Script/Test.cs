using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;

namespace Methodyca.Minigames.SortGame{

    public class Test : MonoBehaviour  //IPointerDownHandler 
    {   
        //the inputs.
        public float degreePerSecond = 20.0f;
        public float amp = 7f;
        public float freq = 1f;

        //Storing positios
        Vector3 posOffset = new Vector3();
        Vector3 temPos = new Vector3();


        // Start is called before the first frame update
        void Start()
        { 
            posOffset = transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            temPos = posOffset;
            temPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * freq) * amp;
            transform.position = temPos;
                
            

        }
    }
}