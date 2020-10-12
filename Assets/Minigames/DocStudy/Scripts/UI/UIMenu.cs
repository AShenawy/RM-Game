using UnityEngine;

namespace Methodyca.Minigames.DocStudy
{
    public class UIMenu : MonoBehaviour
    {
        [SerializeField] private GameObject root;

        private void OnEnable()
        {
            GameManager.OnForumInitiated += ForumInitiatedHandler;
        }

        private void ForumInitiatedHandler(Question question)
        {
            root.SetActive(false);
        }

        private void OnDisable()
        {
            GameManager.OnForumInitiated -= ForumInitiatedHandler;
        }
    }
}