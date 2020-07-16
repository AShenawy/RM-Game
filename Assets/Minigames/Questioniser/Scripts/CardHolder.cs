using System.Collections.Generic;
using UnityEngine;

namespace Methodyca.Minigames.Questioniser
{
    public class CardHolder : MonoBehaviour
    {
        [SerializeField] Transform _holder;

        public Transform Transform => _holder;
        public List<Card> Cards { get; set; }
    }
}