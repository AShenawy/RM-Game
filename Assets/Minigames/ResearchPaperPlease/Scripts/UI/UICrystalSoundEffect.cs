using UnityEngine;

namespace Methodyca.Minigames.ResearchPaperPlease
{
    public class UICrystalSoundEffect : MonoBehaviour
    {
        [SerializeField] private AudioSource progressAudioSource;
        [SerializeField] private AudioSource qualityAudioSource;
        [SerializeField] private AudioClip progressSoundEffect;
        [SerializeField] private AudioClip qualitySoundEffect;

        private int[] progressThresholds = { 4, 8, 12, 16, 20 };
        private int[] qualityPositiveThresholds = { 6, 12, 18, 24, 30 };
        private int[] qualityNegativeThresholds = { -6, -12, -18, -24, -30 };

        private void OnEnable()
        {
            GameManager.OnProgressUpdated += ProgressUpdatedHandler;
            GameManager.OnQualityUpdated += QualityUpdatedHandler;
        }

        private void ProgressUpdatedHandler(int value)
        {
            if (System.Array.Exists(progressThresholds, threshold => threshold == value))
            {
                PlaySoundEffect(progressAudioSource, progressSoundEffect);
            }
        }

        private void QualityUpdatedHandler(int value)
        {
            if (System.Array.Exists(qualityPositiveThresholds, threshold => threshold == value) ||
                System.Array.Exists(qualityNegativeThresholds, threshold => threshold == value))
            {
                PlaySoundEffect(qualityAudioSource, qualitySoundEffect);
            }
        }

        private void PlaySoundEffect(AudioSource audioSource, AudioClip soundEffect)
        {
            if (audioSource != null && soundEffect != null)
            {
                audioSource.PlayOneShot(soundEffect);
            }
            else
            {
                Debug.LogWarning("AudioSource or AudioClip is not set.");
            }
        }

        private void OnDisable()
        {
            GameManager.OnProgressUpdated -= ProgressUpdatedHandler;
            GameManager.OnQualityUpdated -= QualityUpdatedHandler;
        }
    }
}



