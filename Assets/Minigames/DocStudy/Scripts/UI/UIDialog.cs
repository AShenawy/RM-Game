using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

namespace Methodyca.Minigames.DocStudy
{
    public class UIDialog : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Image character;
        [SerializeField] private TextMeshProUGUI speech;
        [SerializeField] private Button nextButton;
        [SerializeField] private Button previousButton;

        private void OnEnable()
        {
            DialogManager.OnDialogUpdated += DialogUpdatedHandler;
            nextButton.onClick.AddListener(ClickNextHandler);
            previousButton.onClick.AddListener(ClickPreviousHandler);
        }

        private void DialogUpdatedHandler(Dialog dialog)
        {
            if (dialog == null)
            {
                return;
            }

            character.sprite = dialog.Character;
            speech.text = dialog.Speech;

            DOTween.Sequence().AppendCallback(() => canvasGroup.alpha = 0)
                              .Append(DOTween.To(() => canvasGroup.alpha, a => canvasGroup.alpha = a, 1, 0.25f));

            previousButton.interactable = DialogManager.Instance.HasPreviousDialog();

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

        private void OnDisable()
        {
            DialogManager.OnDialogUpdated -= DialogUpdatedHandler;
            nextButton.onClick.RemoveAllListeners();
            previousButton.onClick.RemoveAllListeners();
        }
    }
}