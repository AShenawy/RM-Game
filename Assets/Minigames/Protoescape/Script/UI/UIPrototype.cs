using UnityEngine;
using UnityEngine.UI;

namespace Methodyca.Minigames.Protoescape
{
    public class UIPrototype : MonoBehaviour
    {
        [SerializeField] private GameObject root;
        [SerializeField] private GameObject selector;
        [SerializeField] private GameObject alienHand;
        [SerializeField] private GraphicRaycaster graphicRaycaster;

        private void OnEnable()
        {
            GameManager_Protoescape.OnPrototypeInitiated += PrototypeInitiatedHandler;
            PrototypeTester.OnPrototypeTestInitiated += PrototypeTestInitiatedHandler;
            PrototypeTester.OnPrototypeTestCompleted += PrototypeTestCompletedHandler;
        }

        private void Start()
        {
            root.SetActive(false);
        }

        private void PrototypeTestCompletedHandler(string feedback)
        {
            root.SetActive(false);
            alienHand.SetActive(false);
            graphicRaycaster.enabled = true;
        }

        private void PrototypeTestInitiatedHandler(string[] notes)
        {
            root.SetActive(true);
            alienHand.SetActive(true);
            graphicRaycaster.enabled = false;
        }

        private void PrototypeInitiatedHandler()
        {
            root.SetActive(true);
            selector.SetActive(false);
            alienHand.SetActive(false);

            graphicRaycaster.enabled = true;
            GameManager_Protoescape.SelectedEntity = null;
        }

        private void OnDisable()
        {
            GameManager_Protoescape.OnPrototypeInitiated -= PrototypeInitiatedHandler;
            PrototypeTester.OnPrototypeTestInitiated -= PrototypeTestInitiatedHandler;
            PrototypeTester.OnPrototypeTestCompleted -= PrototypeTestCompletedHandler;
        }
    }
}