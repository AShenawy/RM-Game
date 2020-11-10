using UnityEngine;
using UnityEngine.EventSystems;

namespace Methodyca.Minigames.Protoescape
{
    public class UIHighlightTool : UIBaseTool, IPointerClickHandler
    {
        private IHighlightable _selection;

        public void OnPointerClick(PointerEventData eventData)
        {
            _selection.SetHighlight();
        }

        protected override void SelectionTriggered(GameObject selection)
        {
            if (selection != null && selection.GetComponent<IHighlightable>() != null)
            {
                Enable();
            }
            else
            {
                Disable();
            }
        }
    }
}