using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Methodyca.Minigames.DocStudy
{
    [System.Serializable]
    public class Thread
    {
        public bool IsSelected;
        public int Id;
        public string Title;
        public Selection[] Selections;
    }

    [System.Serializable]
    public class Selection
    {
        public bool IsCorrect;
        public string Text;
    }

    public class UIForum : MonoBehaviour
    {
        [SerializeField] private GameObject threadPrefab;
        [SerializeField] private TextMeshProUGUI remainingText;

        private Thread[] _threads;

        public void SelectThread(int id)
        {

        }

        private void OnEnable()
        {
            _threads = GameManager.Instance.CurrentQuestion.Threads;
            //remainingText.text = $"Selections ({}/{_threads.Length})";
            UpdateForumThreads();
        }

        private void UpdateForumThreads()
        {
            foreach (var item in _threads)
            {
                var thread = Instantiate(threadPrefab);

                thread.GetComponentInChildren<TextMeshProUGUI>().text = item.Title;
            }
        }
    }

    public class UIThread : MonoBehaviour, IPointerClickHandler
    {
        private const float _selectedAlpha = 0.75f;

        private CanvasGroup _canvasGroup;
        private bool _isClicked;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _isClicked = !_isClicked;

            if (_isClicked)
            {
                _canvasGroup.alpha = _selectedAlpha;
            }
            else
            {
                _canvasGroup.alpha = 1;
            }
        }
    }

    public class UIPost : MonoBehaviour
    {
        private Question _current;

        private void OnEnable()
        {
            _current = GameManager.Instance.CurrentQuestion;
        }
    }

    public class GameManager : Singleton<GameManager>
    {
        public Question CurrentQuestion { get; private set; }

        public void InitiateForumThread(Question question)
        {
            CurrentQuestion = question;
        }

        public void SelectForumThread()
        {

        }
    }
}