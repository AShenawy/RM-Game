using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Methodyca.Minigames.DocStudy
{
    public class UIPost : MonoBehaviour
    {
        [SerializeField] private GameObject root;
        [SerializeField] private Transform selectedArea;
        [SerializeField] private Transform discardedArea;
        [SerializeField] private Button doneButton;
        [SerializeField] private TextMeshProUGUI profileName;
        [SerializeField] private TextMeshProUGUI profilePost;
        [SerializeField] private Image profileImage;

        private UISelection[] uiSelections;
        private Post _post;

        public void Select(UISelection uISelection)
        {
            uISelection.transform.SetParent(selectedArea);
        }

        public void Discard(UISelection uISelection)
        {
            uISelection.transform.SetParent(discardedArea);
        }

        private void OnEnable()
        {
            GameManager.OnPostInitiated += PostInitiatedHandler;
            doneButton.onClick.AddListener(DoneClickHandler);
            uiSelections = GetComponentsInChildren<UISelection>();
        }

        private void DoneClickHandler()
        {
            GameManager.Instance.HandlePostCompletion();
            doneButton.onClick.RemoveAllListeners();
        }

        private void PostInitiatedHandler(Post post)
        {
            _post = post;

            root.SetActive(true);
            profileName.text = post.ProfileName;
            profilePost.text = post.ProfilePost;
            profileImage.sprite = post.ProfileImage;

            AddPostSelections();
        }

        private void AddPostSelections()
        {
            for (int j = 0; j < uiSelections.Length; j++)
            {
                uiSelections[j].gameObject.SetActive(false);
            }

            for (int i = 0; i < _post.Selections.Length; i++)
            {
                uiSelections[i].gameObject.SetActive(true);
                uiSelections[i].Initialize(_post.Selections[i]);

                _post.Selections[i].IsSelected = true;
                Select(uiSelections[i]);
            }
        }

        private void OnDisable()
        {
            GameManager.OnPostInitiated -= PostInitiatedHandler;
        }
    }
}