using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Methodyca.Minigames.DocStudy
{
    public class UIForum : MonoBehaviour
    {
        [SerializeField] private GameObject root;
        [SerializeField] private TextMeshProUGUI remainingText;
        [SerializeField] private Button finishButton;
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

            finishButton.onClick.AddListener(() => GameManager.Instance.GetFeedback());
        }

        private void PostInitiatedHandler(string question, Thread thread)
        {
            root.SetActive(false);
        }

        private void UpdateForumThread()
        {
            finishButton.gameObject.SetActive(false);

            for (int i = 0; i < _threads.Length; i++)
            {
                threadButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = _threads[i].Title;
            }
        }

        private void ForumInitiatedHandler(Question question)
        {
            root.SetActive(true);
            _threads = question.Threads;
            UpdateForumThread();
        }

        private void PostCompletedHandler(int current, int max)
        {
            if (_selectedThreadButton != null)
            {
                _selectedThreadButton.interactable = false;
            }

            if (current >= max)
            {
                finishButton.gameObject.SetActive(true);
                // add disable all threads
            }

            remainingText.text = $"Thread completed ({current}/{max})";
        }

        private void OnDisable()
        {
            GameManager.OnForumInitiated -= ForumInitiatedHandler;
            GameManager.OnPostCompleted -= PostCompletedHandler;
        }
    }
}