namespace Methodyca.Minigames.Questioniser
{
    public class ChangeTopicCard : MetaCard
    {
        protected override void Throw()
        {
            GameManager.Instance.SetRandomTopic();
        }
    }
}