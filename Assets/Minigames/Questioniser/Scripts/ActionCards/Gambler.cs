using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Methodyca.Minigames.Questioniser
{
    /// <summary>
    /// Player chooses two cards to discard, and draws two random cards from the deck.
    /// </summary>
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
            _collider.enabled = false;
            Sequence throwSeq = DOTween.Sequence();
            yield return throwSeq.Append(_transform.DOMove(_table.GetTransform.position + new Vector3(-5, 0, -5), 0.25f))
                .Join(_transform.DORotate(new Vector3(0, 360, 0), 0.25f))
                .Join(_transform.DOScale(2f, 0.25f))
                .AppendCallback(() => HandleAction()).WaitForCompletion();
        }

        void HandleAction()
        {
            if (_hand.Cards.Count < CARD_COUNT_TO_GAMBLE)
            {
                GameManager.Instance.RaiseGameMessage("Not enough card to gamble");
                Destroy(gameObject);
            }
            else
            {
                IsClickable = false;
                _selectedCards = new HashSet<CardBase>();

                foreach (var selection in _hand.Cards)
                {
                    selection.OnCardClicked += CardClickedHandler;
                    selection.SetOutlineColorAs(Color.yellow);
                }
            }
        }

        void CardClickedHandler(CardBase card)
        {
            if (_selectedCards.Contains(card))
                card.DeselectCard();

            _selectedCards.Add(card);
            card.SelectCard();

            if (_selectedCards.Count >= CARD_COUNT_TO_GAMBLE)
                StartCoroutine(ActionHandlerCor());
        }

        IEnumerator ActionHandlerCor()
        {
            yield return new WaitForSeconds(0.25f);

            foreach (var c in _selectedCards)
                c.Discard();

            foreach (var c in _hand.Cards)
            {
                c.SetOutlineColorAs(Color.clear);
                c.OnCardClicked -= CardClickedHandler;
            }

            GameManager.Instance.DrawRandomCardFromDeck(CARD_COUNT_TO_GAMBLE);
            IsClickable = true;
            Destroy(gameObject); //Replace with Dissolve Effect
        }
    }
}