using System.Collections.Generic;
using UnityEngine;

namespace Methodyca.Minigames.Protoescape
{
    public interface ICheckable
    {
        string EntityID { get; }
        GameObject gameObject { get; }
        EntityCoordinate CurrentCoordinate { get; }
        HashSet<object> GetCurrentData { get; }
        HashSet<CategoryType> Categories { get; }
        string GetNotebookLogData();
        void SetLikables();
        Dictionary<CategoryType, object> GetLikables();
        HashSet<CategoryType> GetLikedCategories();
        HashSet<CategoryType> GetConfusedCategories();
    }
}