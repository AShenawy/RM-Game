using TMPro;
using UnityEngine;

namespace Methodyca.Minigames.Questioniser
{
    public class QuizUI : MonoBehaviour
    {
        [SerializeField] Transform answerHolder;
        [SerializeField] TextMeshProUGUI questionText;

        AnswerButtonUI[] _answerButtons;

        void Awake() => _answerButtons = answerHolder.GetComponentsInChildren<AnswerButtonUI>();

        public void SetQuiz(Question question)
        {
            var answers = question.Answers;
            questionText.text = question.QuestionText;

            foreach (var button in _answerButtons)
                button.gameObject.SetActive(false);

            for (int i = 0; i < answers.Length; i++)
            {
                _answerButtons[i].gameObject.SetActive(true);
                _answerButtons[i].SetOption(answers[i]);
            }
        }
    }
}