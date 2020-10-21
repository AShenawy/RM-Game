using System.Collections.Generic;
using UnityEngine;

namespace Methodyca.Minigames.DocStudy
{
    public class DialogManager : Singleton<DialogManager>
    {
        public static event System.Action<Dialog> OnDialogUpdated = delegate { };
        public static event System.Action<Dialog> OnDialogCompleted = delegate { };

        [SerializeField] private GameObject dialogPanel;
        [SerializeField] private Dialog initialForumDialog;
        [SerializeField] private Dialog initialPostDialog;
        [SerializeField] private Dialog finalDialog;
        [SerializeField] private Dialog[] initialDialogs;

        private bool _isPostDialogInitiated = false;
        private Queue<Dialog> _dialogQueue = new Queue<Dialog>();

        public void TriggerDialog()
        {
            if (_dialogQueue.Count > 0)
            {
                var dialog = _dialogQueue.Dequeue();

                dialogPanel.SetActive(true);
                OnDialogUpdated?.Invoke(dialog);

                if (_dialogQueue.Count <= 0)
                {
                    _dialogQueue = new Queue<Dialog>();
                    OnDialogCompleted?.Invoke(dialog);
                }
            }
            else
            {
                dialogPanel.SetActive(false);
            }
        }

        public void TriggerDialog(Dialog dialog)
        {
            dialogPanel.SetActive(false);
            dialogPanel.SetActive(true);
            OnDialogUpdated?.Invoke(dialog);
        }

        public void SetNewDialogQueue(Dialog[] dialogs)
        {
            for (int i = 0; i < dialogs.Length; i++)
            {
                _dialogQueue.Enqueue(dialogs[i]);
            }
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