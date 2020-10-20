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


        private void Start()
        {
            glowImage.enabled = false;
            oscillator = GetComponent<VerticalOscillator>();
        }

        public void AdjustGlow(int phase)
        {
            crystalImageRenderer.sprite = crystalPhases[phase];
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