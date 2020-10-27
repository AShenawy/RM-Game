using UnityEngine;

namespace Methodyca.Minigames.Protoescape
{
    public class UIFeedback : MonoBehaviour
    {
        private void OnEnable()
        {
            PrototypeTester.OnPrototypeTested += PrototypeTestedHandler;
        }

        private void PrototypeTestedHandler(int current, int total)
        {

        }

        private void OnDisable()
        {
            
        }
    }
    public class UIDiary : MonoBehaviour
    {

    }
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
        }
    }
}