using UnityEngine;

namespace Methodyca.Minigames.Protoescape
{
    public class UIFontTool : UIBaseTool
    {
        protected override void SelectionTriggered(GameObject selection)
        {
            if (selection != null && selection.GetComponent<IReplaceable<TMPro.TMP_FontAsset>>() != null)
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