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
        [SerializeField] private TextMeshProUGUI smallScreenText;
        [SerializeField] private TextMeshProUGUI notebookText;
        [SerializeField] private Button acceptButton;
        [SerializeField] private Button rejectButton;
        [SerializeField] private Button nextButton;
        [SerializeField] private CanvasGroup fixButtonCanvasGroup;

        private ResearchPaperData _currentPaperData;
        private readonly Color _halfTransparent = new Color(1, 1, 1, 0.5f);

        private void OnEnable()
        {
            GameManager.OnLevelInitiated += LevelInitiatedHandler;
            GameManager.OnLevelOver += LevelOverHandler;
            GameManager.OnFix += FixHandler;
            GameManager.OnPaperDecided += PaperDecidedHandler;
            GameManager.OnPaperUpdated += PaperUpdatedHandler;
            GameManager.OnProgressUpdated += ProgressUpdatedHandler;
            GameManager.OnQualityUpdated += QualityUpdatedHandler;
        }

        private void LevelInitiatedHandler(LevelData levelData)
        {
            smallScreenText.text =$"<b>Level {levelData.Level}</b>\n{levelData.LevelInitiatedMessage}";

            rejectButton.interactable = false;
            rejectButton.targetGraphic.raycastTarget = false;
            rejectButton.targetGraphic.color = _halfTransparent;
        }

        private void PaperDecidedHandler(bool isAccepted)
        {
            if (isAccepted)
            {
                acceptButton.interactable = false;
                acceptButton.targetGraphic.raycastTarget = false;
                acceptButton.targetGraphic.color = _halfTransparent;
            }
            else
            {
                rejectButton.interactable = false;
                rejectButton.targetGraphic.raycastTarget = false;
                rejectButton.targetGraphic.color = _halfTransparent;
            }

            fixButtonCanvasGroup.alpha = 0.5f;
            fixButtonCanvasGroup.blocksRaycasts = false;

            nextButton.interactable = true;
            nextButton.targetGraphic.raycastTarget = true;
            nextButton.targetGraphic.color = Color.white;
        }

        private void FixHandler(bool isAnyActivated)
        {
            if (isAnyActivated)
            {
                acceptButton.targetGraphic.raycastTarget = false;
                acceptButton.targetGraphic.color = _halfTransparent;

                rejectButton.targetGraphic.raycastTarget = true;
                rejectButton.targetGraphic.color = Color.white;
            }
            else
            {
                acceptButton.targetGraphic.raycastTarget = true;
                acceptButton.targetGraphic.color = Color.white;

                rejectButton.targetGraphic.raycastTarget = false;
                rejectButton.targetGraphic.color = _halfTransparent;
            }

            //foreach (var option in _currentPaperData.Options)
            //{
            //    if (!char.IsWhiteSpace(option.Index))
            //    {
            //        paperText.text += $"<mark=#ffff00aa><b>{option.Index}) {option.Header}:</b></mark> {option.Text}\n";
            //    }
            //}
        }

        private void PaperUpdatedHandler(ResearchPaperData data)
        {
            _currentPaperData = data;

            paperText.text = "";

            foreach (var item in data.Options)
            {
                if (char.IsWhiteSpace(item.Index))
                {
                    paperText.text += $"<b>{item.Header}:</b> {item.Text}\n";
                }
                else
                {
                    paperText.text += $"<b>{item.Index}) {item.Header}:</b> {item.Text}\n";
                }
            }

            rejectButton.interactable = true;
            rejectButton.targetGraphic.raycastTarget = false;
            rejectButton.targetGraphic.color = _halfTransparent;

            acceptButton.interactable = true;
            acceptButton.targetGraphic.color = Color.white;
            acceptButton.targetGraphic.raycastTarget = true;

            nextButton.targetGraphic.raycastTarget = false;
            nextButton.targetGraphic.color = _halfTransparent;

            fixButtonCanvasGroup.alpha = 1;
            fixButtonCanvasGroup.blocksRaycasts = true;
        }

        private void LevelOverHandler(string message)
        {
            smallScreenText.text =  message;

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

        private void OnDisable()
        {
            GameManager.OnLevelInitiated -= LevelInitiatedHandler;
            GameManager.OnLevelOver -= LevelOverHandler;
            GameManager.OnFix -= FixHandler;
            GameManager.OnPaperDecided -= PaperDecidedHandler;
            GameManager.OnPaperUpdated -= PaperUpdatedHandler;
            GameManager.OnProgressUpdated -= ProgressUpdatedHandler;
            GameManager.OnQualityUpdated -= QualityUpdatedHandler;
        }
    }
}