using UnityEngine;

namespace Methodyca.Minigames.Protoescape
{
    public interface IEntity
    {
        GameObject gameObject { get; }
        int CurrentSiblingIndex { get; }
        int CorrectSiblingIndex { get; }
    }
}