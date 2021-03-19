using UnityEngine;
using TMPro;
using Methodyca.Core;

namespace Methodyca.Minigames.ResearchPaperPlease
{
    public class UIGameOver : MonoBehaviour
    {
        [SerializeField] private string areYouSureWinText;
        [SerializeField] private string areYouSureLoseText;
        [SerializeField] private GameObject finalScreen;
        [SerializeField] private TextMeshProUGUI areYouSureText;

        private bool _isWon;

        public void Accept()
        {
            if (_isWon)
            {
                //Research Plan Mode ON

                // Allow game to be won on quit
                GameManager.Instance.DisplayWinGameButton();
            }
            else
            {
                GameManager.Instance.HandleRestartGame();
            }
        }

        public void Reject()
        {
            //Quit
            GetComponent<ReturnToMainGame>().QuitMinigame();
        }

        private void OnEnable()
        {
            GameManager.OnGameOver += GameOverHandler;
            GameManager.OnLevelInitiated += LevelInitiatedHandler;
        }

        private void LevelInitiatedHandler(LevelData data)
        {
            finalScreen.SetActive(false);
        }

        private void GameOverHandler(bool isWon)
        {
            _isWon = isWon;
            finalScreen.SetActive(true);

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
            GameManager.OnLevelInitiated -= LevelInitiatedHandler;
        }
    }
}