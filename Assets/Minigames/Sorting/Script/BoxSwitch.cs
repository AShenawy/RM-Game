using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace Methodyca.Minigames.SortGame
{   public class BoxSwitch : MonoBehaviour 
    {
        public float amp;
        public float freq;
        public float degreePerSecond = 30f;
        public Vector3 posOffset = new Vector3();
        public Vector3 temPos = new Vector3();
        //public Vector3 loose.position.y = new Vector3();
        
        //[Range(1f, 4f)]
        public RectTransform levitate;
        public RectTransform boibo;// RectTransform of the Object

        void Lift()
        {
            posOffset =  boibo.position;
            temPos = posOffset;
            temPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * freq )* amp;
            
            levitate.position = temPos;
            
            //Debug.Log(levitate.position);
        }

        void Gear()
        {
            if(Input.GetKeyDown(KeyCode.O))
            {
                freq = freq + degreePerSecond;  
                Debug.Log("New Freq is "+ freq);
                
            }
        }
        void Irubie()
        {
            if(Input.GetKeyDown(KeyCode.P))
            {
                degreePerSecond = degreePerSecond+ 1f; 
                Debug.Log("your new degree is " + degreePerSecond);
            }
        }
        void itsjustkewa()
        {
            if(Input.GetKeyDown(KeyCode.I))
            {
                degreePerSecond = degreePerSecond - 1f;
                Debug.Log("Subtracting... Your DPS is " + degreePerSecond); 
            }
        }

        
    
    
        
        void Start()
        {
            //boibo.position = temPos;
        }
        // Update is called once per frame
        void Update()
        {
            Lift();
            Gear();
            Irubie();
            itsjustkewa();
        }
    }
        
            
}   

