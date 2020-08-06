using DG.Tweening;

namespace Methodyca.Minigames.Questioniser
{
    public class ActionCard : CardBase
    {
        public override void Discard()
        {
            _transform.DOScale(0, 0.25f).OnComplete(() => Destroy(gameObject));
        }
    }
}