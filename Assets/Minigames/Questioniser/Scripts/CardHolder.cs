using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Methodyca.Minigames.Questioniser
{
    public class CardHolder : MonoBehaviour
    {
        [SerializeField] Transform holder;

        public Transform GetTransform => holder;
        public List<CardBase> Cards = new List<CardBase>();

        public void ArrangeCardDeck()
        {
            if (Cards.Count > 0)
            {
                Cards.RemoveAll(c => c == null);
                for (int i = 0; i < Cards.Count; i++)
                    Cards[i].transform.DOMoveX(holder.position.x + ((Cards.Count * Cards[i].SpriteSizeX * 0.5f) - (i * Cards[i].SpriteSizeX)) - 1, 0.5f);
            }
        }
    }
}