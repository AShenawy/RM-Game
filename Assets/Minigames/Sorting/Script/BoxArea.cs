using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//namespace
namespace Methodyca.Minigames.SortGame
{
    public class BoxArea : MonoBehaviour
    {
        public RectTransform localPosition; //the local space of the snapping.
        
        
        void Start()
        {
            //localPosition = GetComponent<RectTransform>();//the area of the box relative to the anchor point. 
            Debug.Log("Local postion is" + localPosition.rect);
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}