using UnityEngine;

namespace Methodyca.Core
{
    // this script connects a NPC-class object to a door for unlocking it
    public class NPCToDoorConnection : MonoBehaviour
    {

        [SerializeField] private Door door;

        private void OnEnable()
        {
            GetComponent<Operate>().onOperation += UnlockConnectedDoor;
        }

        void UnlockConnectedDoor(Operate opr)
        {
            door.Unlock();
        }

        private void OnDisable()
        {
            GetComponent<Operate>().onOperation -= UnlockConnectedDoor;
        }
    }
}