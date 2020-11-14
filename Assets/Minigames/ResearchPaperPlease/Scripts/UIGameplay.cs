using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

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
        [SerializeField] private CanvasGroup fixButtonCanvasGroup;

        private readonly Color _halfTransparent = new Color(1, 1, 1, 0.5f);

        private void OnEnable()
        {
            GameManager.OnLevelInitiated += LevelInitiatedHandler;
            GameManager.OnLevelOver += LevelOverHandler;
            GameManager.OnFix += FixHandler;
            GameManager.OnPaperDecided += PaperDecisionHandler;
            GameManager.OnPaperUpdated += PaperUpdatedHandler;
            GameManager.OnProgressUpdated += ProgressUpdatedHandler;
            GameManager.OnQualityUpdated += QualityUpdatedHandler;
        }

        private void FixHandler(string feedback)
        {
            feedbackText.text = feedback;

            nextButton.interactable = true;
            nextButton.targetGraphic.raycastTarget = true;
            nextButton.targetGraphic.color = Color.white;

            fixButtonCanvasGroup.blocksRaycasts = false;
            fixButtonCanvasGroup.alpha = 0.5f;
        }

        private void LevelInitiatedHandler(LevelData levelData)
        {
            notebookText.text = levelData.LevelRules;
        }

        private void LevelOverHandler(string feedback)
        {
            feedbackText.text = feedback;

            nextButton.interactable = false;
            nextButton.interactable = true;
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
                nextButton.targetGraphic.raycastTarget = true;
                nextButton.targetGraphic.color = Color.white;

                acceptButton.interactable = false;
            }
            else
            {
                rejectButton.interactable = false;

                fixButtonCanvasGroup.alpha = 1;
                fixButtonCanvasGroup.interactable = true;
                fixButtonCanvasGroup.blocksRaycasts = true;
            }

            rejectButton.targetGraphic.raycastTarget = false;
            rejectButton.targetGraphic.color = _halfTransparent;

            acceptButton.targetGraphic.raycastTarget = false;
            acceptButton.targetGraphic.color = _halfTransparent;
        }

        private void PaperUpdatedHandler(ResearchPaperData data)
        {
            paperText.text = $"{data.Field}\n{data.Title}\n{data.Author}\n{data.Supervisor}\n{data.ResearchGoal}\n{data.ResearchMethodology}\n{data.ResearchMethods}\n{data.ResearchQuestions}";
            feedbackText.text = data.Feedback;

            acceptButton.interactable = true;
            rejectButton.interactable = true;

            rejectButton.targetGraphic.raycastTarget = true;
            acceptButton.targetGraphic.raycastTarget = true;

            rejectButton.targetGraphic.color = Color.white;
            acceptButton.targetGraphic.color = Color.white;

            nextButton.interactable = false;
            nextButton.targetGraphic.raycastTarget = false;
            nextButton.targetGraphic.color = _halfTransparent;

            fixButtonCanvasGroup.blocksRaycasts = false;
            fixButtonCanvasGroup.alpha = 0.5f;
        }

        private void OnDisable()
        {
            GameManager.OnLevelInitiated -= LevelInitiatedHandler;
            GameManager.OnLevelOver -= LevelOverHandler;
            GameManager.OnFix -= FixHandler;
            GameManager.OnPaperUpdated -= PaperUpdatedHandler;
            GameManager.OnPaperDecided -= PaperDecisionHandler;
            GameManager.OnProgressUpdated -= ProgressUpdatedHandler;
            GameManager.OnQualityUpdated -= QualityUpdatedHandler;
        }
    }
}