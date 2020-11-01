﻿using UnityEngine;
using UnityEngine.UI;

namespace Methodyca.Minigames.Protoescape
{
    public class UITestingNotebook : MonoBehaviour
    {
        [SerializeField] private TMPro.TextMeshProUGUI logText;
        [SerializeField] private Button likableButton;
        [SerializeField] private Button confusingButton;

        private void Awake()
        {
            PrototypeTester.OnSelectionPointed += SelectionPointedHandler;
            PrototypeTester.OnPrototypeTestInitiated += PrototypeTestInitiatedHandler;
        }

        private void PrototypeTestInitiatedHandler()
        {
            logText.text = "";
            likableButton.interactable = true;
            confusingButton.interactable = true;
        }

        private void SelectionPointedHandler(ICheckable checkable)
        {
            if (checkable == null)
            {
                likableButton.interactable = false;
                confusingButton.interactable = false;
            }
            else
            {
                logText.text += $"{checkable.GetNotebookLogData()}~~~~~~~~~~~~~~~~~~~~\n";
            }
        }

        private void OnDestroy()
        {
            PrototypeTester.OnSelectionPointed -= SelectionPointedHandler;
            PrototypeTester.OnPrototypeTestInitiated -= PrototypeTestInitiatedHandler;
        }
    }
}