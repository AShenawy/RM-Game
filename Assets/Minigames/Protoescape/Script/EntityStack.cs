using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Methodyca.Minigames.Protoescape
{
    public class EntityStack : MonoBehaviour, IDragHandler, IDropHandler, ICheckable
    {
        [SerializeField] private string entityId;
        [SerializeField] private Image stackHighlight;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private List<int> likablePositions = new List<int>();

        private ScreenBox _screen;
        private RectTransform _rect;
        private RectTransform _rectParent;
        private Image[] _childrenImages;

        public string EntityID { get => entityId; }
        public bool IsChecked { get; set; } = false;
        public int CurrentSiblingIndex { get => _rect.GetSiblingIndex(); }
        public string ScreenName { get => _screen.ScreenName; }

        public HashSet<CategoryType> Categories
        {
            get => new HashSet<CategoryType>()
            {
                { CategoryType.Position }
            };
        }

        private void Awake()
        {
            _rect = GetComponent<RectTransform>();
            _rectParent = _rect.parent as RectTransform;
            _childrenImages = GetComponentsInChildren<Image>();
            _screen = GetComponentInParent<ScreenBox>();
        }

        private void Start()
        {
            GameManager_Protoescape.OnStackMove += StackMoveHandler;
            stackHighlight.enabled = false;
        }

        private void StackMoveHandler(bool value)
        {
            foreach (var item in _childrenImages)
            {
                item.raycastTarget = !value;
            }

            if (value)
            {
                canvasGroup.alpha = 0.5f;
            }
            else
            {
                canvasGroup.alpha = 1;
            }

            stackHighlight.enabled = stackHighlight.raycastTarget = value;
        }

        public void OnDrag(PointerEventData eventData) { }

        public void OnDrop(PointerEventData eventData)
        {
            var dragged = eventData.pointerDrag;

            if (dragged == null || dragged == gameObject || !GameManager_Protoescape.IsStacksMovable)
            {
                return;
            }

            if (dragged.transform.IsChildOf(_rectParent))
            {
                int index = _rect.GetSiblingIndex();

                _rect.SetSiblingIndex(dragged.transform.GetSiblingIndex());
                dragged.transform.SetSiblingIndex(index);
            }
        }

        public Dictionary<CategoryType, dynamic> GetLikables()
        {
            var dict = new Dictionary<CategoryType, dynamic>();

            if (likablePositions.Contains(CurrentSiblingIndex))
            {
                dict.Add(CategoryType.Position, CurrentSiblingIndex);
            }

            return dict;
        }

        public string GetNotebookLogData()
        {
            var likables = GetLikables();
            string result = "";

            foreach (var category in Categories)
            {
                if (likables.ContainsKey(category))
                {
                    result += $"<b>{category}</b> of {EntityID} at {_screen.ScreenName} screen is <i>liked</i>\n";
                }
                else
                {
                    result += $"<b>{category}</b> of {EntityID} at {_screen.ScreenName} screen is <i>confused</i>\n";
                }
            }

            return result;
        }

        protected virtual void OnDestroy()
        {
            GameManager_Protoescape.OnStackMove -= StackMoveHandler;
        }
    }
}