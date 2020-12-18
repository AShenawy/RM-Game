using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace Methodyca.Minigames.Questioniser
{
    /// <summary>
    /// When this card is played, the player starts with one extra AP next turn for each correct Item Card played this turn.
    /// </summary>
    public class HighFlyer : ActionCard
    {
        protected override void HandleActionBehaviour()
        {
            _gameManager.HandleHighFlayer();
            Destroy(gameObject);
        }
    }
}