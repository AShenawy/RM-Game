using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Methodyca.Minigames.Questioniser
{
    public class QuizUI : MonoBehaviour
    {
        [SerializeField] GameObject root;
        [SerializeField] Image topicCardImage;
        [SerializeField] TextMeshProUGUI questionText;
        [SerializeField] TextMeshProUGUI[] answerTexts = new TextMeshProUGUI[3];

        Option[] _answers;

        public void ClickHandler(int id)
        {
            foreach (var answer in _answers)
            {
                if (id == answer.Id)
                {
                    GameManager.Instance.HandleItemCardQuestionFor(answer);
                    root.SetActive(false);
                }
            }
        }

        void Start()
        {
            GameManager.Instance.OnQuestionAsked += QuestionAskedHandle;
            GameManager.Instance.OnTopicChanged += TopicChangedHandler;
        }

        void TopicChangedHandler(Topic topic)
        {
            topicCardImage.sprite = topic.CardSprite;
        }

        void QuestionAskedHandle(Question question)
        {
            CardBase.IsClickable = false;
            _answers = question.Options;
            root.SetActive(true);
            questionText.text = question.OptionDescription;

            for (int i = 0; i < question.Options.Length; i++)
                answerTexts[i].text = question.Options[i].Text;
        }

        void OnDestroy()
        {
            if (GameManager.InstanceExists)
            {
                GameManager.Instance.OnQuestionAsked -= QuestionAskedHandle;
                GameManager.Instance.OnTopicChanged -= TopicChangedHandler;
            }
        }
    }
}