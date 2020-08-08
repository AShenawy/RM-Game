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

        public override void Discard()
        {
            _transform.DOScale(0, 0.25f).OnComplete(() => Destroy(gameObject));
        }

        void OnDisable()
        {
            if(GameManager.InstanceExists)
                GameManager.Instance.OnTopicClosed -= TopicClosedHandler;
        }
    }
}