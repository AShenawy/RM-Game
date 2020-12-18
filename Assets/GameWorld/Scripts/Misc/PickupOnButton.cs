using UnityEngine;


namespace Methodyca.Core
{
    // This is a helper script which makes a button give the player a pickup
    public class PickupOnButton : MonoBehaviour
    {
        public void GiveItem(Item item)
        {
            InventoryManager.instance.Add(item);
        }
    }
}