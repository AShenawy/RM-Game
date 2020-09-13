using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Methodyca.Minigames.Questioniser
{
    public class QuizUI : MonoBehaviour
    {
        [SerializeField] GameObject root;
        [SerializeField] Image topicCardImage;
        [SerializeField] Image header;
        [SerializeField] Image feedback;
        [SerializeField] Button doneButton;
        [SerializeField] Image[] options;
        [SerializeField] TextMeshProUGUI[] answerTexts = new TextMeshProUGUI[3];

        Option _selectedOption;
        Option[] _options;

        public void ClickHandler(int id) // Called from the editor
        {
            for (int i = 0; i < _options.Length; i++)
            {
                if (id == _options[i].Id)
                {
                    _selectedOption = _options[i];
                    if (_selectedOption.IsCorrect)
                    {
                        options[i].color = Color.green;
                    }
                    else
                    {
                        options[i].color = Color.red;
                    }

                    feedback.gameObject.SetActive(true);
                    feedback.sprite = _selectedOption.Feedback;

                    doneButton.gameObject.SetActive(true);
                    doneButton.onClick.AddListener(DoneClickHandler);
                }
            }
        }

        void DoneClickHandler()
        {
            GameManager.Instance.HandleItemCardQuestionFor(_selectedOption);
            feedback.gameObject.SetActive(false);
            root.SetActive(false);
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
            _options = question.Options;
            root.SetActive(true);
            //header.sprite = question.Header;

            for (int i = 0; i < question.Options.Length; i++)
                answerTexts[i].text = question.Options[i].Text;
        }

        void OnDestroy()
        {
            doneButton.onClick.RemoveAllListeners();
            if (GameManager.InstanceExists)
            {
                GameManager.Instance.OnQuestionAsked -= QuestionAskedHandle;
                GameManager.Instance.OnTopicChanged -= TopicChangedHandler;
            }
        }
    }
}