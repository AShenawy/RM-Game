using DG.Tweening;
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

        protected override void HandleActionBehaviour()
        {
            byte cardCount = 0;
            _selectableCards = new List<ItemCard>();
            _gameManager.GameState = GameState.Selectable;

            foreach (ItemCard itemCard in _hand.Cards)
            {
                cardCount++;
                _selectableCards.Add(itemCard);
                itemCard.OnCardClicked += CardClickedHandler;
                itemCard.SetOutlineColorAs(Color.yellow);
            }

            if (cardCount <= 0)
            {
                _gameManager.SendGameMessage("There is not any Item Card in hand");
                Destroy(gameObject);
            }
        }

        void CardClickedHandler(CardBase card)
        {
            _gameManager.ActionPoint += card.CostPoint;
            DOTween.Sequence().Append(card.DiscardTweener).AppendCallback(() =>
            {
                _hand.ArrangeCards();
                Destroy(gameObject);
            });

            foreach (var c in _selectableCards)
            {
                c.SetOutlineColorAs(Color.clear);
                c.OnCardClicked -= CardClickedHandler;
            }
        }
    }
}