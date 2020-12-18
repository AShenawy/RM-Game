namespace Methodyca.Minigames.Questioniser
{
    public class TopicChanger : ActionCard
    {
        protected override void HandleActionBehaviour()
        {
            _gameManager.SetRandomTopic();
            _gameManager.GameState = GameState.Playable;
            Destroy(gameObject);
        }
    }
}