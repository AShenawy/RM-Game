using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Methodyca.Minigames.Questioniser
{
    public class Strategiser : ActionCard
    {
        //[Header("Strategiser Attributes")]

        List<ItemCard> _selectableCards;

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
            _hand.ArrangeCardDeck();
            Sequence throwSeq = DOTween.Sequence();
            yield return throwSeq.Append(_transform.DOMove(_table.GetTransform.position + new Vector3(0, 0, 0), 0.25f))
                .Join(_transform.DORotate(new Vector3(0, 360, 0), 0.25f))
                .Join(_transform.DOScale(2f, 0.25f)).AppendCallback(() => Recycle()).WaitForCompletion();
        }

        void Recycle()
        {
            var selectables = _hand.Cards;
            foreach (var selection in selectables)
            {
                IsClickable = false;
                if (selection is ItemCard itemCard)
                {
                    itemCard.OnCardClicked += CardClickedHandler;
                    _selectableCards.Add(itemCard);
                }
            }
        }

        void CardClickedHandler(ItemCard card)
        {
            card.Discard();
            GameManager.Instance.ActionPoint += card.ActionPoint;
            IsClickable = true;
            Destroy(gameObject);
        }

        void OnDisable()
        {
            foreach (var card in _selectableCards)
                card.OnCardClicked -= CardClickedHandler;
        }
    }
}