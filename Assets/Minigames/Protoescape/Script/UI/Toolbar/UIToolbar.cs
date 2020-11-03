using UnityEngine;

namespace Methodyca.Minigames.Protoescape
{
    public class UIToolbar : MonoBehaviour
    {
        [SerializeField] private GameObject root;
        [SerializeField] private GameObject editToolbar;
        [SerializeField] private GameObject testToolbar;

        private void OnEnable()
        {
            GameManager_Protoescape.OnPrototypeInitiated += PrototypeInitiatedHandler;
            PrototypeTester.OnPrototypeTestInitiated += PrototypeTestInitiatedHandler;
        }

        private void Start()
        {
            root.SetActive(false);
            testToolbar.SetActive(false);
        }

        private void PrototypeTestInitiatedHandler()
        {
            root.SetActive(true);
            editToolbar.SetActive(false);
            testToolbar.SetActive(true);
        }

        private void PrototypeInitiatedHandler()
        {
            root.SetActive(true);
            editToolbar.SetActive(true);
            testToolbar.SetActive(false);
        }

        private void OnDisable()
        {
            GameManager_Protoescape.OnPrototypeInitiated -= PrototypeInitiatedHandler;
            PrototypeTester.OnPrototypeTestInitiated -= PrototypeTestInitiatedHandler;
        }
    }
}