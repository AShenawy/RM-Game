using UnityEngine;


namespace Methodyca.Minigames.SortGame
{   
    public class BoxSwitch : MonoBehaviour 
    {
       public DragSlot firstbox;
       public DragSlot secondbox;
       
       public bool almost = false;
       GameManager levelman;


        void Start()
        {
            levelman =  FindObjectOfType<GameManager>();
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

