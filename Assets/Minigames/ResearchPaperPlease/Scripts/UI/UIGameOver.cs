using UnityEngine;
using TMPro;
using Methodyca.Minigames.Utils;

namespace Methodyca.Minigames.ResearchPaperPlease
{
    public class UIGameOver : MonoBehaviour
    {
        [SerializeField] private string areYouSureWinText;
        [SerializeField] private string areYouSureLoseText;
        [SerializeField] private GameObject finalScreen;
        [SerializeField] private TextMeshProUGUI areYouSureText;
        [SerializeField] private SceneController sceneController;
        [SerializeField] private string mainSceneName;

        private bool _isWon;

        public void Accept()
        {
            if (_isWon)
            {
                //sceneController.ChangeScene(mainSceneName);

                // use minigame hookup scripts
                GetComponent<SortGame.WinMinigame>().CompleteSingleLoadedMinigame();
                GetComponent<Core.ReturnToMainGame>().QuitMinigame();
            }
            else
            {
                GameManager.Instance.HandleRestartGame();
            }
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