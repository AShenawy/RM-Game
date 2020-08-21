using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace Methodyca.Minigames.Questioniser
{
    /// <summary>
    /// When this card is played, for the rest of the turn, you can spend IP as if they were AP.
    /// </summary>
    public class Improviser : ActionCard
    {
        protected override void HandleActionBehaviour()
        {
            _gameManager.HandleImproviser();
            Destroy(gameObject);
        }
    }
}