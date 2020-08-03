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

        void OnEnable() => button.onClick.AddListener(() => GameManager.Instance.HandleItemCardQuestionFor(_answer));
        void OnDisable() => button.onClick.RemoveAllListeners();
    }
}