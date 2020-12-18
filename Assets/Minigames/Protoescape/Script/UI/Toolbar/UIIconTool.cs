using UnityEngine;

namespace Methodyca.Minigames.Protoescape
{
    public class UIIconTool : UIBaseTool
    {
        protected override void SelectionTriggered(GameObject selection)
        {
            if (selection != null && selection.GetComponent<IReplaceable<Sprite>>() != null)
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