using TMPro;
using UnityEngine;

namespace Methodyca.Minigames.Questioniser
{
    public class QuizGridUI : MonoBehaviour
    {
        [SerializeField] RectTransform gridPanel;
        [SerializeField] RectTransform tickPrefab;
        [SerializeField] TextMeshProUGUI[] topicTexts;
        [SerializeField] TextMeshProUGUI[] cardTexts;

        public void EndDialog()
        {
            GameManager.Instance.HandleStoryDialog();
        }

        void Start()
        {
            GameManager.Instance.OnQuizGridUpdated += QuizGridUpdatedHandler;
        }

        void QuizGridUpdatedHandler(string currentTopicName, string currentCardName)
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
                GameManager.Instance.OnQuizGridUpdated -= QuizGridUpdatedHandler;
        }
    }
}