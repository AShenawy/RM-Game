using UnityEngine;

namespace Methodyca.Minigames.Questioniser
{
    public abstract class CardBase : MonoBehaviour
    {
        public virtual int ActionPoint { get; }
        public virtual int InterestPoint { get; }
        public virtual Question Question { get; }
        public abstract void Draw();
        public abstract void InitializeCard(Camera camera, CardData data, CardHolder hand, CardHolder table);
        protected abstract void TriggerActionAfterThrown();
    }
}