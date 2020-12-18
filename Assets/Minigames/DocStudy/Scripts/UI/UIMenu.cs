using UnityEngine;

namespace Methodyca.Minigames.DocStudy
{
    public class UIMenu : MonoBehaviour
    {
        [SerializeField] private GameObject root;
        [SerializeField] private GameObject questionPanel;
        [SerializeField] private GameObject menuButton;

        private void OnEnable()
        {
            GameManager.OnForumInitiated += ForumInitiatedHandler;
            DialogManager.OnDialogCompleted += DialogCompletedHandler;
        }

        private void DialogCompletedHandler(Dialog lastDialog)
        {
            questionPanel.SetActive(true);
            menuButton.SetActive(true);
        }

        private void ForumInitiatedHandler(Question question)
        {
            root.SetActive(false);
        }

        private void OnDisable()
        {
            GameManager.OnForumInitiated -= ForumInitiatedHandler;
            DialogManager.OnDialogCompleted -= DialogCompletedHandler;
        }
    }
}