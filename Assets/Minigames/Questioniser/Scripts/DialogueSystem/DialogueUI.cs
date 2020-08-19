using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Methodyca.Minigames.Questioniser
{
    public class DialogueUI : MonoBehaviour
    {
        [SerializeField] GameObject root;
        [SerializeField] TextMeshProUGUI speechText;
        [SerializeField] Button[] respondButtons;

        void Start()
        {
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
                speechText.text = dialogue.Text;
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