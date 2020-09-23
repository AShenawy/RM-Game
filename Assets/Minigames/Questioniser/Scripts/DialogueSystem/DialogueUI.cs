using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Methodyca.Minigames.Questioniser
{
    public class DialogueUI : MonoBehaviour
    {
        [SerializeField] GameObject root;
        [SerializeField] Image dateExpression;
        [SerializeField] GameObject dateDialogBubble;
        [SerializeField] Button[] respondButtons;

        TextMeshProUGUI _dateDialogText;

        void Start()
        {
            _dateDialogText = dateDialogBubble.GetComponentInChildren<TextMeshProUGUI>();
            DialogManager.Instance.OnDialogInitiated += DialogueInitiatedHandler;
        }

        void DialogueInitiatedHandler(Dialog dialogue)
        {
            if (dialogue == null)
            {
                root.SetActive(false);
            }
            else
            {
                root.SetActive(true);

                if (string.IsNullOrEmpty(dialogue.Text))
                {
                    dateDialogBubble.SetActive(false);
                }
                else
                {
                    dateDialogBubble.SetActive(true);
                    _dateDialogText.text = dialogue.Text;
                }

                var responds = dialogue.Responds;

                foreach (var button in respondButtons)
                    button.gameObject.SetActive(false);

                for (int i = 0; i < responds.Length; i++)
                {
                    respondButtons[i].gameObject.SetActive(true);
                    respondButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = responds[i].Text;
                }
            }
        }

        void OnDestroy()
        {
            if (DialogManager.InstanceExists)
                DialogManager.Instance.OnDialogInitiated -= DialogueInitiatedHandler;
        }
    }
}