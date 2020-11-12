using UnityEngine;
using UnityEngine.UI;

namespace Methodyca.Minigames.ResearchPaperPlease
{
    [RequireComponent(typeof(Button))]
    public class UIFixButton : MonoBehaviour
    {
        [SerializeField] private char optionIndex;

        private Button _button;

        public void SetInteractable(bool value)
        {
            _button.interactable = value;
        }

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(ClickHandler);
        }

        private void ClickHandler()
        {
            GameManager.Instance.HandleFixOptions(optionIndex);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveAllListeners();
        }
    }
}