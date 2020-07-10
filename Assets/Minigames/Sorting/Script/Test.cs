using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;

namespace Methodyca.Minigames.SortGame{

    public class Test : MonoBehaviour  //IPointerDownHandler 
    {   
        float currentTIme = 0f;
        float setTime = 10f;


        // Start is called before the first frame update
        void Start()
        { 
            currentTIme = setTime;
        }

        // public void OnPointerDown(PointerEventData eventData)
        // {
        //     Debug.Log("shouting");
        // }

        // Update is called once per frame
        void Update()
        {
            currentTIme -= 1 * Time.deltaTime;
            Debug.Log(currentTIme);
            if(currentTIme <= 0)
            {
                currentTIme = setTime;
                Debug.Log("Auto Off");
                
            }

        }
    }
}