using UnityEngine;

// this script handles the sorting minigame gameplay
namespace Methodyca.Minigames.SortGame
{
    public class SortingManager : MonoBehaviour
    {
        public bool filesArranged; // **************** this field seems unused 
        public bool completed;
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
