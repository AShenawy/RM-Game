using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;

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
        [SerializeField] private Sprite progressGlowingSprite; // Sprite for glowing effect
        [SerializeField] private Sprite qualityGlowingSprite;  // Sprite for glowing effect

        private int[] progressThresholds = { 4, 8, 12, 16, 20 };
        private int[] qualityPositiveThresholds = { 6, 12, 18, 24, 30 };
        private int[] qualityNegativeThresholds = { -6, -12, -18, -24, -30 };

        private void Start()
        {
            SetImageAlpha(qualityCrystalImage, 0.1f);
            qualityCrystalImage.sprite = qualityGlowingSprite;
            SetImageAlpha(progressCrystalImage, 0.1f);
            progressCrystalImage.sprite = progressGlowingSprite;
        }
        private void OnEnable()
        {
            GameManager.OnProgressUpdated += ProgressUpdatedHandler;
            GameManager.OnQualityUpdated += QualityUpdatedHandler;
        }

        private void QualityUpdatedHandler(int value)
        {
            float alpha = value < -30 ? 0.1f : (value > 30 ? 1.0f : Mathf.Lerp(0.1f, 1.0f, (value + 30) / 60f));
            SetImageAlpha(qualityCrystalImage, alpha);
            qualityCrystalImage.sprite = qualityGlowingSprite;
        }

        private void ProgressUpdatedHandler(int value)
        {
            float alpha = value == 0 ? 0.1f : (value > 20 ? 1.0f : Mathf.Lerp(0.1f, 1.0f, value / 20f));
            SetImageAlpha(progressCrystalImage, alpha);
            progressCrystalImage.sprite = progressGlowingSprite;
        }

        private void SetImageAlpha(Image image, float alpha)
        {
            Color color = image.color;
            color.a = alpha;
            image.color = color;
        }

        private void OnDisable()
        {
            GameManager.OnProgressUpdated -= ProgressUpdatedHandler;
            GameManager.OnQualityUpdated -= QualityUpdatedHandler;
        }
    }
}





