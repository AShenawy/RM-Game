using UnityEngine;

namespace Methodyca.Minigames.Utils
{
    public class AudioController : MonoBehaviour
    {
        [SerializeField] private AudioSource background; // For background music
        [SerializeField] private AudioSource soundEffectsSource; // For button sound effects
        [SerializeField] private AudioClip backgroundAudio;

        public static AudioController Instance;

        public void PlayOnBackground(AudioClip clip, bool isLooping)
        {
            background.clip = clip;
            background.loop = isLooping;
            background.Play();
        }

        // Method to play sound effects (used by buttons)
        public void PlaySoundEffect(AudioClip clip)
        {
            if (soundEffectsSource != null && clip != null)
            {
                soundEffectsSource.PlayOneShot(clip);
            }
            else
            {
                Debug.LogWarning("Sound effect or AudioSource missing!");
            }
        }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject); // Ensure only one instance exists
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject); // Persist across scenes
            }
        }

        private void Start()
        {
            PlayOnBackground(backgroundAudio, true); // Start background music
        }
    }
}
