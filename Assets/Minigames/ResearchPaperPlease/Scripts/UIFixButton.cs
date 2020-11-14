using UnityEngine;
using UnityEngine.UI;

namespace Methodyca.Minigames.ResearchPaperPlease
{
    [RequireComponent(typeof(Button))]
    public class UIFixButton : MonoBehaviour
    {
        [SerializeField] private char optionIndex;
        [SerializeField] private CanvasGroup canvasGroup;

        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void Start()
        {
            GameManager.OnLevelInitiated += LevelInitiatedHandler;
            _button.onClick.AddListener(ClickHandler);
        }

        private void LevelInitiatedHandler(LevelData data)
        {
            gameObject.SetActive(false);

            foreach (var item in data.ActiveOptionsToFix)
            {
                if (item == optionIndex)
                {
                    gameObject.SetActive(true);
                }
            }
        }

        private void ClickHandler()
        {
            GameManager.Instance.HandleFixingOption(optionIndex);
        }

        private void OnDestroy()
        {
            GameManager.OnLevelInitiated -= LevelInitiatedHandler;
            _button.onClick.RemoveAllListeners();
        }
    }
}