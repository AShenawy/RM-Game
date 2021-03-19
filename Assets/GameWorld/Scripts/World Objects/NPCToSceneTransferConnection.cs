using UnityEngine;


namespace Methodyca.Core
{
    // this script connects a NPC-class object to a scene transfer link object for allowing to use it
    [RequireComponent(typeof(NPC))]
    public class NPCToSceneTransferConnection : MonoBehaviour
    {
        [Tooltip("The object that transfers the player to another scene"), SerializeField]
        private SceneTransfer sceneTransferObject;


        private void OnEnable()
        {
            GetComponent<NPC>().onGivenAllItems += UnlockConnectedDoor;
        }

        void UnlockConnectedDoor()
        {
            sceneTransferObject.AllowTransfer(true);
        }

        private void OnDisable()
        {
            GetComponent<NPC>().onGivenAllItems -= UnlockConnectedDoor;
        }
    }
}