using UnityEngine;
using UnityEngine.UI;

namespace Methodyca.Minigames.SortGame
{
    // this script handles crystal behaviour
    public class Crystal : MonoBehaviour
    {
        public Image crystalImageRenderer;
        public Sprite[] crystalPhases;
        public Image glowImage;

        private int chargePhase = 0;
        private VerticalOscillator oscillator;
        private AudioSource audioSource;

        private void Start()
        {
            glowImage.enabled = false;
            oscillator = GetComponent<VerticalOscillator>();
            audioSource = GetComponent<AudioSource>();
        }

        public void IncreaseCharge()
        {
            chargePhase++;
            crystalImageRenderer.sprite = crystalPhases[chargePhase];

            if (chargePhase > 4)
            {
                glowImage.enabled = true;
                audioSource.Play();
            }
        }

        public void DecreaseCharge()
        {
            chargePhase--;
            crystalImageRenderer.sprite = crystalPhases[chargePhase];

            // no need for if statement since the glow is displayed at max only.
            glowImage.enabled = false;
            audioSource.Stop();
        }

        public void AdjustGlow(int phase)
        {
            crystalImageRenderer.sprite = crystalPhases[phase];
            oscillator.AdjustSpeed(phase);

            if (phase > 4)
            {
                glowImage.enabled = true;
                audioSource.Play();
            }
            else
            {
                glowImage.enabled = false;
                audioSource.Stop();
            }
        }
    }
}