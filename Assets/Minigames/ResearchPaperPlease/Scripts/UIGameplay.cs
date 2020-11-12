using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Methodyca.Minigames.ResearchPaperPlease
{
    public class UIGameplay : MonoBehaviour
    {
        [SerializeField] private Transform qualityCursor;
        [SerializeField] private Transform progressCursor;
        [SerializeField] private TextMeshProUGUI paperText;
        [SerializeField] private TextMeshProUGUI feedbackText;
        [SerializeField] private TextMeshProUGUI notebookText;
        [SerializeField] private Button acceptButton;
        [SerializeField] private Button rejectButton;
        [SerializeField] private Button nextButton;
        [SerializeField] private UIFixButton[] fixButtons;

        private void OnEnable()
        {
            GameManager.OnFix += FixHandler;
            GameManager.OnPaperDecided += PaperDecisionHandler;
            GameManager.OnPaperUpdated += PaperUpdatedHandler;
            GameManager.OnProgressUpdated += ProgressUpdatedHandler;
            GameManager.OnQualityUpdated += QualityUpdatedHandler;
        }

        private void QualityUpdatedHandler(int value)
        {
            var rotation = Mathf.Rad2Deg * ((2 * Mathf.PI) - (Mathf.PI / GameManager.Instance.TotalPaperCount * value));
            var quaternion = Quaternion.AngleAxis(rotation, Vector3.forward);

            DOTween.Sequence().Append(qualityCursor.DORotateQuaternion(quaternion, 0.3f).SetEase(Ease.InCubic))
                              .Append(qualityCursor.DOShakeRotation(0.3f, 10 * Vector3.forward, 15 * value));
        }

        private void ProgressUpdatedHandler(int value)
        {
            var rotation = Mathf.Rad2Deg * ((2 * Mathf.PI) - (2 * Mathf.PI / GameManager.Instance.TotalPaperCount * value));
            var quaternion = Quaternion.AngleAxis(rotation, Vector3.forward);

            DOTween.Sequence().Append(progressCursor.DORotateQuaternion(quaternion, 0.3f).SetEase(Ease.InCubic))
                              .Append(progressCursor.DOShakeRotation(0.3f, 10 * Vector3.forward, 15 * value));
        }

        private void PaperDecisionHandler(bool isAccepted, ResearchPaperData data)
        {
            if (isAccepted)
            {
                nextButton.interactable = true;
            }
            else
            {
                //activate buttons
                for (int i = 0; i < fixButtons.Length; i++)
                {
                    fixButtons[i].SetInteractable(true);
                }
            }

            acceptButton.interactable = false;
            rejectButton.interactable = false;
        }

        private void PaperUpdatedHandler(ResearchPaperData data)
        {
            paperText.text = data.Field;

            acceptButton.interactable = true;
            rejectButton.interactable = true;
            nextButton.interactable = false;
        }

        private void FixHandler(string feedback)
        {
            feedbackText.text = feedback;
            nextButton.interactable = true;

            for (int i = 0; i < fixButtons.Length; i++)
            {
                fixButtons[i].SetInteractable(false);
            }
        }


        private void OnDisable()
        {
            GameManager.OnFix -= FixHandler;
            GameManager.OnPaperUpdated -= PaperUpdatedHandler;
            GameManager.OnPaperDecided -= PaperDecisionHandler;
            GameManager.OnProgressUpdated -= ProgressUpdatedHandler;
            GameManager.OnQualityUpdated -= QualityUpdatedHandler;
        }
    }
}