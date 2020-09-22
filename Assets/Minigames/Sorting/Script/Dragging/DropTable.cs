using UnityEngine;
using UnityEngine.EventSystems;


namespace Methodyca.Minigames.SortGame
{
    public class DropTable : MonoBehaviour, IDropHandler //The Remove method to the Game Object: Table. 
    {
        public GameObject boxQuali;
        public GameObject boxQuanti; 
        public GameObject placementParent;
        public void OnDrop(PointerEventData eventData)//calling the event OnDrop 
        {   
            if (eventData.pointerDrag == null)
            { 
                return;
            }

            //calling the method Remove from DragSlot to remove from the box to the table
            boxQuali.GetComponent<DragSlot>().Remove(eventData.pointerDrag);
            boxQuanti.GetComponent<DragSlot>().Remove(eventData.pointerDrag);

            //for the image to appear back on the table.
            eventData.pointerDrag.GetComponent<Drag>().OutsideBox(placementParent);
        }           
    }
}
