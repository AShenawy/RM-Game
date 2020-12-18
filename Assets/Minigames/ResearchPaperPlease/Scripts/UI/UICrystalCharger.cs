using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Methodyca.Minigames.ResearchPaperPlease
{
    public class UICrystalCharger : MonoBehaviour
    {
        [SerializeField] private Image progressCrystalImage;
        [SerializeField] private Image qualityCrystalImage;
        [SerializeField] private Sprite progressChargedCrystal;
        [SerializeField] private Sprite progressUnchargedCrystal;
        [SerializeField] private Sprite qualityChargedCrystal;
        [SerializeField] private Sprite qualityUnchargedCrystal;

        private void OnEnable()
        {
            GameManager.OnProgressUpdated += ProgressUpdatedHandler;
            GameManager.OnQualityUpdated += QualityUpdatedHandler;
        }

        private void QualityUpdatedHandler(int value)
        {
            if (value > GameManager.Instance.QualityValueToWin)
            {
                qualityCrystalImage.sprite = qualityChargedCrystal;
                qualityCrystalImage.transform.DOShakePosition(duration: 0.5f, strength: 8 * Vector2.one, vibrato: 50, fadeOut: false);
            }
            else
            {
                qualityCrystalImage.sprite = qualityUnchargedCrystal;
            }
        }

        private void ProgressUpdatedHandler(int value)
        {
            if (value > GameManager.Instance.ProgressValueToWin)
            {
                progressCrystalImage.sprite = progressChargedCrystal;
                progressCrystalImage.transform.DOShakePosition(duration: 0.5f, strength: 8 * Vector2.one, vibrato: 50, fadeOut: false);
            }
            else
            {
                progressCrystalImage.sprite = progressUnchargedCrystal;
            }
        }

        private void OnDisable()
        {
            GameManager.OnProgressUpdated -= ProgressUpdatedHandler;
            GameManager.OnQualityUpdated -= QualityUpdatedHandler;
        }
    }
}