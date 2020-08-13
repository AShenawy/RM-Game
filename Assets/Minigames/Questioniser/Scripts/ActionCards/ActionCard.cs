using DG.Tweening;

namespace Methodyca.Minigames.Questioniser
{
    public class ActionCard : CardBase
    {
        void Start()
        {
            GameManager.Instance.OnTopicClosed += TopicClosedHandler;
        }

        void TopicClosedHandler()
        {
            InterestPoint++;
        }

        void OnDestroy()
        {
            if(GameManager.InstanceExists)
                GameManager.Instance.OnTopicClosed -= TopicClosedHandler;
        }
    }
}