using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Methodyca.Minigames.Questioniser
{
    public class TopicUI : MonoBehaviour
    {
        [SerializeField] GameObject storyPanel;
        [SerializeField] Transform closedCards;
        [SerializeField] SpriteRenderer topicSprite;

        void Start()
        {
            GameManager.Instance.OnTopicChanged += TopicChangedHandler;
            GameManager.Instance.OnStoryInitiated += StoryInitiatedHandler;
        }

        void StoryInitiatedHandler(bool status)
        {
            storyPanel.SetActive(status);

            if (status)
                storyPanel.transform.DOShakePosition(duration: 0.75f, strength: 5, vibrato: 50, fadeOut: false);
        }

        void TopicChangedHandler(Topic topic)
        {
            Sequence seq = DOTween.Sequence();
            seq.Append(topicSprite.transform.DOLocalMoveX(-3f, 0.2f))
                .Join(topicSprite.transform.DOLocalRotate(new Vector3(0, 0, 180), 0.2f))
                .AppendCallback(() => topicSprite.sprite = topic.CardSprite).SetDelay(0.2f)
                .Append(topicSprite.transform.DOLocalMoveX(3f, 0.2f))
                .Join(topicSprite.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.2f));
            //topicSprite.transform.DOShakePosition(duration: 0.75f, strength: 5, vibrato: 50, fadeOut: false);
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