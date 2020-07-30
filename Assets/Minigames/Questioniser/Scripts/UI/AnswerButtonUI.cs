using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Methodyca.Minigames.Questioniser
{
    public class AnswerButtonUI : MonoBehaviour
    {
        [SerializeField] Button button;
        [SerializeField] TextMeshProUGUI answerText;

        Answer _answer;

        public void SetOption(Answer answer)
        {
            _answer = answer;
            answerText.text = answer.AnswerText;
        }

        void OnEnable() => button.onClick.AddListener(ClickHandler);
        void OnDisable() => button.onClick.RemoveAllListeners();

        void ClickHandler()
        {
            GameManager.Instance.InterestPoint += _answer.Point;
            GameManager.Instance.QuizGUI.gameObject.SetActive(false);
        }
    }
}