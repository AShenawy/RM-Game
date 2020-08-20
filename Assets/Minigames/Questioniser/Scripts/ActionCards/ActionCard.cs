using UnityEngine;

namespace Methodyca.Minigames.Questioniser
{
    public class ActionCard : CardBase
    {
        protected readonly Vector3 _thrownLocation = new Vector3(-5, 0, 0);

        void Start()
        {
            GameManager.Instance.OnTopicClosed += TopicClosedHandler;
        }

        void TopicClosedHandler(Topic topic)
        {
            InterestPoint++;
        }

        void OnDestroy()
        {
            if (GameManager.InstanceExists)
                GameManager.Instance.OnTopicClosed -= TopicClosedHandler;
        }
    }
}