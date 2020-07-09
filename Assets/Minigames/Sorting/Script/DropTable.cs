using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Methodyca.Minigames.SortGame
{
    public class DropTable : MonoBehaviour, IDropHandler 
    {
        public GameObject boxQuali;
        public GameObject boxQuanti; 
        public void OnDrop(PointerEventData eventData)//calling the event OnDrop 
        {   
            if (eventData.pointerDrag == null)
            { 
                return;
            }
            //calling the method remove from DragSlot to remove from the box to the table
            boxQuali.GetComponent<DragSlot>().Remove(eventData.pointerDrag);
            boxQuanti.GetComponent<DragSlot>().Remove(eventData.pointerDrag);
            Debug.Log("Dropped on table");
            
        }           
   
    }
 
}
