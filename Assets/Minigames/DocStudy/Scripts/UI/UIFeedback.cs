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

        // main game connection
        [SerializeField] private GameObject winAndQuitButton;

        private readonly string _emailTitle = "Re: Game Jam Post-mortem";
        private readonly string _feedbackForLessCorrect = "Unfortunately, our editorial team has found that the article does not " +
            "cite enough evidence to support the generalizations it makes about the subject. " +
            "As such, we regret to inform you that we have decided against publishing it.";
        private readonly string _feedbackForMediumCorrect = "While the article contains a lot of interesting information, " +
            "much of the evidence the article cites does not appear to be directly relevant to the subject at hand. " +
            "It was often difficult to make a connection between a specific quote and the overall message. " +
            "As such, we would like to ask you to revise the article and remove the unnecessary digressions, then submit it to us again.";
        private readonly string _feedbackForMoreCorrect = "We have read your article and enjoyed how your argumentation is grounded in actual forum quotes. " +
            "We are pleased to inform you that your article has been accepted for publication. Check out Gamathustra’s Article section tomorrow!";
        private readonly string _emailGratitude = "Best of luck with future game jams";

        private void OnEnable()
        {
            GameManager.OnFeedbackInitiated += ScoreUpdatedHandler;

            returnMenu.onClick.AddListener(() => GameManager.Instance.ResetData());
        }

        private void ScoreUpdatedHandler((int SelectedCorrectPosts, int TotalCorrectPosts, int SelectedCorrectThreads, int TotalCorrectThreads) score)
        {
            root.SetActive(true);

            threadScore.text = $"You selected <b>{score.SelectedCorrectThreads} out of {score.TotalCorrectThreads}</b> relevant threads";
            postScore.text = $"You selected <b>{score.SelectedCorrectPosts} out of {score.TotalCorrectPosts}</b> relevant posts across the selected threads";

            var ratio = score.SelectedCorrectPosts / (float)score.TotalCorrectPosts;

            if (ratio < 0.4f)
            {
                feedback.text = $"<b>{_emailTitle}</b>\n\n{_feedbackForLessCorrect}\n\n{_emailGratitude}";
            }
            else if (ratio >= 0.4f && ratio < 0.6f)
            {
                feedback.text = $"<b>{_emailTitle}</b>\n\n{_feedbackForMediumCorrect}\n\n{_emailGratitude}";
            }
            else
            {
                feedback.text = $"<b>{_emailTitle}</b>\n\n{_feedbackForMoreCorrect}\n\n{_emailGratitude}";
                
                // allow player to win game and quit to main game
                winAndQuitButton.SetActive(true);
            }
        }

        private void OnDisable()
        {
            GameManager.OnFeedbackInitiated -= ScoreUpdatedHandler;
        }
    }
}