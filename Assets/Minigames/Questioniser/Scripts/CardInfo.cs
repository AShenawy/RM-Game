using System;
using UnityEngine;

namespace Methodyca.Minigames.Questioniser
{
    public class CardInfo : MonoBehaviour
    {
        [SerializeField] CardBase card;

        public static event Action<bool, CardBase> OnCardInfoCalled = delegate { };

        void OnMouseDown()
        {
            OnCardInfoCalled?.Invoke(true, card);
            
        }

        void OnMouseUp()
        {
            OnCardInfoCalled?.Invoke(false, null);
        }
    }
}