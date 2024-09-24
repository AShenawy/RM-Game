using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Methodyca.Minigames.DocStudy
{
    public class UIFeedback : MonoBehaviour
    {
        private bool isShowingSelections = false;
        [SerializeField] private GameObject root;
        [SerializeField] private Button returnMenu;
        [SerializeField] private Button seeSelections;
        [SerializeField] private Button closeSelections;
        [SerializeField] private GameObject selectionPanel;
        [SerializeField] private TextMeshProUGUI threadScore;
        [SerializeField] private TextMeshProUGUI postScore;
        [SerializeField] private TextMeshProUGUI feedback;
        [SerializeField] private TextMeshProUGUI correctSelections;
        [SerializeField] private RectTransform areaRectTransform;
        [SerializeField] private Slider selectionsSlider;
        

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

            seeSelections.onClick.AddListener(ShowSelectionPanel);
            closeSelections.onClick.AddListener(ShowSelectionPanel);
            selectionsSlider.onValueChanged.AddListener(ToDrag);
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

            DisplayCorrectSelections();
        }

        private void DisplayCorrectSelections()
        {
            var currentQuestion = GameManager.Instance.GetCurrentQuestion();

            if (currentQuestion == null)
            {
                correctSelections.text = "No correct selections found.";
                return;
            }

            string correctSelectionDetails = $"Your research question was:\n{currentQuestion.Title}\n\n";

            int selectedCorrectThreads = 0;
            foreach (var thread in currentQuestion.Threads)
            {
                if (thread.IsCorrect && thread.IsCompleted)
                {
                    selectedCorrectThreads++;
                }
            }
            correctSelectionDetails += $"You selected {selectedCorrectThreads}/{currentQuestion.Threads.Length} suitable threads:\n\n";

            int threadIndex = 1;
            foreach (var thread in currentQuestion.Threads)
            {
                if (thread.IsCorrect && thread.IsCompleted)
                {
                    correctSelectionDetails += $"{threadIndex})  {thread.Title}\n";
                    threadIndex++;
                }
            }

            correctSelectionDetails += $"\nYou selected {GameManager.Instance.GetSelectedCorrectPostsCount()}/{GameManager.Instance.GetTotalCorrectPostsCount()} suitable posts:\n\n";

            foreach (var thread in currentQuestion.Threads)
            {
                bool hasCorrectPosts = false;
                string threadPostsDetails = $"{thread.Title}\n";

                int postIndex = 1;
                foreach (var post in thread.Posts)
                {
                    if (post.IsCorrect && post.IsSelected)
                    {
                        threadPostsDetails += $"{postIndex})  {post.Message}\n\n";
                        //threadPostsDetails += $"Post by: {post.Name}\n\n";
                        postIndex++;
                        hasCorrectPosts = true;
                    }
                }

                if (hasCorrectPosts)
                {
                    correctSelectionDetails += threadPostsDetails;
                }
            }

            if (correctSelectionDetails == $"Your research question was:\n {currentQuestion.Title}\n\n")
            {
                correctSelectionDetails += "None of your selections were correct.";
            }

            correctSelectionDetails += "\n———————END———————\n";

            correctSelections.text = correctSelectionDetails;
        }

        private void ShowSelectionPanel()
        {
            if (isShowingSelections == false)
            {
                selectionPanel.SetActive(true);
            }
            else
            {
                selectionPanel.SetActive(false);
            }
            isShowingSelections = !isShowingSelections;
        }
        private void ToDrag(float value)
        {
            //sliding value
            float tempSliderValue = value;
            //total slide height
            float tempTotalHeight = 5000;
            //set y
            areaRectTransform.anchoredPosition = new Vector2(areaRectTransform.anchoredPosition.x, tempSliderValue * tempTotalHeight);
        }

        private void OnDisable()
        {
            GameManager.OnFeedbackInitiated -= ScoreUpdatedHandler;
            seeSelections.onClick.RemoveListener(ShowSelectionPanel);
            closeSelections.onClick.RemoveListener(ShowSelectionPanel);
        }
    }
}