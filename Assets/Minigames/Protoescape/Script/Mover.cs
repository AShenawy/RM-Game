using UnityEngine;
using DG.Tweening;

namespace Methodyca.Minigames.Protoescape
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] private bool isPlayOnEnable;
        [SerializeField] private float initialDelay;
        [SerializeField] private float finalDelay;
        [SerializeField] private float duration;
        [SerializeField] private RectTransform endPoint;
        [SerializeField] private AudioClip footstep;

        private Tween _moveTween;
        private RectTransform _transform;
        private Animator _animator;
        private AudioSource _source;

        public void Play()
        {
            _moveTween.Play();
            _animator.SetBool("IsMoving", true);
            PlayFootStep(true);
        }

        public void Pause()
        {
            _moveTween.Pause();
            _animator.SetBool("IsMoving", false);
            PlayFootStep(false);
        }

        private void Awake()
        {
            _transform = GetComponent<RectTransform>();
            _animator = GetComponentInChildren<Animator>();
            _source = GetComponentInChildren<AudioSource>();

            if (_source != null)
            {
                _source.clip = footstep;
            }
        }

        private void OnEnable()
        {
            _moveTween.Play();
        }

        private void Start()
        {
            PrototypeTester.OnPrototypeTestInitiated += PrototypeTestInitiatedHandler;
            PrototypeTester.OnPrototypeTestCompleted += PrototypeTestCompletedHandler;

            float _duration = Mathf.Abs(duration);
            Vector2 startPosition = _transform.anchoredPosition;

            _moveTween = DOTween.Sequence().SetDelay(initialDelay)
                                           .AppendCallback(() => _transform.LookAt(endPoint, Vector3.up))
                                           .AppendCallback(() => PlayFootStep(true))
                                           .Append(_transform.DOAnchorPos(endPoint.anchoredPosition, _duration))
                                           .AppendCallback(() => PlayFootStep(false))
                                           .AppendCallback(() => _transform.Rotate(Vector3.up, -180))
                                           .SetDelay(finalDelay)
                                           .AppendCallback(() => PlayFootStep(true))
                                           .Append(_transform.DOAnchorPos(startPosition, _duration))
                                           .AppendCallback(() => PlayFootStep(false))
                                           .SetEase(Ease.Linear).SetLoops(-1).SetAutoKill(false).Pause();

            if (isPlayOnEnable)
            {
                Play();
            }
        }

        private void PrototypeTestCompletedHandler(bool isCompleted,string feedback)
        {
            Pause();

            if (isCompleted)
            {
                Destroy(gameObject);
            }
        }

        private void PrototypeTestInitiatedHandler(string[] notes)
        {
            Pause();
        }

        private void PlayFootStep(bool isPlaying)
        {
            if (footstep != null)
            {
                if (isPlaying)
                {
                    _source.Play();
                }
                else
                {
                    _source.Stop();
                }
            }
        }

        private void OnDisable()
        {
            _moveTween.Pause();
        }

        private void OnDestroy()
        {
            PrototypeTester.OnPrototypeTestInitiated -= PrototypeTestInitiatedHandler;
            PrototypeTester.OnPrototypeTestCompleted -= PrototypeTestCompletedHandler;
        }
    }
}