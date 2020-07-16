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
            QuizManager.OnAnswerSelected += AnswerSelectedHandler;
        }

        private void AnswerSelectedHandler(Answer answer)
        {
            deckCountText.text = GameManager.Instance.GetDeckSize.ToString();
        }
        private void OnDisable()
        {
            QuizManager.OnAnswerSelected -= AnswerSelectedHandler;
        }
    }
}