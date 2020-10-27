using System.Collections.Generic;
using UnityEngine;

namespace Methodyca.Minigames.Protoescape
{
    public interface ICheckable
    {
        GameObject gameObject { get; }
        string EntityID { get; }
        bool IsChecked { get; set; }
        int GetSiblingIndex { get; }
        Dictionary<CategoryType, GameObject> GetConfusions();
    }
}