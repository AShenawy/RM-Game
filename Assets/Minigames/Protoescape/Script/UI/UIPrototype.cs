using UnityEngine;

namespace Methodyca.Minigames.Protoescape
{
    public class UIPrototype : MonoBehaviour
    {
        [SerializeField] private GameObject root;
        [SerializeField] private GameObject alienHand;

        private void OnEnable()
        {
            GameManager_Protoescape.OnPrototypeInitiated += PrototypeInitiatedHandler;
            PrototypeTester.OnPrototypeTestInitiated += PrototypeTestInitiatedHandler;
            PrototypeTester.OnPrototypeTestCompleted += PrototypeTestCompletedHandler;
        }

        private void PrototypeTestCompletedHandler(int current, int total)
        {
            root.SetActive(false);
            alienHand.SetActive(false);
        }

        private void PrototypeTestInitiatedHandler()
        {
            root.SetActive(true);
            alienHand.SetActive(true);
        }

        private void PrototypeInitiatedHandler()
        {
            root.SetActive(true);
            alienHand.SetActive(false);
        }

        private void OnDisable()
        {
            GameManager_Protoescape.OnPrototypeInitiated -= PrototypeInitiatedHandler;
            PrototypeTester.OnPrototypeTestInitiated -= PrototypeTestInitiatedHandler;
            PrototypeTester.OnPrototypeTestCompleted -= PrototypeTestCompletedHandler;
        }
    }
}