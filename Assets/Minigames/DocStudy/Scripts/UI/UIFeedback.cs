using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
            GameManager.OnScoreUpdated += ScoreUpdatedHandler;

            restart.onClick.AddListener(() => GameManager.Instance.RestartGame());
            returnMenu.onClick.AddListener(() => GameManager.Instance.ReturnMenu());
        }

        private void ScoreUpdatedHandler((int SelectedCorrectPosts, int TotalCorrectPosts, int SelectedCorrectThreads, int TotalCorrectThreads) score)
        {
            root.SetActive(true);

            postScore.text = $"Post Choices: {score.SelectedCorrectPosts}/{score.TotalCorrectPosts}";
            threadScore.text = $"Forum Thread Choices:{score.SelectedCorrectThreads}/{score.TotalCorrectThreads}";
        }

        private void OnDisable()
        {
            GameManager.OnScoreUpdated -= ScoreUpdatedHandler;
        }
    }
}