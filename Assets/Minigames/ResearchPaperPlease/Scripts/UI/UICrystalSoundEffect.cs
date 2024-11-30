using UnityEngine;

namespace Methodyca.Minigames.ResearchPaperPlease
{
    public class UICrystalSoundEffect : MonoBehaviour
    {
        [SerializeField] private AudioSource toneAudioSource;
        [SerializeField] private AudioClip correctSoundEffect;
        [SerializeField] private AudioClip incorrectSoundEffect;

        public void PlayCorrectSoundEffect()
        {
            toneAudioSource.PlayOneShot(correctSoundEffect);
        }

        public void PlayIncorrectSoundEffect()
        {
            toneAudioSource.PlayOneShot(incorrectSoundEffect);
        }
    }
}



