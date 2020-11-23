using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Methodyca.Minigames.ResearchPaperPlease
{
    public class UIGameOver : MonoBehaviour
    {
        [SerializeField] private string areYouSureWinText;
        [SerializeField] private string areYouSureLoseText;
        [SerializeField] private GameObject finalScreen;
        [SerializeField] private Image character;
        [SerializeField] private TextMeshProUGUI feedbackText;
        [SerializeField] private TextMeshProUGUI areYouSureText;

        private bool _isWon;

        public void Accept()
        {
            if (_isWon)
            {
                //Research Plan Mode ON
            }
            else
            {
                //Restart Game
            }
        }

        public void Reject()
        {
            // Quit
        }

        private void OnEnable()
        {
            GameManager.OnGameOver += GameOverHandler;
        }

        private void GameOverHandler(bool isWon, Feedback feedback)
        {
            _isWon = isWon;
            finalScreen.SetActive(true);
            character.sprite = feedback.Character;
            feedbackText.text = feedback.Speech;

            if (isWon)
            {
                areYouSureText.text = areYouSureWinText;
            }
            else
            {
                areYouSureText.text = areYouSureLoseText;
            }
        }

        private void OnDisable()
        {
            GameManager.OnGameOver -= GameOverHandler;
        }
    }
}