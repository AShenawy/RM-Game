using System;
using System.Collections.Generic;
using UnityEngine;

namespace Methodyca.Minigames.Questioniser
{
    [Serializable]
    public struct Question
    {
        public string QuestionText;
        public Answer[] Answers;
    }

    [Serializable]
    public struct Answer
    {
        public string AnswerText;
        public int Score;
        public bool IsCorrect;
    }

    public class QuizManager : Singleton<QuizManager>
    {
        [SerializeField] GameObject quizObject;
        [SerializeField] List<Question> questions;

        public Question CurrentQuestion { get; set; }
        public event Action<Answer> OnAnswerSelected = delegate { };

        Queue<Question> _availableQuestions = new Queue<Question>();

        public void SetQuizQuestion()
        {
            if (_availableQuestions.Count > 0)
                CurrentQuestion = _availableQuestions.Dequeue();

            quizObject.SetActive(true);
        }

        public void SelectAnswer(Answer answer)
        {
            OnAnswerSelected?.Invoke(answer);
            quizObject.SetActive(false);
        }

        void Start()
        {
            for (int i = 0; i < questions.Count; i++)
                _availableQuestions.Enqueue(questions[i]);
        }
    }
}