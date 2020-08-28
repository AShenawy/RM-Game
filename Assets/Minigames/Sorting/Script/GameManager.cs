using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Methodyca.Minigames.SortGame
{
    public class GameManager : MonoBehaviour
    {
        public bool filesArranged = false;
        public GameObject filesSorted;
        public void Complete()
        {
            filesSorted.SetActive(true);
            
            //Debug.Log("You Sabi");
        }
        // public void Reup()
        // {
        //     if(Input.GetKeyDown(KeyCode.R))
        //     {
        //         SceneManager.LoadScene("Drag and Drop");
        //         Debug.Log("Restart");
        //     }
        // }
    }

}
