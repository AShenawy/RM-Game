using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Methodyca.Minigames.Questioniser
{
    public class Gambler : ActionCard
    {
        const byte CARD_COUNT_TO_GAMBLE = 2;

        HashSet<CardBase> _selectedCards;

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
                .Join(_transform.DOScale(2f, 0.25f))
                .AppendCallback(() => SetSelectables()).WaitForCompletion();
        }

        void SetSelectables()
        {
            IsClickable = false;
            _selectedCards = new HashSet<CardBase>();
            foreach (var selection in _hand.Cards)
            {
                selection.OnCardClicked += CardClickedHandler;
                selection.SetAsSelectable();
            }
        }

        void CardClickedHandler(CardBase card)
        {
            if (_selectedCards.Contains(card))
                card.DeselectCard();
            
            if (_selectedCards.Count < CARD_COUNT_TO_GAMBLE)
            {
                _selectedCards.Add(card);
                card.SelectCard();
            }
            else
            {
                foreach (var c in _selectedCards)
                {
                    c.DeselectCard();
                    c.Discard();
                }
                GameManager.Instance.DrawRandomCardFromDeck(CARD_COUNT_TO_GAMBLE);
                Destroy(gameObject); //Replace with Dissolve Effect
            }
        }

        void OnDisable()
        {
            foreach (var card in _hand.Cards)
                card.OnCardClicked -= CardClickedHandler;
        }
    }
}