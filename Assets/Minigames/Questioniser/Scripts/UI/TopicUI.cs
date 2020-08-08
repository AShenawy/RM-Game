using TMPro;
using UnityEngine;

namespace Methodyca.Minigames.Questioniser
{
    public class TopicUI : MonoBehaviour
    {
        [SerializeField] GameObject storyPanel;
        [SerializeField] TextMeshProUGUI topicText;

        void Start()
        {
            GameManager.Instance.OnTopicChanged += TopicChangedHandler;
            GameManager.Instance.OnStoryInitiated += StoryInitiatedHandler;
        }

        void StoryInitiatedHandler(bool status)// FIX LATER
        {
            if (status)
                storyPanel.SetActive(true);
        }

        void TopicChangedHandler(Topic topic)
        {
            topicText.text = topic.Name;

            if (topic.IsStoryInitiated)
                storyPanel.SetActive(true);
        }

        void OnDisable()
        {
            if (GameManager.InstanceExists)
            {
                GameManager.Instance.OnTopicChanged -= TopicChangedHandler;
                GameManager.Instance.OnStoryInitiated -= StoryInitiatedHandler;
            }
        }
    }
}