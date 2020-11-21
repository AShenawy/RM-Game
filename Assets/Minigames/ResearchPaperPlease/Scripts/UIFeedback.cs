﻿using UnityEngine;
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
                speechBubble.DOScale(endValue: 0, duration: 0.1f).OnComplete(() => speechBubble.gameObject.SetActive(false));
            }
            else
            {

                speechBubble.DOScale(endValue: 1, duration: 0.1f).OnStart(() => speechBubble.gameObject.SetActive(true));
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