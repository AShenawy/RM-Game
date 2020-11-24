using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Methodyca.Minigames.ResearchPaperPlease
{
    public class UIFeedback : MonoBehaviour
    {
        [SerializeField] private Transform speechBubble;
        [SerializeField] private Image image;
        [SerializeField] private TextMeshProUGUI speech;

        private void Start()
        {
            GameManager.OnFeedbackInitiated += FeedbackInitiatedHandler;
            gameObject.SetActive(false);
        }

        private void FeedbackInitiatedHandler(Feedback feedback)
        {
            if (feedback == null)
            {
                speechBubble.gameObject.SetActive(false);
            }
            else
            {
                speechBubble.gameObject.SetActive(true);
                image.sprite = feedback.Character;
                speech.text = $"<b>{feedback.Name}:</b> {feedback.Speech}";
            }
        }

        private void OnDestroy()
        {
            GameManager.OnFeedbackInitiated -= FeedbackInitiatedHandler;
        }
    }
}