using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Methodyca.Minigames.Protoescape
{
    public class PrototypeTester : MonoBehaviour
    {
        public static event Action<ConfusionType, GameObject> OnSelectionPointed = delegate { };

        private List<ICheckable> _selections;

        private void Start()
        {
            _selections = new List<ICheckable>(GameManager_Protoescape.Instance.GetAllSelectedCheckablesToTest());
        }

        public void PointSelectedCheckable()
        {
            var checkable = _selections[Random.Range(0, _selections.Count)];
            var confusions = checkable.GetConfusions();

            if (confusions.Count > 0)
            {
                var index = Random.Range(0, confusions.Count);
                var key = confusions.Keys.ElementAt(index);
                var value = confusions.Values.ElementAt(index);

                OnSelectionPointed?.Invoke(key, value);
            }
            else
            {
                OnSelectionPointed?.Invoke(ConfusionType.None, checkable.gameObject);
            }

            _selections.Remove(checkable);
        }
    }
}