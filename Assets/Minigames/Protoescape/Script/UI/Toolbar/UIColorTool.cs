﻿using UnityEngine;

namespace Methodyca.Minigames.Protoescape
{
    public class UIColorTool : UIBaseTool
    {
        protected override void SelectionTriggered(GameObject selection)
        {
            if (selection != null && selection.GetComponent<IReplaceable<Color>>() != null)
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