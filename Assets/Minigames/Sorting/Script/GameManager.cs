using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Methodyca.Minigames.SortGame
{
    public class GameManager : MonoBehaviour
    {
        public bool filesArranged, completed = false;
        public GameObject winScreen;
        SoundManager soundMan;
        
        
        void Start()
        {
            soundMan = FindObjectOfType<SoundManager>();
        }

        public void Complete()
        {
            if(!completed)  // ******************* is check necessary?
            {
                completed = true;
                winScreen.SetActive(true);
                soundMan.Stop("Fraud Full");
                soundMan.Stop("static");
                soundMan.Play("comp");
                soundMan.Play("staticL");
            }
        }
    }

}
