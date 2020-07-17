using System;
using UnityEngine;

namespace Methodyca.Minigames.Questioniser
{
    public abstract class CardBase : MonoBehaviour
    {
        public abstract event Action<CardBase> OnCardThrown;
        public abstract void InitializeCard(Camera camera, CardData data, CardHolder hand, CardHolder table);
        public abstract void SetHolder(CardHolder holder);
        protected abstract void TriggerActionAfterThrown();
    }
}