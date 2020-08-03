using System;
using TMPro;
using UnityEngine;

namespace Methodyca.Minigames.Questioniser
{
    public class QuizUI : MonoBehaviour
    {
        [SerializeField] Transform answerHolder;
        [SerializeField] TextMeshProUGUI questionText;
        [SerializeField] AnswerButtonUI[] answerButtons = new AnswerButtonUI[3];

        public void AskQuestion(Question question)
        {
            gameObject.SetActive(true);
            questionText.text = question.QuestionText;

            for (int i = 0; i < question.Answers.Length; i++)
                answerButtons[i].SetOption(question.Answers[i]);
        }
    }
}