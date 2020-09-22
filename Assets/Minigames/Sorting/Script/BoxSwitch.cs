using UnityEngine;


namespace Methodyca.Minigames.SortGame
{   
    public class BoxSwitch : MonoBehaviour 
    {
       public DragSlot firstbox;
       public DragSlot secondbox;
       
       public bool almost = false;
       SortingManager levelman;


        void Start()
        {
            levelman =  FindObjectOfType<SortingManager>();
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

