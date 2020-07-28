using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Methodyca.Core
{
    // This creates a scriptable object to store data for each inventory item
    [CreateAssetMenu(fileName = "New Item",menuName = "Inventory/Item")]
    public class Item : ScriptableObject
    {
        public string name;
        public Sprite icon;
        public Texture2D cursorImage;
    }
}