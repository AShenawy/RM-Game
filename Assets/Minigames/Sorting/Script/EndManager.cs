//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

namespace Methodyca.Minigames.SortGame
{
    public class EndManager : MonoBehaviour
    {
        public bool filesArranged = false;
        

        public void EndGame()
        {
                Debug.Log("Weldone");
            // if(filesArranged == false)
            // {
            //     filesArranged = true;
            // }
        }
        public void Leave(bool yea)
        {
            Debug.Log("Great you suck");
        }

    }

}
