//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System;

namespace Methodyca.Minigames.SortGame
{
    public class EndManager : MonoBehaviour, IPointerClickHandler
    {
        //public bool filesArranged = false;
        

        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log("restart");
            SceneManager.LoadScene("Data Charger");

        }
        
        
        
    }

}
