using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Methodyca.Minigames.DocStudy
{
    public class UIFeedback : MonoBehaviour
    {
        [SerializeField] private GameObject root;
        [SerializeField] private Button returnMenu;
        [SerializeField] private TextMeshProUGUI threadScore;
        [SerializeField] private TextMeshProUGUI postScore;
        [SerializeField] private TextMeshProUGUI feedback;

        private readonly string _feedbackForLessCorrect = "Unfortunately, our editorial team has found that the article does not " +
            "cite enough evidence to support the generalizations it makes about the subject. " +
            "As such, we regret to inform you that we have decided against publishing it.";
        private readonly string _feedbackForMediumCorrect = "While the article contains a lot of interesting information, " +
            "much of the evidence the article cites does not appear to be directly relevant to the subject at hand. " +
            "It was often difficult to make a connection between a specific quote and the overall message. " +
            "As such, we would like to ask you to revise the article and remove the unnecessary digressions, then submit it to us again.";
        private readonly string _feedbackForMoreCorrect = "We have read your article and enjoyed how your argumentation is grounded in actual forum quotes. " +
            "We are pleased to inform you that your article has been accepted for publication. Check out Gamathustra’s Article section tomorrow!";

        private void OnEnable()
        {
            GameManager.OnFeedbackInitiated += ScoreUpdatedHandler;

            returnMenu.onClick.AddListener(() => GameManager.Instance.ResetData());
        }

        private void ScoreUpdatedHandler((int SelectedCorrectPosts, int TotalCorrectPosts, int SelectedCorrectThreads, int TotalCorrectThreads) score)
        {
            root.SetActive(true);

            threadScore.text = $"You selected {score.SelectedCorrectThreads} out of {score.TotalCorrectThreads} relevant threads";
            postScore.text = $"You selected {score.SelectedCorrectPosts} out of {score.TotalCorrectPosts} relevant posts across the selected threads";

            var ratio = score.SelectedCorrectPosts / (float)score.TotalCorrectPosts;

            if (ratio < 0.4f)
            {
                feedback.text = _feedbackForLessCorrect;
            }
            else if (ratio >= 0.4f && ratio < 0.6f)
            {
                feedback.text = _feedbackForMediumCorrect;
            }
            else
            {
                feedback.text = _feedbackForMoreCorrect;
            }
        }

        private void OnDisable()
        {
            GameManager.OnFeedbackInitiated -= ScoreUpdatedHandler;
        }
    }
}