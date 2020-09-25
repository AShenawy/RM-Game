using UnityEngine;

namespace Methodyca.Minigames.SortGame
{
    // this script handles the sorting minigame gameplay
    public class SortingManager : MonoBehaviour
    {
        public int currentPoints;
        public int goalPoints = 10;         // the total number of points needed to win from both QL and QN

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
