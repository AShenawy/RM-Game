using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Methodyca.Minigames.DocStudy
{
    public class UIForum : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI remainingText;
        [SerializeField] private Button finishButton;
        [SerializeField] private Button[] threads;

        private Button _selectedThreadButton;
        private Thread[] _threads;

        public void SelectThread(int id)
        {
            for (int i = 0; i < _threads.Length; i++)
            {
                if (_threads[i].Id == id)
                {
                    _selectedThreadButton = threads[i];
                    GameManager.Instance.HandlePostInitiation(_threads[i]);
                }
            }
        }

        private void OnEnable()
        {
            GameManager.OnForumInitiated += ForumInitiatedHandler;
            GameManager.OnPostCompleted += PostCompletedHandler;

            finishButton.onClick.AddListener(() => GameManager.Instance.GetFeedback());
        }

        private void Start()
        {
            UpdateForumThread();
        }

        private void UpdateForumThread()
        {
            finishButton.gameObject.SetActive(false);

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].GetComponentInChildren<TextMeshProUGUI>().text = _threads[i].Title;
            }
        }

        private void ForumInitiatedHandler(Question question)
        {
            _threads = question.Threads;
        }

        private void PostCompletedHandler(int current, int max)
        {
            if (_selectedThreadButton != null)
            {
                _selectedThreadButton.interactable = false;
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