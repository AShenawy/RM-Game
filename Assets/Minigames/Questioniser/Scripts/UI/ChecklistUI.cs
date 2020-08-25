using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Methodyca.Minigames.Questioniser
{
    public class ChecklistUI : MonoBehaviour
    {
        [SerializeField] RectTransform gridPanel;
        [SerializeField] RectTransform tickPrefab;
        [SerializeField] RectTransform crossOutPrefab;
        [SerializeField] TextMeshProUGUI[] topicTexts;
        [SerializeField] TextMeshProUGUI[] cardTexts;

        Canvas _checkListCanvas;

        void Start()
        {
            _checkListCanvas = gridPanel.GetComponentInParent<Canvas>();

            GameManager.Instance.OnChecklistUpdated += ChecklistUpdatedHandler;
            GameManager.Instance.OnTopicClosed += TopicClosedHandler;
            GameManager.Instance.OnGameOver += GameOverHandler;
        }

        void GameOverHandler()
        {
            DOTween.Sequence().Append(_checkListCanvas.transform.DOMove(Vector2.zero, 0.5f)).Join(_checkListCanvas.transform.DORotate(Vector3.zero,0.5f));
        }

        void TopicClosedHandler(Topic topic)
        {
            foreach (var t in topicTexts)
            {
                if (t.text == topic.Name)
                {
                    var line = Instantiate(crossOutPrefab, gridPanel);
                    line.anchoredPosition = new Vector2(0, t.rectTransform.anchoredPosition.y);
                }
            }
        }

        void ChecklistUpdatedHandler(string currentTopicName, string currentCardName)
        {
            Debug.Log("Topic: " + currentTopicName + " - Card: " + currentCardName);
            for (int i = 0; i < topicTexts.Length; i++)
            {
                if (topicTexts[i].text == currentTopicName)
                {
                    for (int j = 0; j < cardTexts.Length; j++)
                    {
                        if (cardTexts[j].text == currentCardName)
                        {
                            var tick = Instantiate(tickPrefab, gridPanel);
                            tick.anchoredPosition = new Vector2(cardTexts[j].rectTransform.anchoredPosition.x, topicTexts[i].rectTransform.anchoredPosition.y);
                        }
                    }
                }
            }
        }

        void OnDestroy()
        {
            if (GameManager.InstanceExists)
            {
                GameManager.Instance.OnChecklistUpdated -= ChecklistUpdatedHandler;
                GameManager.Instance.OnTopicClosed -= TopicClosedHandler;
                GameManager.Instance.OnGameOver -= GameOverHandler;
            }
        }
    }
}