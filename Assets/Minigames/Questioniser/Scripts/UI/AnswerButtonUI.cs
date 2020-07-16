using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Methodyca.Minigames.Questioniser
{
    public class AnswerButtonUI : MonoBehaviour
    {
        [SerializeField] TextMeshPro answerText;
        [SerializeField] Button button;

        Answer _answer;

        public void SetButton(Answer answer)
        {
            _answer = answer;
            answerText.text = answer.AnswerText;
        }

        void OnEnable()
        {
            button.onClick.AddListener(() => QuizManager.Instance.SelectAnswer(_answer));
        }

        void OnDisable()
        {
            button.onClick.RemoveAllListeners();
        }
    }
}