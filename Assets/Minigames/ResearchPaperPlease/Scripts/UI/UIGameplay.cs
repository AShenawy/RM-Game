using System.Collections.Generic;
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
        [SerializeField] private TextMeshProUGUI pageText;
        [SerializeField] private Button acceptButton;
        [SerializeField] private Button rejectButton;
        [SerializeField] private Button nextButton;
        [SerializeField] private Button scrollUpButton;
        [SerializeField] private Button scrollDownButton;
        [SerializeField] private CanvasGroup fixButtonCanvasGroup;

        private int _currentPageIndex = 1;
        private ResearchPaperData _currentPaperData;
        private readonly Color _halfTransparent = new Color(1, 1, 1, 0.5f);

        public void ScrollUp()
        {
            if (--_currentPageIndex <1)
            {
                _currentPageIndex = 1;
            }

            paperText.pageToDisplay = _currentPageIndex;
            scrollUpButton.interactable = _currentPageIndex != 1;
            scrollDownButton.interactable = true;
        }

        public void ScrollDown()
        {
            if (++_currentPageIndex > paperText.textInfo.pageCount)
            {
                _currentPageIndex = paperText.textInfo.pageCount;
            }

            paperText.pageToDisplay = _currentPageIndex;
            scrollDownButton.interactable = _currentPageIndex != paperText.textInfo.pageCount;
            scrollUpButton.interactable = true;
        }

        private void OnEnable()
        {
            GameManager.OnLevelInitiated += LevelInitiatedHandler;
            GameManager.OnLevelOver += LevelOverHandler;
            GameManager.OnFix += FixHandler;
            GameManager.OnOptionHighlighted += OptionHighlightedHandler;
            GameManager.OnPageUpdated += PageUpdatedHandler; ;
            GameManager.OnPaperDecided += PaperDecidedHandler;
            GameManager.OnPaperUpdated += PaperUpdatedHandler;
            GameManager.OnProgressUpdated += ProgressUpdatedHandler;
            GameManager.OnQualityUpdated += QualityUpdatedHandler;
            GameManager.OnGameOver += GameOverHandler;
        }

        private void PageUpdatedHandler(int current, int max)
        {
            pageText.gameObject.SetActive(true);
            pageText.text = $"Paper {current}/{max}";
        }

        private void GameOverHandler(bool isWon)
        {
            paperText.gameObject.SetActive(false);

            rejectButton.gameObject.SetActive(false);
            acceptButton.gameObject.SetActive(false);

            nextButton.targetGraphic.raycastTarget = false;
            nextButton.targetGraphic.color = _halfTransparent;

            fixButtonCanvasGroup.alpha = 0.5f;
            fixButtonCanvasGroup.blocksRaycasts = false;

            scrollUpButton.targetGraphic.raycastTarget = false;
            scrollUpButton.targetGraphic.color = _halfTransparent;

            scrollDownButton.targetGraphic.raycastTarget = false;
            scrollDownButton.targetGraphic.color = _halfTransparent;
        }

        private void LevelInitiatedHandler(LevelData levelData)
        {
            pageText.gameObject.SetActive(false);
            paperText.gameObject.SetActive(true);
            acceptButton.gameObject.SetActive(true);
            rejectButton.gameObject.SetActive(true);

            paperText.text = $"<align=\"center\"><b>LEVEL {levelData.Level}</b></align>\n{levelData.LevelInitiatedMessage}";

            acceptButton.targetGraphic.raycastTarget = false;
            acceptButton.targetGraphic.color = _halfTransparent;

            rejectButton.targetGraphic.raycastTarget = false;
            rejectButton.targetGraphic.color = _halfTransparent;

            fixButtonCanvasGroup.alpha = 0.5f;
            fixButtonCanvasGroup.blocksRaycasts = false;

            scrollUpButton.targetGraphic.raycastTarget = false;
            scrollUpButton.targetGraphic.color = _halfTransparent;

            scrollDownButton.targetGraphic.raycastTarget = false;
            scrollDownButton.targetGraphic.color = _halfTransparent;

            nextButton.interactable = true;
            nextButton.targetGraphic.raycastTarget = true;
            nextButton.targetGraphic.color = Color.white;
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
        }

        private void OptionHighlightedHandler(Dictionary<char, bool> fixbuttons)
        {
            paperText.text = "";

            foreach (var option in _currentPaperData.Options)
            {
                if (char.IsWhiteSpace(option.Index))
                {
                    paperText.text += $"<b>{option.Header}:</b> {option.Text}\n";
                }
                else
                {
                    if (fixbuttons[option.Index])
                    {
                        paperText.text += $"<mark=#000000aa><font=\"VT323\"><b>{option.Index}) {option.Header}:</b> {option.Text}</mark>\n";
                    }
                    else
                    {
                        paperText.text += $"<b>{option.Index}) {option.Header}:</b> {option.Text}\n";
                    }
                }
            }

            paperText.ForceMeshUpdate(true);
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

            paperText.ForceMeshUpdate(true);
            paperText.pageToDisplay = 1;

            if (paperText.textInfo.pageCount > 1)
            {
                scrollUpButton.targetGraphic.raycastTarget = true;
                scrollUpButton.targetGraphic.color = Color.white;

                scrollDownButton.targetGraphic.raycastTarget = true;
                scrollDownButton.targetGraphic.color = Color.white;
            }
            else
            {
                scrollDownButton.targetGraphic.raycastTarget = false;
                scrollDownButton.targetGraphic.color = _halfTransparent;

                scrollUpButton.targetGraphic.raycastTarget = false;
                scrollUpButton.targetGraphic.color = _halfTransparent;
            }

            scrollUpButton.interactable = false;
            scrollDownButton.interactable = true;

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
            paperText.text = $"<align=\"center\">{message}</align>";
            smallScreenText.text = "";

            pageText.gameObject.SetActive(false);
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
            GameManager.OnOptionHighlighted -= OptionHighlightedHandler;
            GameManager.OnPageUpdated -= PageUpdatedHandler; ;
            GameManager.OnFix -= FixHandler;
            GameManager.OnPaperDecided -= PaperDecidedHandler;
            GameManager.OnPaperUpdated -= PaperUpdatedHandler;
            GameManager.OnProgressUpdated -= ProgressUpdatedHandler;
            GameManager.OnQualityUpdated -= QualityUpdatedHandler;
            GameManager.OnGameOver -= GameOverHandler;
        }
    }
}