using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

namespace Methodyca.Minigames.DocStudy
{
    public class UIPostPanel : MonoBehaviour
    {
        [SerializeField] private GameObject root;
        [SerializeField] private UIPost selectedPostPrefab;
        [SerializeField] private Transform selectedArea;
        [SerializeField] private Transform discardedArea;
        [SerializeField] private Button selectButton;
        [SerializeField] private Button discardButton;
        [SerializeField] private Button completeButton;
        [SerializeField] private GameObject postPanel;
        [SerializeField] private Image profileImage;
        [SerializeField] private TextMeshProUGUI profileName;
        [SerializeField] private TextMeshProUGUI profilePost;
        [SerializeField] private TextMeshProUGUI threadTitle;
        [SerializeField] private TextMeshProUGUI questionTitle;

        private Queue<Post> _allPosts;

        public void Select(UIPost uISelection)
        {
            uISelection.transform.SetParent(selectedArea);
        }

        public void Discard(UIPost uISelection)
        {
            uISelection.transform.SetParent(discardedArea);
        }

        private void OnEnable()
        {
            GameManager.OnPostInitiated += PostInitiatedHandler;
            selectButton.onClick.AddListener(SelectClickHandler);
            discardButton.onClick.AddListener(DiscardClickHandler);
            completeButton.onClick.AddListener(CompleteClickHandler);
        }

        private void CompleteClickHandler()
        {
            root.SetActive(false);
            GameManager.Instance.HandlePostCompletion();
        }

        private void PostInitiatedHandler(string question, Thread thread)
        {
            root.SetActive(true);
            postPanel.SetActive(true);
            completeButton.gameObject.SetActive(false);

            questionTitle.text = $"<b>Research question</b>: {question}";
            threadTitle.text = $"<b>Selected forum thread</b>: {thread.Title}";

            _allPosts = new Queue<Post>();

            foreach (var item in thread.Posts)
            {
                _allPosts.Enqueue(item);
            }

            ClearPosts();
            DisplayPostDataAtPeek();
        }

        private void ClearPosts()
        {
            var discardedPosts = discardedArea.GetComponentsInChildren<UIPost>();
            var selectedPosts = selectedArea.GetComponentsInChildren<UIPost>();

            foreach (var d in discardedPosts)
            {
                Destroy(d.gameObject);
            }

            foreach (var s in selectedPosts)
            {
                Destroy(s.gameObject);
            }
        }

        private void DiscardClickHandler()
        {
            HandlePostSelection(false, discardedArea);
        }

        private void SelectClickHandler()
        {
            HandlePostSelection(true, selectedArea);
        }

        private void HandlePostSelection(bool isSelected, Transform content)
        {
            var post = _allPosts.Dequeue();

            if (post == null)
            {
                return;
            }

            if (_allPosts.Count == 0)
            {
                postPanel.SetActive(false);
                completeButton.gameObject.SetActive(true);
            }

            post.IsSelected = isSelected;

            var spawned = Instantiate(selectedPostPrefab, content);
            spawned.Initialize(post);
            DisplayPostDataAtPeek();
        }

        private void DisplayPostDataAtPeek()
        {
            if (_allPosts.Count > 0)
            {
                var next = _allPosts.Peek();
                profileName.text = next.Name;
                profilePost.text = next.Message;
                profileImage.sprite = next.ProfileImage;
            }
        }

        private void OnDisable()
        {
            GameManager.OnPostInitiated -= PostInitiatedHandler;
            selectButton.onClick.RemoveAllListeners();
            discardButton.onClick.RemoveAllListeners();
            completeButton.onClick.RemoveAllListeners();
        }
    }
}