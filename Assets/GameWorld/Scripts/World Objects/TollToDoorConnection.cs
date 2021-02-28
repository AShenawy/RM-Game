using System.Collections;
using UnityEngine;

namespace Methodyca.Core
{
    // this script connects the toll machine to the door for unlocking it
    public class TollToDoorConnection : MonoBehaviour
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