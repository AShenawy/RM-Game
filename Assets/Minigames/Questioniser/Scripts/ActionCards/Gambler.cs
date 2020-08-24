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

        protected override void HandleActionBehaviour()
        {
            if (_hand.Cards.Count < CARD_COUNT_TO_GAMBLE)
            {
                _gameManager.SendGameMessage("Not enough card to gamble");
                Destroy(gameObject);
            }
            else
            {
                _selectedCards = new HashSet<CardBase>();
                _gameManager.GameState = GameState.Selectable;

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
            _gameManager.GameState = GameState.Busy;
            yield return new WaitForSeconds(0.25f);

            foreach (var c in _selectedCards)
                c.Discard();

            _hand.ArrangeCards();
            foreach (var c in _hand.Cards)
            {
                c.SetOutlineColorAs(Color.clear);
                c.OnCardClicked -= CardClickedHandler;
            }

            _gameManager.DrawRandomCardFromDeck(CARD_COUNT_TO_GAMBLE);
            Destroy(gameObject);
        }
    }
}