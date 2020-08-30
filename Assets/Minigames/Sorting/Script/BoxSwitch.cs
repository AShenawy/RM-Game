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
       public GameManager cry;
       
       GameManager levelman;
       SoundManager soundman;


        void Start()
        {
            //firstbox = boxOne.GetComponent<DragSlot>();
            //secondbox = boxtwo.GetComponent<DragSlot>();
            levelman =  FindObjectOfType<GameManager>();
            soundman = FindObjectOfType<SoundManager>();
        }

        public void Sky()
        {
            if(cry.filesArranged == true)
            {
                Debug.Log("Crazy");
            }
        }
    

       void Update()
       {
          if (firstbox.sorted && secondbox.sorted)
          {
              //soundman.Play("comp");
              levelman.Complete();
              soundman.Stop("Fraud Full");
              soundman.Stop("static");
              Debug.Log("Wins");
              //soundman.Stop("paper_hit");
              //soundman.Stop("battery");
          }
          Sky();
       }
        

    }

        
            
}   

