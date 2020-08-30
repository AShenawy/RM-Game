using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Methodyca.Minigames.SortGame
{
    public class GameManager : MonoBehaviour
    {
        public bool filesArranged;
        public GameObject filesSorted;
        SoundManager soundMan;
        
        void Start()
        {
            soundMan = FindObjectOfType<SoundManager>();

        }
        public void Complete()
        {
            filesSorted.SetActive(true);
            // if(filesArranged == true)
            // {
            //     Debug.Log("You Sabi");
                
            // }
        }
        void Update()
        {
            //Complete();
        }
      
    }

}
