using UnityEngine;
using UnityEngine.EventSystems;

namespace Methodyca.Minigames.Protoescape
{
    public class UIHighlightTool : UIBaseTool, IPointerClickHandler
    {
        [SerializeField] private Sprite on;
        [SerializeField] private Sprite off;

        private IHighlighted _selection;

        public void OnPointerClick(PointerEventData eventData)
        {
            _selection.SetHighlight();
        }

        protected override void SelectionTriggered(GameObject selection)
        {
            if (selection.GetComponent<IHighlighted>() != null)
            {
                Enable();

                _selection = selection.GetComponent<IHighlighted>();

                if (_selection.IsHighlighted)
                {
                    toolbarIcon.sprite = on;
                }
                else
                {
                    toolbarIcon.sprite = off;
                }
            }
            else
            {
                toolbarIcon.sprite = off;
                Disable();
            }
        }
    }
}