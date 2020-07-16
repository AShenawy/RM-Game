using TMPro;
using UnityEngine;

namespace Methodyca.Minigames.Questioniser
{
    public class QuizUI : MonoBehaviour
    {
        [SerializeField] Transform answerHolder;
        [SerializeField] TextMeshPro questionText;

        AnswerButtonUI[] _answerButtons;

        void Awake()
        {
            _answerButtons = answerHolder.GetComponentsInChildren<AnswerButtonUI>();
        }

        void OnEnable()
        {
            SetQuizPanel();
        }

        void SetQuizPanel()
        {
            var currentQuestion = QuizManager.Instance.CurrentQuestion;
            var answers = currentQuestion.Answers;
            questionText.text = currentQuestion.QuestionText;

            foreach (var button in _answerButtons)
                button.gameObject.SetActive(false);

            for (int i = 0; i < answers.Length; i++)
            {
                _answerButtons[i].gameObject.SetActive(true);
                _answerButtons[i].SetButton(answers[i]);
            }
        }
    }
}