using System;
using UnityEngine;

namespace Methodyca.Minigames.Questioniser
{
    [Serializable]
    public class Respond
    {
        public Dialog NextDialog;
        [TextArea(3, 10)] public string Text;
    }

    public class DialogManager : Singleton<DialogManager>
    {
        public event Action<Dialog> OnDialogInitiated = delegate { };

        Dialog _currentDialog;

        public void StartDialog(Dialog dialog)
        {
            _currentDialog = dialog;
            OnDialogInitiated?.Invoke(dialog);
        }

        public void TriggerRespondAt(int index)
        {
            if (index > _currentDialog.Responds.Length)
                return;

            if (_currentDialog.Responds[index].NextDialog == null)
            {
                GameManager.Instance.HandleStoryDialog();
                OnDialogInitiated?.Invoke(null);
            }
            else
            {
                StartDialog(_currentDialog.Responds[index].NextDialog);
            }
        }
    }
}