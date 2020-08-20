using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace Methodyca.Minigames.Questioniser
{
    /// <summary>
    /// When this card is played, the player starts with one extra AP next turn for each correct Item Card played this turn.
    /// </summary>
    public class HighFlyer : ActionCard
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
            yield return throwSeq.Append(_transform.DOMove(_table.GetTransform.position + _thrownLocation, 0.25f))
                .Join(_transform.DORotate(new Vector3(0, 360, 0), 0.25f)).AppendCallback(() => HandleAction()).WaitForCompletion();
        }

        void HandleAction()
        {
            GameManager.Instance.HandleHighFlayer();
            Destroy(gameObject);
        }
    }
}