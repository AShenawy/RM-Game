using System.Collections.Generic;
using UnityEngine;

namespace Methodyca.Minigames.DocStudy
{
    public class DialogManager : Singleton<DialogManager>
    {
        public static event System.Action<Dialog> OnDialogUpdated = delegate { };
        public static event System.Action<Dialog> OnDialogCompleted = delegate { };
        public static event System.Action OnNextClicked = delegate { };
        public static event System.Action OnPreviousClicked = delegate { };

        [SerializeField] private GameObject dialogPanel;
        [SerializeField] private Dialog initialForumDialog;
        [SerializeField] private Dialog initialPostDialog;
        [SerializeField] private Dialog finalDialog;
        [SerializeField] private Dialog[] initialDialogs;

        private bool _isPostDialogInitiated = false;
        private bool _isRevisiting = false;
        //private Queue<Dialog> _dialogQueue = new Queue<Dialog>();
        //private Stack<Dialog> _dialogHistory = new Stack<Dialog>();
        private List<Dialog> _dialogList = new List<Dialog>(); // List to replace Queue
        private int _currentDialogIndex = -1;

        public void TriggerDialog()
        {
            // Check if we can advance to the next dialog
            if (_currentDialogIndex < _dialogList.Count - 1)
            {
                _currentDialogIndex++; // Move to the next dialog
                var dialog = _dialogList[_currentDialogIndex];

                dialogPanel.SetActive(true);
                OnDialogUpdated?.Invoke(dialog);

                // Check if we are at the last dialog
                if (_currentDialogIndex >= _dialogList.Count - 1)
                {
                    OnDialogCompleted?.Invoke(dialog);
                }
            }
            else
            {
                dialogPanel.SetActive(false);
            }

            OnNextClicked?.Invoke();
        }

        public void TriggerDialog(Dialog dialog)
        {
            dialogPanel.SetActive(false);
            dialogPanel.SetActive(true);
            OnDialogUpdated?.Invoke(dialog);
        }

        public void TriggerPreviousDialog()
        {
           
            if (_currentDialogIndex > 0)
            {
                _currentDialogIndex--; // Move to the previous dialog
                var dialog = _dialogList[_currentDialogIndex];

                dialogPanel.SetActive(true);
                OnDialogUpdated?.Invoke(dialog);
            }

            OnPreviousClicked?.Invoke();
        }
    

    public bool HasPreviousDialog()
    {
        return _currentDialogIndex > 0;
    }
    

        public bool NoMoreDialogs()
        {
            return _currentDialogIndex >= _dialogList.Count - 1;
        }

        // New method to get the current dialog
        public Dialog GetCurrentDialog()
        {
            if (_currentDialogIndex >= 0 && _currentDialogIndex < _dialogList.Count)
            {
                return _dialogList[_currentDialogIndex];
            }
            return null;
        }

        public void SetNewDialogQueue(Dialog[] dialogs)
        {
            _dialogList.Clear(); // Clear any existing dialogs
            _dialogList.AddRange(dialogs); // Add new dialogs
            _currentDialogIndex = -1; // Reset index to before the first dialog
        }

        private void Start()
        {
            SetNewDialogQueue(initialDialogs);
            TriggerDialog();
        }

        private void OnEnable()
        {
            GameManager.OnForumInitiated += ForumInitiatedHandler;
            GameManager.OnPostInitiated += PostInitiatedHandler;
            GameManager.OnFeedbackInitiated += FeedbackInitiatedHandler;
        }

        private void FeedbackInitiatedHandler((int SelectedCorrectPosts, int TotalCorrectPosts, int SelectedCorrectThreads, int TotalCorrectThreads) score)
        {
            TriggerDialog(finalDialog);
        }

        private void PostInitiatedHandler(string question, Thread thread)
        {
            if (!_isPostDialogInitiated)
            {
                _isPostDialogInitiated = true;
                TriggerDialog(initialPostDialog);
            }
        }

        private void ForumInitiatedHandler(Question question)
        {
            _isPostDialogInitiated = false;
            TriggerDialog(initialForumDialog);
        }

        private void OnDisable()
        {
            GameManager.OnForumInitiated -= ForumInitiatedHandler;
            GameManager.OnPostInitiated -= PostInitiatedHandler;
            GameManager.OnFeedbackInitiated -= FeedbackInitiatedHandler;
        }
    }
}
