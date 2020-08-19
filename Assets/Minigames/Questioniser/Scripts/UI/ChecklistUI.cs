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

        void Start()
        {
            GameManager.Instance.OnChecklistUpdated += ChecklistUpdatedHandler;
            GameManager.Instance.OnTopicClosed += TopicClosedHandler;
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
            }
        }
    }
}