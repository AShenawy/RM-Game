using UnityEngine;


namespace Methodyca.Minigames.SortGame
{   
    public class BoxSwitch : MonoBehaviour 
    {
       public SortBoxBehaviour firstbox;
       public SortBoxBehaviour secondbox;
       
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

