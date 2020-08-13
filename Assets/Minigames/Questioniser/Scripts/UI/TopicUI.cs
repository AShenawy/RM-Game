using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Methodyca.Minigames.Questioniser
{
    public class TopicUI : MonoBehaviour
    {
        [SerializeField] GameObject storyPanel;
        [SerializeField] Image topicText;

        void Start()
        {
            GameManager.Instance.OnTopicChanged += TopicChangedHandler;
            GameManager.Instance.OnStoryInitiated += StoryInitiatedHandler;
        }

        void StoryInitiatedHandler(bool status)
        {
            storyPanel.SetActive(status);

            if(status)
                storyPanel.transform.DOShakePosition(duration: 0.75f, strength: 5, vibrato: 50, fadeOut: false);
        }

        void TopicChangedHandler(Topic topic)
        {
            topicText.sprite = topic.CardSprite;
            topicText.rectTransform.DOShakePosition(duration: 0.75f, strength: 5, vibrato: 50, fadeOut: false);
            if (topic.IsStoryInitiated)
                storyPanel.SetActive(true);
        }

        void OnDestroy()
        {
            if (GameManager.InstanceExists)
            {
                GameManager.Instance.OnTopicChanged -= TopicChangedHandler;
                GameManager.Instance.OnStoryInitiated -= StoryInitiatedHandler;
            }
        }
    }
}