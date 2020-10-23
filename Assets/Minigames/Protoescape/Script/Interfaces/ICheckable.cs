using System.Collections.Generic;
using UnityEngine;

namespace Methodyca.Minigames.Protoescape
{
    public interface ICheckable
    {
        GameObject gameObject { get; }
        Dictionary<ConfusionType, GameObject> GetConfusions();
    }
}