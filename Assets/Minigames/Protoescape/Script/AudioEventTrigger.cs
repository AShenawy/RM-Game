using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Methodyca.Minigames.Protoescape
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioEventTrigger : MonoBehaviour
    {
        [SerializeField] private bool startPlayOnEnable;
        [SerializeField] private bool destroyAfterPlay;
        [SerializeField] private AudioClip clip;
        [SerializeField] private UnityEvent onPlay;
        [SerializeField] private UnityEvent onStop;

        public UnityEvent OnPlay { get => onPlay; }
        public UnityEvent OnStop { get => onStop; }

        private AudioSource _source;
        private WaitUntil waitUntilAudioStop;

        public void Play()
        {
            StartCoroutine(PlayCor());
        }

        public void Play(AudioClip audioClip)
        {
            clip = audioClip;
            StartCoroutine(PlayCor());
        }

        private void Awake()
        {
            _source = GetComponent<AudioSource>();
            _source.playOnAwake = false;

            waitUntilAudioStop = new WaitUntil(() => !_source.isPlaying);
        }

        private void OnEnable()
        {
            if (startPlayOnEnable)
            {
                Play();
            }
        }

        private IEnumerator PlayCor()
        {
            if (clip == null)
            {
                yield return null;
            }

            _source.clip = clip;
            _source.Play();

            onPlay?.Invoke();
            yield return waitUntilAudioStop;
            onStop?.Invoke();

            if (destroyAfterPlay)
            {
                Destroy(gameObject);
            }
        }
    }
}