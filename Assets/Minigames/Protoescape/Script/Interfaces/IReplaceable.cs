using UnityEngine;

namespace Methodyca.Minigames.Protoescape
{
    public interface IReplaceable<in A>
    {
        GameObject gameObject { get; }
        void Replace(A value);
    }
}