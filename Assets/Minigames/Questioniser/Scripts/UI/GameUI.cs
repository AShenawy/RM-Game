using TMPro;
using UnityEngine;

namespace Methodyca.Minigames.Questioniser
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI deckCountText;
        [SerializeField] TextMeshProUGUI actionPointText;

        private void OnEnable()
        {
            QuizManager.Instance.OnAnswerSelected += AnswerSelectedHandler;
        }

        private void AnswerSelectedHandler(Answer answer)
        {
            deckCountText.text = GameManager.Instance.GetDeckSize.ToString();
        }
        private void OnDisable()
        {
            QuizManager.Instance.OnAnswerSelected -= AnswerSelectedHandler;
        }
    }
}