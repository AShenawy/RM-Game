using UnityEngine;
using UnityEngine.UI;
using Methodyca.Core;

namespace Methodyca.Minigames.SortGame
{
    // this script handles crystal behaviour
    public class Crystal : MonoBehaviour
    {
        public Image crystalImageRenderer;
        public Sprite[] crystalPhases;
        public Image glowImage;
        private VerticalOscillator oscillator;
        public Sound chargedSFX;

        public int currentPhase;

        private void Start()
        {
            glowImage.enabled = false;
            oscillator = GetComponent<VerticalOscillator>();
        }

        public void AdjustGlow(int phase)
        {
            currentPhase = phase;
            //phase = Mathf.Clamp(phase, -5, 5);
            int spriteIndex = phase + 5;
            crystalImageRenderer.sprite = crystalPhases[spriteIndex];
            oscillator.AdjustSpeed(phase);

            if (phase > 4)
            {
                glowImage.enabled = true;
                SoundManager.instance.PlaySFX(chargedSFX);
            }
            else
            {
                glowImage.enabled = false;
                SoundManager.instance.StopSFX(chargedSFX.name);
            }
        }
    }
}