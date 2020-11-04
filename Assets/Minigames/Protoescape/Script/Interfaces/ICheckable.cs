using System.Collections.Generic;
using UnityEngine;

namespace Methodyca.Minigames.Protoescape
{
    public interface ICheckable
    {
        string EntityID { get; }
        string ScreenName { get; }
        int CurrentSiblingIndex { get; }
        bool IsChecked { get; set; }
        GameObject gameObject { get; }
        HashSet<CategoryType> Categories { get; }
        string GetNotebookLogData();
        Dictionary<CategoryType, object> GetLikables();
    }
}