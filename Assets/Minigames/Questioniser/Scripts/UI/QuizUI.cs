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
        [SerializeField] GameObject profile;
        [SerializeField] Button doneButton;
        [SerializeField] Image[] options;
        [SerializeField] TextMeshProUGUI[] answerTexts = new TextMeshProUGUI[3];

        Option _selectedOption;
        Option[] _options;
        readonly Vector2 _optionPanelPaddingSize = new Vector2(75, 50);

        public Sound ClickSFX;

        public void ClickHandler(int id) // Called from the editor
        {
            for (int i = 0; i < _options.Length; i++)
            {
                if (id == _options[i].Id)
                {
                    _selectedOption = _options[i];

                    if (_selectedOption.IsCorrect)
                    {
                        options[i].color = new Color(0.3f, 0.92f, 0.43f, 1);
                        profile.SetActive(true);
                    }
                    else
                    {
                        options[i].color = new Color(0.9f, 0.4f, 0.45f, 1);
                        profile.SetActive(false);
                    }

                    feedback.gameObject.SetActive(true);
                    feedback.sprite = _selectedOption.Feedback;

                    doneButton.gameObject.SetActive(true);
                    doneButton.onClick.AddListener(DoneClickHandler);
                    break;
                }
            }
            SoundManager.instance.PlaySFX(ClickSFX);
            //Debug.Log("Call");
        }

        void DoneClickHandler()
        {
            foreach (var option in options)
                option.color = Color.white;

            GameManager.Instance.HandleItemCardQuestionFor(_selectedOption);
            feedback.gameObject.SetActive(false);
            doneButton.gameObject.SetActive(false);
            root.SetActive(false);
            doneButton.onClick.RemoveAllListeners();
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
            header.sprite = question.Header;

            for (int i = 0; i < question.Options.Length; i++)
            {
                answerTexts[i].text = question.Options[i].Text;
                answerTexts[i].ForceMeshUpdate();

                Vector2 textSize = answerTexts[i].GetRenderedValues(false);
                options[i].rectTransform.sizeDelta = textSize + _optionPanelPaddingSize;
            }
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