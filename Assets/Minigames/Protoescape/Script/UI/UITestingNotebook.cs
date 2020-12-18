using UnityEngine;
using UnityEngine.UI;

namespace Methodyca.Minigames.Protoescape
{
    public class UITestingNotebook : MonoBehaviour
    {
        [SerializeField] private GameObject notebook;
        [SerializeField] private TMPro.TextMeshProUGUI feedbackText;
        [SerializeField] private TMPro.TextMeshProUGUI logText;
        [SerializeField] private Button nextButton;

        private void Awake()
        {
            PrototypeTester.OnSelectionPointed += SelectionPointedHandler;
            PrototypeTester.OnPrototypeTestInitiated += PrototypeTestInitiatedHandler;
            PrototypeTester.OnPrototypeTestCompleted += PrototypeTestCompletedHandler;
        }

        private void PrototypeTestCompletedHandler(string feedback)
        {
            notebook.SetActive(true);
            feedbackText.text = feedback;
        }

        private void PrototypeTestInitiatedHandler(string[] notes)
        {
            logText.text = "";
            for (int i = 0; i < notes.Length; i++)
            {
                logText.text += $"{notes[i]}~~~~~~~~~~~~~~~~~~~~~~\n";
            }

            nextButton.interactable = true;
        }

        private void SelectionPointedHandler(ICheckable checkable)
        {
            if (checkable == null)
            {
                nextButton.interactable = false;
            }
        }

        private void OnDestroy()
        {
            PrototypeTester.OnSelectionPointed -= SelectionPointedHandler;
            PrototypeTester.OnPrototypeTestInitiated -= PrototypeTestInitiatedHandler;
            PrototypeTester.OnPrototypeTestCompleted -= PrototypeTestCompletedHandler;
        }
    }
}