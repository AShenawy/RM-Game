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
        readonly int outlineColor = Shader.PropertyToID("_OutlineColor");

        [Header("Base Attributes")]
        [SerializeField] new string name;
        [SerializeField] int actionPoint;
        [SerializeField] int interestPoint;
        [SerializeField] int spawnSize;
        [SerializeField] [Multiline] string description;
        [SerializeField] Sprite sprite;

        public string Name => name;
        public int ActionPoint { get => actionPoint; protected set { actionPoint = value; OnActionValueChanged?.Invoke(actionPoint); } }
        public int InterestPoint { get => interestPoint; protected set { interestPoint = value; OnInterestValueChanged?.Invoke(interestPoint); } }
        public int SpawnSize => spawnSize;
        public string Description => description;

        protected Camera _camera;
        protected Transform _transform;
        protected Collider2D _collider;
        protected CardHolder _hand;
        protected CardHolder _table;
        protected CardHolder _deck;

        SpriteRenderer _renderer;

        public static bool IsClickable = false;
        public virtual void Discard() { }
        protected virtual void Throw() { }
        public void Draw() => StartCoroutine(DrawCoroutine());

        public event Action<CardBase> OnCardClicked = delegate { };
        public event Action<int> OnActionValueChanged = delegate { };
        public event Action<int> OnInterestValueChanged = delegate { };
        public event EventHandler<OnCardThrownEventArgs> OnCardThrown;
        public class OnCardThrownEventArgs : EventArgs { public CardBase Card; }
        protected void TriggerCardIsThrown(CardBase card) => OnCardThrown?.Invoke(this, new OnCardThrownEventArgs { Card = card });

        public void InitializeCard(Camera camera, CardHolder hand, CardHolder table, CardHolder deck)
        {
            _camera = camera;
            _hand = hand;
            _table = table;
            _deck = deck;
            _collider.enabled = false;
        }

        public void ReturnDeck()
        {
            IsClickable = false;
            Sequence seq = DOTween.Sequence();
            seq.Append(_transform.DOMoveY(_collider.bounds.extents.y, 0.1f))
                .AppendCallback(() => _hand.Cards.Remove(this))
                .Join(_transform.DORotate(new Vector3(0, -180, 0), 0.1f))
                .Append(_transform.DOMove(_deck.GetTransform.position, 0.5f))
                .AppendCallback(() => _deck.Cards.Add(this));
        }

        public void SetAsSelectable()
        {
            _renderer.material.SetColor(outlineColor, new Color(0, 0, 1, 1));
        }

        public void SelectCard()
        {
            _transform.DOScale(1.2f, 0.2f);
        }

        public void DeselectCard()
        {
            _transform.DOScale(1f, 0.2f);
        }

        void Awake()
        {
            _transform = transform;
            _collider = GetComponent<Collider2D>();
            _renderer = GetComponent<SpriteRenderer>();
            _renderer.sprite = sprite;
        }

        void OnMouseEnter()
        {
            if (!IsClickable)
                return;

            _renderer.material.SetColor(outlineColor, new Color(1, 0, 0, 1));
        }

        void OnMouseExit()
        {
            if (!IsClickable)
                return;

            _renderer.material.SetColor(outlineColor, new Color(0, 0, 0, 0));
        }

        protected virtual void OnMouseDown()
        {
            if (!IsClickable)
                return;

            _hand.Cards.Remove(this);
            _transform.DOScale(SELECTION_SCALE, SELECTION_SCALE_PACE_IN_SEC);
        }

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

            if (inputPosition.y < 0.4f || inputPosition.y > 0.9f || inputPosition.x < 0.25f || inputPosition.x > 0.75f)
                ReturnHand();
        }

        protected virtual void OnMouseUpAsButton()
        {
            _renderer.material.SetColor(outlineColor, new Color(0, 1, 0, 1));
            OnCardClicked?.Invoke(this);
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
                .Append(_transform.DORotate(new Vector3(0, 0, 0), 0.5f))
                .Join(_transform.DOMove(_hand.GetTransform.position, 0.5f));

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