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

        private void OnEnable()
        {
            GameManager.OnProgressUpdated += ProgressUpdatedHandler;
            GameManager.OnQualityUpdated += QualityUpdatedHandler;
        }

        private void QualityUpdatedHandler(int value)
        {
            // Check for any threshold
            if (qualityPositiveThresholds.Contains(value) || qualityNegativeThresholds.Contains(value))
            {
                ApplyGlowEffect(qualityCrystalImage, qualityGlowingSprite);
            }
            else
            {
                qualityCrystalImage.sprite = value > GameManager.Instance.QualityValueToWin ? qualityChargedCrystal : qualityUnchargedCrystal;
            }
        }

        private void ProgressUpdatedHandler(int value)
        {
            // Check if the value is one of the thresholds
            if (progressThresholds.Contains(value))
            {
                ApplyGlowEffect(progressCrystalImage, progressGlowingSprite);
            }
            else
            {
                progressCrystalImage.sprite = value > GameManager.Instance.ProgressValueToWin ? progressChargedCrystal : progressUnchargedCrystal;
            }
        }

        private void ApplyGlowEffect(Image crystalImage, Sprite glowingSprite)
        {
            crystalImage.sprite = glowingSprite;
            crystalImage.transform
                .DOShakePosition(duration: 0.5f, strength: 8 * Vector2.one, vibrato: 50, fadeOut: false);
        }

        private void OnDisable()
        {
            GameManager.OnProgressUpdated -= ProgressUpdatedHandler;
            GameManager.OnQualityUpdated -= QualityUpdatedHandler;
        }
    }
}





