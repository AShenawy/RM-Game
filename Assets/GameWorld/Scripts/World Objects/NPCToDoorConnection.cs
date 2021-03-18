using UnityEngine;

namespace Methodyca.Core
{
    // this script connects a NPC-class object to a door for unlocking it
    [RequireComponent(typeof(NPC))]
    public class NPCToDoorConnection : MonoBehaviour
    {
        [SerializeField] private Door door;


        private void OnEnable()
        {
            GetComponent<NPC>().onGivenAllItems += UnlockConnectedDoor;
        }

        void UnlockConnectedDoor()
        {
            door.Unlock();
        }

        private void OnDisable()
        {
            GetComponent<NPC>().onGivenAllItems -= UnlockConnectedDoor;
        }
    }
}