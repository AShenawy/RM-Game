﻿using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace Methodyca.Minigames.Questioniser
{
    public class TopicChanger : ActionCard
    {
        protected override void OnMouseUp()
        {
            if (!IsClickable)
                return;
            base.OnMouseUp();

            if (GameManager.Instance.InterestPoint >= InterestPoint)
            {
                Throw();
            }
            else
            {
                ReturnHand();
                GameManager.Instance.RaiseGameMessage("Not enough interest points");
            }
        }

        protected override void Throw()
        {
            StartCoroutine(ThrowCoroutine());
        }

        IEnumerator ThrowCoroutine()
        {
            TriggerCardIsThrown((ActionCard)this);
            Sequence throwSeq = DOTween.Sequence();
            yield return throwSeq.Append(_transform.DOMove(_table.GetTransform.position + new Vector3(-5, 0, -5), 0.25f))
                .Append(_transform.DORotate(new Vector3(0, 360, 0), 0.25f))
                .Join(_transform.DOScale(1.5f, 0.25f))
                .AppendCallback(() => GameManager.Instance.SetRandomTopic()).WaitForCompletion();
            Destroy(gameObject);
        }
    }
}