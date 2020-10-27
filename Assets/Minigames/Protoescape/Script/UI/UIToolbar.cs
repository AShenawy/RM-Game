using UnityEngine;

namespace Methodyca.Minigames.Protoescape
{
    public class UIToolbar : MonoBehaviour
    {
        [SerializeField] private GameObject root;

        private void OnEnable()
        {
            GameManager_Protoescape.OnPrototypeInitiated += PrototypeInitiatedHandler;
        }

        private void PrototypeInitiatedHandler()
        {
            root.SetActive(true);
        }

        private void OnDisable()
        {
            GameManager_Protoescape.OnPrototypeInitiated -= PrototypeInitiatedHandler;
        }
    }
}