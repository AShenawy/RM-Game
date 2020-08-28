using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;




namespace Methodyca.Minigames.SortGame
{   public class BoxSwitch : MonoBehaviour 
    {
            
       //public GameObject boxOne;
       //public GameObject boxtwo;

       public DragSlot firstbox;
       public DragSlot secondbox;
       
       GameManager levelman;
       SoundManager soundman;

       
       
    
       //public int balance = 10;

        void Start()
        {
            //firstbox = boxOne.GetComponent<DragSlot>();
            //secondbox = boxtwo.GetComponent<DragSlot>();
            levelman =  FindObjectOfType<GameManager>();
            soundman = FindObjectOfType<SoundManager>();
        }

        public void Sky()
        {
        
        }
    

       void Update()
       {
          if (firstbox.sorted && secondbox.sorted)
          {
              //Debug.Log("Wins");
              soundman.Stop("paper_hit");
              soundman.Stop("battery");
              levelman.Complete();
              soundman.Stop("Fraud Full");
          }
       }
        

    }

        
            
}   

