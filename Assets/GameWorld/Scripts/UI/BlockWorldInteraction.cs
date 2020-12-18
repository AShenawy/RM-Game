using UnityEngine.EventSystems;
using UnityEngine;


namespace Methodyca.Core
{
    public class BlockWorldInteraction : MonoBehaviour
    {
        // Update is called once per frame
        void Update()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                GameManager.instance.canInteract = false;
            }
        }
    }
}