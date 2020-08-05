using TMPro;
using UnityEngine;

namespace Methodyca.Minigames.Questioniser
{
    public class QuizUI : MonoBehaviour
    {
        [SerializeField] GameObject root;
        [SerializeField] TextMeshProUGUI questionText;
        [SerializeField] TextMeshProUGUI[] answerTexts = new TextMeshProUGUI[3];

        Answer[] _answers;

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
        }

        void QuestionAskedHandle(Question question)
        {
            _answers = question.Answers;
            root.SetActive(true);
            questionText.text = question.QuestionText;

            for (int i = 0; i < question.Answers.Length; i++)
                answerTexts[i].text = question.Answers[i].AnswerText;
        }

        void OnDisable()
        {
            if (GameManager.InstanceExists)
                GameManager.Instance.OnQuestionAsked -= QuestionAskedHandle;
        }
    }
}