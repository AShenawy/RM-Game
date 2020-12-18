using UnityEngine;

namespace Methodyca.Minigames.Utils
{
    public class AudioController : MonoBehaviour
    {
        [SerializeField] private AudioSource background;
        [SerializeField] private AudioClip backgroundAudio;

        public static AudioController Instance;

        public void PlayOnBackground(AudioClip clip, bool isLooping)
        {
            background.clip = clip;
            background.loop = isLooping;
            background.Play();
        }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Debug.LogWarningFormat("Trying to create a second instance of {0}", typeof(AudioController));
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        private void Start()
        {
            PlayOnBackground(backgroundAudio, true);
        }
    }
}