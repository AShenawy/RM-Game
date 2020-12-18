using System.Collections.Generic;
using UnityEngine;

namespace Methodyca.Minigames.Protoescape
{
    public interface ICheckable
    {
        string EntityID { get; }
        bool IsChecked { get; set; }
        GameObject gameObject { get; }
        EntityCoordinate CurrentCoordinate { get; }
        HashSet<CategoryType> Categories { get; }
        string GetNotebookLogData();
        Dictionary<CategoryType, object> GetLikables();
        HashSet<CategoryType> GetLikedCategories();
        HashSet<CategoryType> GetConfusedCategories();
    }
}