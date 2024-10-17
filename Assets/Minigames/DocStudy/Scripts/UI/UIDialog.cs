using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System.Collections;

namespace Methodyca.Minigames.DocStudy
{
    public class UIDialog : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Image character;
        [SerializeField] private TextMeshProUGUI speech;
        [SerializeField] private Button nextButton;
        [SerializeField] private Button previousButton;
        [SerializeField] private AudioClip typeSoundClip; // AudioClip for the typewriter sound
        [SerializeField] private float typingSpeed = 0.05f; // Delay between each character
        [SerializeField] private Sprite nextIcon;
        [SerializeField] private Sprite endIcon;

        private AudioSource audioSource; // We'll create this at runtime
        private Coroutine typingCoroutine;

        private void Awake()
        {
            // Add or find the AudioSource component on this GameObject
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        private void OnEnable()
        {
            DialogManager.OnDialogUpdated += DialogUpdatedHandler;
            GameObjectActivator.OnGameObjectActivated += HidePreviousButton;
            GameObjectActivator.OnGameObjectDeactivated += ShowPreviousButton;

            nextButton.onClick.AddListener(ClickNextHandler);
            previousButton.onClick.AddListener(ClickPreviousHandler);
        }

        private void DialogUpdatedHandler(Dialog dialog)
        {
            if (dialog == null)
                return;

            character.sprite = dialog.Character;

            // Stop any existing typing effect
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
            }

            // Start the fade-in and then the typing effect
            DOTween.Sequence().AppendCallback(() => canvasGroup.alpha = 0)
                              .Append(DOTween.To(() => canvasGroup.alpha, a => canvasGroup.alpha = a, 1, 0.25f))
                              .OnComplete(() =>
                              {
                                  if (dialog.Speech != null)
                                  {
                                      typingCoroutine = StartCoroutine(TypeText(dialog.Speech));
                                  }
                              });

            if (DialogManager.Instance.HasPreviousDialog())
            {
                previousButton.gameObject.SetActive(true);
                previousButton.interactable = true;
            }
            else
            {
                previousButton.gameObject.SetActive(false);
            }

            if (DialogManager.Instance.NoMoreDialogs())
            {
                nextButton.image.sprite = endIcon;
            }
            else
            {
                nextButton.image.sprite = nextIcon;
            }
        }

        private IEnumerator TypeText(string text)
        {
            speech.text = "";
            foreach (char letter in text)
            {
                speech.text += letter;

                if (typeSoundClip != null && audioSource != null)
                {
                    audioSource.PlayOneShot(typeSoundClip); // Play the sound effect for each letter
                }

                yield return new WaitForSeconds(typingSpeed); // Wait between each letter
            }
        }

        private void ClickNextHandler()
        {
            // Stop the typing effect if it's ongoing and immediately display full text
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
                speech.text = DialogManager.Instance.GetCurrentDialog().Speech;
            }

            // Fade out the dialog before triggering the next one
            DOTween.To(() => canvasGroup.alpha, a => canvasGroup.alpha = a, 0, 0.25f)
                   .OnComplete(() =>
                   {
                       DialogManager.Instance.TriggerDialog();
                   });


        }

        private void ClickPreviousHandler()
        {

            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
                speech.text = DialogManager.Instance.GetCurrentDialog().Speech;
            }

            // Fade out the dialog before triggering the previous one
            DOTween.To(() => canvasGroup.alpha, a => canvasGroup.alpha = a, 0, 0.25f)
                   .OnComplete(() =>
                   {
                       DialogManager.Instance.TriggerPreviousDialog();
                   });

        }
        private void HidePreviousButton()
        {
            if (previousButton != null)
            {
                previousButton.gameObject.SetActive(false);
            }
        }

        private void ShowPreviousButton()
        {
            if (previousButton != null && DialogManager.Instance != null && DialogManager.Instance.HasPreviousDialog())
            {
                previousButton.gameObject.SetActive(true);
            }
        }

        private void OnDisable()
        {
            DialogManager.OnDialogUpdated -= DialogUpdatedHandler;
            GameObjectActivator.OnGameObjectActivated -= HidePreviousButton;
            GameObjectActivator.OnGameObjectDeactivated -= ShowPreviousButton;

            nextButton.onClick.RemoveAllListeners();
            previousButton.onClick.RemoveAllListeners();
        }
    }
}


