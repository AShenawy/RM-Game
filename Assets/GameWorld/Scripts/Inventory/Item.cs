using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This creates a scriptable object to store data for each inventory item
[CreateAssetMenu(fileName = "New Item",menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite itemImage;
}
