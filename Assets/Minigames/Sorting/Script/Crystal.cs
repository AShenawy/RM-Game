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

        //private int chargePhase = 0;          //***** unused. removed
        private VerticalOscillator oscillator;
        //private AudioSource audioSource;      //**** switched to sound manager for handling sfx
        public Sound chargedSFX;
        //private SortingManager gameManager;


        private void Start()
        {
            glowImage.enabled = false;
            oscillator = GetComponent<VerticalOscillator>();
            //audioSource = GetComponent<AudioSource>();        //***** switched to sound mananger for handling sfx
            //audioSource.mute = true;
            //gameManager = FindObjectOfType<SortingManager>();
            //gameManager.gameComplete += LowerVolume;
        }

        //private void OnDisable()
        //{
        //    gameManager.gameComplete -= LowerVolume;
        //}

        //public void IncreaseCharge()
        //{
        //    chargePhase++;
        //    crystalImageRenderer.sprite = crystalPhases[chargePhase];

        //    if (chargePhase > 4)
        //    {
        //        glowImage.enabled = true;
        //        audioSource.Play();
        //    }
        //}

        //public void DecreaseCharge()
        //{
        //    chargePhase--;
        //    crystalImageRenderer.sprite = crystalPhases[chargePhase];

        //    // no need for if statement since the glow is displayed at max only.
        //    glowImage.enabled = false;
        //    audioSource.Stop();
        //}

        public void AdjustGlow(int phase)
        {
            crystalImageRenderer.sprite = crystalPhases[phase];
            oscillator.AdjustSpeed(phase);

            if (phase > 4)
            {
                glowImage.enabled = true;
                //audioSource.mute = false;     //**** switched to sound manager
                SoundManager.instance.PlaySFX(chargedSFX);
            }
            else
            {
                glowImage.enabled = false;
                //audioSource.mute = true;      //**** switched to sound manager
                SoundManager.instance.StopSFX(chargedSFX.name);
            }
        }

        //private void LowerVolume()
        //{
        //    audioSource.volume = 0.13f;
        //}
    }
}