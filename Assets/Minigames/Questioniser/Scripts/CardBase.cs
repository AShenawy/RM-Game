using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

namespace Methodyca.Minigames.Questioniser
{
    public class CardBase : MonoBehaviour
    {
        const float SELECTION_SCALE = 1.2F;
        const float SELECTION_SCALE_PACE_IN_SEC = 0.25F;

        [Header("Base Attributes")]
        [SerializeField] new string name;
        [SerializeField] int actionPoint;
        [SerializeField] int interestPoint;
        [SerializeField] int spawnSize;
        [SerializeField] [Multiline] string description;
        [SerializeField] Sprite sprite;
        [SerializeField] Color outlineColor;

        public string Name => name;
        public int ActionPoint { get => actionPoint; protected set { actionPoint = value; OnActionValueChanged?.Invoke(actionPoint); } }
        public int InterestPoint { get => interestPoint; protected set { interestPoint = value; OnInterestValueChanged?.Invoke(interestPoint); } }
        public int SpawnSize => spawnSize;
        public string Description => description;
        public Sprite Sprite => sprite;
        public Color OutlineColor => outlineColor;

        protected Camera _camera;
        protected Transform _transform;
        protected Collider2D _collider;
        protected CardHolder _hand;
        protected CardHolder _table;

        SpriteRenderer _renderer;

        public static bool IsClickable = false;
        public virtual void Discard() { }
        protected virtual void Throw() { }
        public void Draw() => StartCoroutine(DrawCoroutine());

        public event Action<int> OnActionValueChanged = delegate { };
        public event Action<int> OnInterestValueChanged = delegate { };
        public event EventHandler<OnCardThrownEventArgs> OnCardThrown;
        public class OnCardThrownEventArgs : EventArgs { public CardBase Card; }
        protected void TriggerCardIsThrown(CardBase card) => OnCardThrown?.Invoke(this, new OnCardThrownEventArgs { Card = card });

        public void InitializeCard(Camera camera, CardHolder hand, CardHolder table)
        {
            _camera = camera;
            _hand = hand;
            _table = table;
            _collider.enabled = false;
        }

        void Awake()
        {
            _transform = transform;
            _collider = GetComponent<Collider2D>();
            _renderer = GetComponent<SpriteRenderer>();
            _renderer.sprite = sprite;
        }

        protected virtual void OnMouseDown()
        {
            if (!IsClickable)
                return;

            _hand.Cards.Remove(this);
            _transform.DOScale(SELECTION_SCALE, SELECTION_SCALE_PACE_IN_SEC);
        }

        protected virtual void OnMouseUpAsButton() { }

        void OnMouseDrag()
        {
            if (!IsClickable)
                return;

            var distance = Vector3.Distance(_transform.position, _camera.transform.position);
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            Vector2 rayPoint = ray.GetPoint(distance);
            _transform.position = rayPoint;
        }

        protected virtual void OnMouseUp()
        {
            if (!IsClickable)
                return;

            _transform.DOScale(Vector2.one, SELECTION_SCALE_PACE_IN_SEC);
            var inputPosition = (Vector2)_camera.ScreenToViewportPoint(Input.mousePosition);

            if (inputPosition.y < 0.5f || inputPosition.y > 0.9f || inputPosition.x < 0.25f || inputPosition.x > 0.75f)
                ReturnHand();
        }

        protected void ReturnHand()
        {
            _hand.Cards.Add(this);
            Sequence seq = DOTween.Sequence();
            seq.Append(_transform.DOMove(_hand.GetTransform.position, 0.25f)
                .OnStart(() => _collider.enabled = false)
                .OnComplete(() =>
                {
                    _hand.ArrangeCardDeck();
                    _collider.enabled = true;
                })).Join(_transform.DOScale(1, 0.25f));
        }

        IEnumerator DrawCoroutine()
        {
            Sequence drawSequence = DOTween.Sequence();
            drawSequence.Append(_transform.DOMoveY(_collider.bounds.extents.y, 0.25f))
                .Append(_transform.DORotate(new Vector3(0, 0, 0), 0.3f))
                .Append(_transform.DOMove(_hand.GetTransform.position, 0.75f));

            yield return drawSequence.WaitForCompletion();

            if (!_hand.Cards.Contains(this))
                _hand.Cards.Add(this);

            _transform.SetParent(_hand.GetTransform);
            _hand.ArrangeCardDeck();
            _collider.enabled = true;
            IsClickable = true;
        }
    }
}