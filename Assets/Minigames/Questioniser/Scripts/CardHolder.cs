using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Methodyca.Minigames.Questioniser
{
    public class CardHolder : MonoBehaviour
    {
        [SerializeField] Transform holder;

        public Transform GetTransform => holder;
        public List<CardBase> Cards { get; set; } = new List<CardBase>();

        public void ArrangeCardDeck()
        {
            if (Cards.Count > 0)
                for (int i = 0; i < Cards.Count; i++)
                    Cards[i].transform.DOMoveX(Cards.Count - 1 - i, 1f);
        }
    }
}