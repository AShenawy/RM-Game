namespace Methodyca.Minigames.Questioniser
{
    public class Compromiser : ActionCard
    {
        public static event System.Action OnEnabled = delegate { };

        protected override void HandleActionBehaviour()
        {
            OnEnabled?.Invoke();
            Destroy(gameObject);
        }
    }
}