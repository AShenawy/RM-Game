using DG.Tweening;
using UnityEngine;

namespace Methodyca.Minigames.Protoescape
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] private bool isPlayOnStart;
        [SerializeField] private float duration;
        [SerializeField] private Transform endPoint;

        private Tween _moveTween;
        private Transform _transform;

        public void PlayPauseMoveTween()
        {
            _moveTween.TogglePause();
        }

        private void Awake()
        {
            _transform = transform;
        }

        private void Start()
        {
            float _duration = Mathf.Abs(duration);
            Vector2 startPosition = _transform.position;

            _moveTween = DOTween.Sequence().AppendCallback(() => _transform.LookAt(endPoint, Vector3.up))
                                           .Append(_transform.DOMove(endPoint.position, _duration))
                                           .AppendCallback(() => _transform.LookAt(startPosition, Vector3.up))
                                           .Append(_transform.DOMove(startPosition, _duration))
                                           .SetEase(Ease.Linear).SetLoops(-1).SetAutoKill(false).Pause();

            if (isPlayOnStart)
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