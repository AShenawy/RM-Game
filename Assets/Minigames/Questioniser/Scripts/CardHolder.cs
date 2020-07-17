using System.Collections.Generic;
using UnityEngine;

namespace Methodyca.Minigames.Questioniser
{
    public class CardHolder : MonoBehaviour
    {
        [SerializeField] Transform holder;

        public Transform Transform => holder;
        public List<Card> Cards { get; set; }
    }
}