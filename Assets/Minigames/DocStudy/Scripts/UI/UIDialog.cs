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
        [SerializeField] private Sprite nextIcon;
        [SerializeField] private Sprite endIcon;

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
            speech.text = dialog.Speech;


            // Start the fade-in and then the typing effect
            DOTween.Sequence().AppendCallback(() => canvasGroup.alpha = 0)
                              .Append(DOTween.To(() => canvasGroup.alpha, a => canvasGroup.alpha = a, 1, 0.25f));

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

        private void ClickNextHandler()
        {
            DOTween.To(() => canvasGroup.alpha, a => canvasGroup.alpha = a, 0, 0.25f)
                   .OnComplete(() =>
                   {
                       DialogManager.Instance.TriggerDialog();
                   });
        }

        private void ClickPreviousHandler()
        {
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


