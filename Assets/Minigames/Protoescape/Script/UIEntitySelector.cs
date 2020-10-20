using UnityEngine;

namespace Methodyca.Minigames.Protoescape
{
    public class UIEntitySelector : MonoBehaviour
    {
        private RectTransform _rect;

        private void Start()
        {
            _rect = GetComponent<RectTransform>();
            GameManager_Protoescape.OnSelected += SelectedHandler;
            gameObject.SetActive(false);
        }

        private void SelectedHandler(GameObject selection)
        {
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
        }
    }
}