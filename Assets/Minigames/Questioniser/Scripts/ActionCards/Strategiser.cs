using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Methodyca.Minigames.Questioniser
{
    /// <summary>
    /// Player chooses one Item Card to discard. They gain Action Points (AP) equal to the cost of discarded card.
    /// </summary>
    public class Strategiser : ActionCard
    {
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
            _collider.enabled = false;
            Sequence throwSeq = DOTween.Sequence();
            yield return throwSeq.Append(_transform.DOMove(_table.GetTransform.position + new Vector3(-5, 0, -5), 0.25f))
                .Join(_transform.DORotate(new Vector3(0, 360, 0), 0.25f))
                .Join(_transform.DOScale(2f, 0.25f))
                .AppendCallback(() => Recycle()).WaitForCompletion();
        }

        void Recycle()
        {
            byte cardCount = 0;
            IsClickable = false;
            _selectableCards = new List<ItemCard>();

            foreach (ItemCard itemCard in _hand.Cards)
            {
                cardCount++;
                itemCard.OnCardClicked += CardClickedHandler;
                _selectableCards.Add(itemCard);
                itemCard.SetOutlineColorAs(Color.yellow);
            }

            if (cardCount <= 0)
            {
                GameManager.Instance.RaiseGameMessage("There is not any Item Card in hand");
                Destroy(gameObject);
            }
        }

        void CardClickedHandler(CardBase card)
        {
            GameManager.Instance.ActionPoint += card.ActionPoint;
            card.Discard();
            Destroy(gameObject);
            IsClickable = true;
            foreach (var c in _selectableCards)
            {
                c.SetOutlineColorAs(Color.clear);
                c.OnCardClicked -= CardClickedHandler;
            }
        }
    }
}