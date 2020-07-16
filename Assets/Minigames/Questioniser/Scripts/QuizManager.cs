using System.Collections.Generic;

namespace Methodyca.Minigames.Questioniser
{
    [System.Serializable]
    public struct Question
    {
        public string QuestionText;
        public Answer[] Answers;
    }

    [System.Serializable]
    public struct Answer
    {
        public string AnswerText;
        public int Score;
        public bool IsCorrect;
    }

    public class QuizManager : Singleton<QuizManager>
    {
        public List<Question> questions;
        public Question CurrentQuestion { get; set; }
        public static event System.Action<Answer> OnAnswerSelected = delegate { };

        public void SetQuiz()
        {

        }

        public void SelectAnswer(Answer answer)
        {
            OnAnswerSelected?.Invoke(answer);
        }
    }
}