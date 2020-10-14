using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Methodyca.Minigames.DocStudy
{
    public class UIForum : MonoBehaviour
    {
        [SerializeField] private GameObject root;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private TextMeshProUGUI remainingText;
        [SerializeField] private Button finishButton;
        [SerializeField] private Button backButton;
        [SerializeField] private Button[] threadButtons;

        private Button _selectedThreadButton;
        private Thread[] _threads;

        public void SelectThread(int id)
        {
            for (int i = 0; i < _threads.Length; i++)
            {
                if (_threads[i].Id == id)
                {
                    _selectedThreadButton = threadButtons[i];
                    GameManager.Instance.HandlePostInitiation(_threads[i]);

                }
            }
        }

        private void OnEnable()
        {
            GameManager.OnForumInitiated += ForumInitiatedHandler;
            GameManager.OnPostInitiated += PostInitiatedHandler;
            GameManager.OnPostCompleted += PostCompletedHandler;

            finishButton.onClick.AddListener(HandleGameResult);
            backButton.onClick.AddListener(() => GameManager.Instance.ResetData());
        }

        private void HandleGameResult()
        {
            root.SetActive(false);
            GameManager.Instance.GetFeedback();
        }

        private void PostInitiatedHandler(string question, Thread thread)
        {
            root.SetActive(false);
        }

        private void ForumInitiatedHandler(Question question)
        {
            _threads = question.Threads;
            _selectedThreadButton = null;
            canvasGroup.blocksRaycasts = true;

            root.SetActive(true);
            finishButton.gameObject.SetActive(false);

            for (int i = 0; i < _threads.Length; i++)
            {
                threadButtons[i].interactable = true;
                threadButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = _threads[i].Title;
            }
        }

        private void PostCompletedHandler(int current, int max)
        {
            root.SetActive(true);

            if (_selectedThreadButton != null)
            {
                _selectedThreadButton.interactable = false;
            }

            if (current >= max)
            {
                finishButton.gameObject.SetActive(true);
                canvasGroup.blocksRaycasts = false;
            }

            remainingText.text = $"Thread completed ({current}/{max})";
        }

        private void OnDisable()
        {
            GameManager.OnForumInitiated -= ForumInitiatedHandler;
            GameManager.OnPostInitiated -= PostInitiatedHandler;
            GameManager.OnPostCompleted -= PostCompletedHandler;

            finishButton.onClick.RemoveAllListeners();
        }
    }
}