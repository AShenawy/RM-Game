using System.Collections.Generic;
using UnityEngine;

namespace Methodyca.Minigames.Protoescape
{
    public class ScreenBox : MonoBehaviour
    {
        [SerializeField] private string screenName;

        public string ScreenName => screenName;

        public IEnumerable<ICheckable> GetAllCheckables()
        {
            foreach (var item in GetComponentsInChildren<ICheckable>())
            {
                yield return item;
            }
        }
    }
}