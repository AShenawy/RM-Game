using UnityEngine;

namespace Methodyca.Core
{
    // this script connects an Operate-class object to a door for unlocking it
    [RequireComponent(typeof(Operate))]
    public class OperateToDoorConnection : MonoBehaviour
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