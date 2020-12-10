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
        [SerializeField] private Transform endPoint;

        private Tween _moveTween;
        private Transform _transform;
        private Animator _animator;

        public void PlayPauseMoveTween()
        {
            _moveTween.TogglePause();
            _animator.SetBool("IsMoving", _moveTween.IsPlaying());
        }

        private void Awake()
        {
            _transform = transform;
            _animator = GetComponentInChildren<Animator>();
        }

        private void OnEnable()
        {
            float _duration = Mathf.Abs(duration);
            Vector2 startPosition = _transform.position;

            _moveTween = DOTween.Sequence().SetDelay(initialDelay)
                                           .AppendCallback(() => _transform.LookAt(endPoint, Vector3.up))
                                           .Append(_transform.DOMove(endPoint.position, _duration))
                                           .AppendCallback(() => _transform.LookAt(startPosition, Vector3.up))
                                           .SetDelay(finalDelay)
                                           .Append(_transform.DOMove(startPosition, _duration))
                                           .SetEase(Ease.Linear).SetLoops(-1).SetAutoKill(false).Pause();

            if (isPlayOnEnable)
            {
                PlayPauseMoveTween();
            }
        }

        private void OnDisable()
        {
            _moveTween.Pause();
        }

        private void OnDestroy()
        {
            _moveTween.Kill();
        }
    }
}