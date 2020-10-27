﻿using UnityEngine;
using UnityEngine.UI;

namespace Methodyca.Minigames.Protoescape
{
    public class UIJail : MonoBehaviour
    {
        [SerializeField] private RectTransform alien;
        [SerializeField] private Button prototypeButton;

        private void OnEnable()
        {
            prototypeButton.onClick.AddListener(ClickPrototypeHandler);
        }

        private void ClickPrototypeHandler()
        {
            GameManager_Protoescape.Instance.HandlePrototypeInitiation();
        }

        private void OnDisable()
        {
            prototypeButton.onClick.RemoveAllListeners();
        }
    }
}