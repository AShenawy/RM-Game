using System;
using UnityEngine;

namespace Methodyca.Minigames.Questioniser
{
    public class CardInfo : MonoBehaviour
    {
        [SerializeField] CardBase card;

        public static event Action<bool, string> OnCardInfoCalled = delegate { };

        void OnMouseDown()
        {
            OnCardInfoCalled?.Invoke(true, card.Description);
        }

        void OnMouseUp()
        {
            OnCardInfoCalled?.Invoke(false, "");
        }
    }
}