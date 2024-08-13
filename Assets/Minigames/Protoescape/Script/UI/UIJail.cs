using UnityEngine;

namespace Methodyca.Minigames.Protoescape
{
    public class UIJail : MonoBehaviour
    {
        [SerializeField] private GameObject root;

        private void OnEnable()
        {
            GameManager_Protoescape.OnPrototypeInitiated += PrototypeInitiatedHandler;
            PrototypeTester.OnPrototypeTestInitiated += PrototypeTestInitiatedHandler;
            PrototypeTester.OnPrototypeTestCompleted += PrototypeTestCompletedHandler;
        }

        private void PrototypeInitiatedHandler()
        {
            root.SetActive(false);
        }

        private void PrototypeTestCompletedHandler(bool isCompleted, string obj)
        {
            root.SetActive(true);
        }

        private void PrototypeTestInitiatedHandler(string[] obj)
        {
            root.SetActive(false);
        }

        private void OnDisable()
        {
            GameManager_Protoescape.OnPrototypeInitiated -= PrototypeInitiatedHandler;
            PrototypeTester.OnPrototypeTestInitiated -= PrototypeTestInitiatedHandler;
            PrototypeTester.OnPrototypeTestCompleted -= PrototypeTestCompletedHandler;
        }
    }
}