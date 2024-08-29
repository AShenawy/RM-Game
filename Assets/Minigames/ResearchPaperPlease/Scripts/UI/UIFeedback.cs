using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

namespace Methodyca.Minigames.ResearchPaperPlease
{
    public class UIFeedback : MonoBehaviour
    {
        [SerializeField] private Transform speechBubble;
        [SerializeField] private Image image;
        [SerializeField] private TextMeshProUGUI speech;
        [SerializeField] private float typeSpeed = 0.05f; // Speed of the typewriter effect
        [SerializeField] private AudioSource audioSource; // Audio source for playing sound effects
        [SerializeField] private AudioClip typeSound; // The sound effect for typing

        private void Start()
        {
            GameManager.OnFeedbackInitiated += FeedbackInitiatedHandler;
            gameObject.SetActive(false);
            image.gameObject.SetActive(false);
        }

        private void FeedbackInitiatedHandler(Feedback feedback)
        {
            if (feedback == null)
            {
                speechBubble.gameObject.SetActive(false);
                image.gameObject.SetActive(false);
                StopAllCoroutines(); // Stop any ongoing text animation
            }
            else
            {
                speechBubble.gameObject.SetActive(true);
                image.gameObject.SetActive(true);
                image.sprite = feedback.Character;
                StartCoroutine(AnimateText(feedback.Name, feedback.Speech));
            }
        }

        private IEnumerator AnimateText(string name, string fullText)
        {
            speech.text = $"<b>{name}:</b> "; // Start with the name
            foreach (char letter in fullText.ToCharArray())
            {
                speech.text += letter;

                if (typeSound != null && audioSource != null)
                {
                    audioSource.PlayOneShot(typeSound); // Play the typing sound effect
                }

                yield return new WaitForSeconds(typeSpeed);
            }
        }

        private void OnDestroy()
        {
            GameManager.OnFeedbackInitiated -= FeedbackInitiatedHandler;
        }
    }
}

