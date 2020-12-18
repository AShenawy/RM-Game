using UnityEngine;

namespace Methodyca.Minigames.Protoescape
{
    public class EntityStack : MonoBehaviour
    {
        private ScreenBox _screen;
        private RectTransform _rect;

        public int CurrentSiblingIndex { get => _rect.GetSiblingIndex(); }
        public string ScreenName { get => _screen.ScreenName; }

        private void Awake()
        {
            _rect = GetComponent<RectTransform>();
            _screen = GetComponentInParent<ScreenBox>();
        }
    }
}