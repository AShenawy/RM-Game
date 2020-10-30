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

        public IEnumerable<ICheckable> GetRandomCheckablesBy(int count = 2)
        {
            var checkables = new List<ICheckable>(GetAllCheckables());

            while (count > 0)
            {
                ICheckable random = checkables[Random.Range(0, checkables.Count)];
                yield return random;
                checkables.Remove(random);
                count--;
            }
        }
    }
}