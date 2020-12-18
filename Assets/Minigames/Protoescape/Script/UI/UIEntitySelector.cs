using UnityEngine;

namespace Methodyca.Minigames.Protoescape
{
    public class UIEntitySelector : MonoBehaviour
    {
        private RectTransform _rect;

        public void Select(GameObject selection)
        {
            if (selection == null)
            {
                gameObject.SetActive(false);
                return;
            }

            var selectedRect = selection.GetComponent<RectTransform>();

            gameObject.SetActive(true);
            _rect.sizeDelta = selectedRect.sizeDelta;
            _rect.position = selectedRect.position;
        }

        private void Awake()
        {
            _rect = GetComponent<RectTransform>();
            GameManager_Protoescape.OnSelected += SelectedHandler;
            GameManager_Protoescape.OnStackMove += StackMoveHandler;
        }

        private void StackMoveHandler(bool isMovable)
        {
            gameObject.SetActive(!isMovable);
        }

        private void SelectedHandler(GameObject selection)
        {
            Select(selection);
        }

        private void OnDestroy()
        {
            GameManager_Protoescape.OnSelected -= SelectedHandler;
            GameManager_Protoescape.OnStackMove -= StackMoveHandler;
        }
    }
}