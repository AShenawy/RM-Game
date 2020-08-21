using UnityEngine;

namespace Methodyca.Minigames.Questioniser
{
    public class PointConverter : ActionCard
    {
        [Header("PointConverter Attributes")]
        [SerializeField] int affectedActionPoint = 5;
        [SerializeField] int affectedInterestPoint = -10;

        protected override void HandleActionBehaviour()
        {
            _gameManager.SwitchInterestToActionPoint(affectedInterestPoint, affectedActionPoint);
            Destroy(gameObject);
        }
    }
}