using System.Collections.Generic;
using UnityEngine;

namespace Methodyca.Minigames.DocStudy
{
    public class DialogManager : Singleton<DialogManager>
    {
        public static event System.Action<Dialog> OnDialogUpdated = delegate { };
        public static event System.Action<Dialog> OnDialogCompleted = delegate { };

        [SerializeField] private GameObject dialogPanel;
        [SerializeField] private Dialog[] dialogs;

        private Queue<Dialog> _dialogQueue = new Queue<Dialog>();

        private void Start()
        {
            for (int i = 0; i < dialogs.Length; i++)
            {
                _dialogQueue.Enqueue(dialogs[i]);
            }

            TriggerDialog();
        }

        public void TriggerDialog()
        {
            if (_dialogQueue.Count > 0)
            {
                var dialog = _dialogQueue.Dequeue();

                dialogPanel.SetActive(true);
                OnDialogUpdated?.Invoke(dialog);

                if (_dialogQueue.Count <= 0)
                    OnDialogCompleted?.Invoke(dialog);
            }
        }
    }
}