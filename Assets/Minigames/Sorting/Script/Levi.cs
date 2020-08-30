using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//namespace
namespace Methodyca.Minigames.SortGame
{
    public class Levi : MonoBehaviour
    {
       public float speed;
       public float amplitude;
       private float temp;
       private Vector3 tempPos;
       private RectTransform used; 

       void Start ()
       {
           used = this.gameObject.GetComponent<RectTransform>();
           tempPos = used.position;
           temp = used.position.y;
       }

       

        // Update is called once per frame
        void Update()
        {
            tempPos.y = temp + amplitude * Mathf.Sin(speed* Time.time);
            used.position = tempPos;
            
        }
    }

}

