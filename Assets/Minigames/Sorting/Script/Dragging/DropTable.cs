using UnityEngine;
using UnityEngine.EventSystems;


namespace Methodyca.Minigames.SortGame
{
    
    public class DropTable : MonoBehaviour, IDropHandler 
    {
        public GameObject boxQuali;
        public GameObject boxQuanti; 
        public GameObject placementParent;
        public void OnDrop(PointerEventData eventData) 
        {
            if (eventData.pointerDrag == null)
            { 
                return;
            }

            //calling the method Remove from DragSlot to remove from the box to the table
            boxQuali.GetComponent<SortBoxBehaviour>().RemoveFromBox(eventData.pointerDrag);
            boxQuanti.GetComponent<SortBoxBehaviour>().RemoveFromBox(eventData.pointerDrag);

            //for the image to appear back on the table.
            eventData.pointerDrag.GetComponent<Drag>().ReturnOriginalLocation(placementParent);
        }           
    }
}
