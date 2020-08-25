using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace Methodyca.Minigames.Questioniser
{
    public class ActionCard : CardBase
    {
        protected const float THROW_TWEEN_DURATION = 0.25f;
        protected readonly Vector3 _throwLocation = new Vector3(-5, 0, 0);

        void Start()
        {
            _gameManager.OnTopicClosed += TopicClosedHandler;
        }

        void TopicClosedHandler(Topic topic)
        {
            CostPoint++;
        }

        protected override void Throw()
        {
            StopCoroutine(ThrowCor());
            StartCoroutine(ThrowCor());
        }

        IEnumerator ThrowCor()
        {
            yield return DOTween.Sequence()
            .Append(_transform.DOMove(_table.GetTransform.position + _throwLocation, THROW_TWEEN_DURATION))
            .Join(_transform.DOScale(1.5f, THROW_TWEEN_DURATION)).WaitForCompletion();

            TriggerCardIsThrown(this);
            HandleActionBehaviour();
        }

        protected virtual void HandleActionBehaviour() { }

        void OnDestroy()
        {
            _gameManager.GameState = GameState.Playable;
            _gameManager.OnTopicClosed -= TopicClosedHandler;
        }
    }
}