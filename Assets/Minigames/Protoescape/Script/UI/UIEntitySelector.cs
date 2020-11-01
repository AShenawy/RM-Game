﻿using UnityEngine;

namespace Methodyca.Minigames.Protoescape
{
    public class UIEntitySelector : MonoBehaviour
    {
        private RectTransform _rect;

        private void Start()
        {
            _rect = GetComponent<RectTransform>();

            GameManager_Protoescape.OnSelected += SelectedHandler;
            GameManager_Protoescape.OnStackMove += StackMoveHandler;

            gameObject.SetActive(false);
        }

        private void StackMoveHandler(bool isMovable)
        {
            gameObject.SetActive(!isMovable);
        }

        private void SelectedHandler(GameObject selection)
        {
            if (selection == null)
            {
                gameObject.SetActive(false);
                return;
            }

            var selectedRect = selection.GetComponent<RectTransform>();

            gameObject.SetActive(true);

            _rect.SetParent(selectedRect);
            _rect.anchoredPosition = selectedRect.anchoredPosition;
            _rect.sizeDelta = selectedRect.sizeDelta;
            _rect.localPosition = Vector2.zero;
        }

        private void OnDestroy()
        {
            GameManager_Protoescape.OnSelected -= SelectedHandler;
            GameManager_Protoescape.OnStackMove -= StackMoveHandler;
        }
    }
}