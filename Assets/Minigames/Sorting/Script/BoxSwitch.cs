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
       //public GameManager cry;
       
       public bool almost = false;
       GameManager levelman;
       SoundManager soundman;


        void Start()
        {
            levelman =  FindObjectOfType<GameManager>();
            soundman = FindObjectOfType<SoundManager>();
        }
    

       void Update()
       {
          if (firstbox.sorted && secondbox.sorted)
          {
                levelman.Complete();
                almost = true;
          }
       }
        

    }

        
            
}   

