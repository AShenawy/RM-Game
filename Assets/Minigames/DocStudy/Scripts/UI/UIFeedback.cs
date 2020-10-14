using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Methodyca.Minigames.DocStudy
{
    public class UIFeedback : MonoBehaviour
    {
        [SerializeField] private GameObject root;
        [SerializeField] private Button restart;
        [SerializeField] private Button returnMenu;
        [SerializeField] private TextMeshProUGUI threadScore;
        [SerializeField] private TextMeshProUGUI postScore;

        private void OnEnable()
        {
            GameManager.OnFeedbackInitiated += ScoreUpdatedHandler;
            GameManager.OnRestartGame += RestartGameHandler;

            restart.onClick.AddListener(() => GameManager.Instance.RestartGame());
            returnMenu.onClick.AddListener(() => GameManager.Instance.ResetData());
        }

        private void RestartGameHandler()
        {
            root.SetActive(false);
        }

        private void ScoreUpdatedHandler((int SelectedCorrectPosts, int TotalCorrectPosts, int SelectedCorrectThreads, int TotalCorrectThreads) score)
        {
            root.SetActive(true);

            postScore.text = $"Post Choices: {score.SelectedCorrectPosts}/{score.TotalCorrectPosts}";
            threadScore.text = $"Forum Thread Choices:{score.SelectedCorrectThreads}/{score.TotalCorrectThreads}";
        }

        private void OnDisable()
        {
            GameManager.OnFeedbackInitiated -= ScoreUpdatedHandler;
            GameManager.OnRestartGame -= RestartGameHandler;
        }
    }
}