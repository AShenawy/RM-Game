using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace Methodyca.Minigames.Questioniser
{
    public class ItemCard : CardBase
    {
        [Header("ItemCard Attributes")]
        [SerializeField] Question[] questions;

        public Question[] Questions => questions;

        protected override void Throw() => StartCoroutine(ThrowCoroutine());
        protected override void OnMouseUp()
        {
            if (!IsClickable)
                return;

            base.OnMouseUp();

            if (GameManager.Instance.ActionPoint >= ActionPoint)
            {
                Throw();
            }
            else
            {
                ReturnHand();
                GameManager.Instance.RaiseGameMessage("Not enough action points");
            }
        }

        public override void Discard()
        {
            _transform.DOScale(0, 0.25f).OnComplete(() => Destroy(gameObject));
        }

        IEnumerator ThrowCoroutine()
        {
            if (!_table.Cards.Contains(this))
                _table.Cards.Add(this);

            _transform.SetParent(_table.GetTransform);
            _table.ArrangeCardDeck();
            _collider.enabled = false;
            _hand.ArrangeCardDeck();

            Sequence throwSequence = DOTween.Sequence();
            yield return throwSequence.Append(_transform.DOMove(_table.GetTransform.position, 0.5f))
                .Join(_transform.DORotate(new Vector3(45, 0, 0), 0.5f)).WaitForCompletion();

            TriggerCardIsThrown((ItemCard)this);
        }
    }
}