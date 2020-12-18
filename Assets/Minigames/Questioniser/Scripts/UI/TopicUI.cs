using DG.Tweening;
using System;
using UnityEngine;

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
            GameManager.Instance.OnGameOver += GameOverHandler;
        }

        void GameOverHandler()
        {
            gameObject.SetActive(false);
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
            if (topic.IsStoryInitiated)
                storyPanel.SetActive(true);
        }

        void OnDestroy()
        {
            if (GameManager.InstanceExists)
            {
                GameManager.Instance.OnTopicChanged -= TopicChangedHandler;
                GameManager.Instance.OnStoryInitiated -= StoryInitiatedHandler;
                GameManager.Instance.OnGameOver -= GameOverHandler;
            }
        }
    }
}