using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

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
                //speechBubble.DOScale(endValue: 0, duration: 0.1f).OnComplete(() => );
            }
            else
            {
                speechBubble.gameObject.SetActive(true);
                //speechBubble.DOScale(endValue: 1, duration: 0.1f).OnStart(() => );
                image.sprite = feedback.Character;
                speech.text = feedback.Speech;
            }
        }

        private void OnDestroy()
        {
            GameManager.OnFeedbackInitiated -= FeedbackInitiatedHandler;
        }
    }
}