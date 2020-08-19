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
        public float SpriteSizeX => sprite.bounds.size.x;

        protected Camera _camera;
        protected Transform _transform;
        protected Collider2D _collider;
        protected CardHolder _hand;
        protected CardHolder _table;
        protected CardHolder _deck;

        SpriteRenderer _renderer;

        public static bool IsClickable = false;
        protected virtual void Throw() { }

        public event Action<CardBase> OnCardClicked = delegate { };
        public event Action<int> OnActionValueChanged = delegate { };
        public event Action<int> OnInterestValueChanged = delegate { };
        public event EventHandler<OnCardThrownEventArgs> OnCardThrown;
        public class OnCardThrownEventArgs : EventArgs { public CardBase Card; }
        protected void TriggerCardIsThrown(CardBase card) => OnCardThrown?.Invoke(this, new OnCardThrownEventArgs { Card = card });
        public void Draw() => StartCoroutine(DrawCoroutine());

        public void InitializeCard(Camera camera, CardHolder hand, CardHolder table, CardHolder deck)
        {
            _camera = camera;
            _hand = hand;
            _table = table;
            _deck = deck;
            _collider.enabled = false;
        }

        public void Discard()
        {
            _transform.DOScale(0, 0.25f).OnComplete(() =>
            {
                if (_hand.Cards.Contains(this))
                    _hand.Cards.Remove(this);
                else if (_table.Cards.Contains(this))
                    _table.Cards.Remove(this);

                Destroy(gameObject);
            });
        }

        public void ReturnDeck()
        {
            IsClickable = false;
            Sequence seq = DOTween.Sequence();
            seq.Append(_transform.DOMoveY(_collider.bounds.extents.y, 0.1f))
                .AppendCallback(() => _hand.Cards.Remove(this))
                .Join(_transform.DORotate(new Vector3(0, -180, 0), 0.1f))
                .Append(_transform.DOMove(_deck.GetTransform.position, 0.5f))
                .AppendCallback(() =>
                {
                    _deck.Cards.Add(this);
                    _transform.SetParent(_deck.GetTransform);
                });
        }

        public void SelectCard()
        {
            _transform.DOScale(1.2f, 0.2f);
        }

        public void DeselectCard()
        {
            _transform.DOScale(1f, 0.2f);
        }

        public void SetOutlineColorAs(Color color)
        {
            _renderer.material.SetColor(outlineColor, color);
        }

        protected virtual void OnMouseDown()
        {
            if (!IsClickable)
                return;

            _hand.Cards.Remove(this);
            _transform.DOScale(SELECTION_SCALE, SELECTION_SCALE_PACE_IN_SEC);
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
            _renderer.material.SetColor(outlineColor, Color.green);
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

            _renderer.material.SetColor(outlineColor, Color.red);
        }

        void OnMouseExit()
        {
            if (!IsClickable)
                return;

            _renderer.material.SetColor(outlineColor, Color.clear);
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

        IEnumerator DrawCoroutine()
        {
            Sequence drawSequence = DOTween.Sequence();
            drawSequence.Append(_transform.DOMoveY(_collider.bounds.extents.y, 0.25f))
                .Append(_transform.DORotate(new Vector3(0, 0, 0), 0.5f))
                .Join(_transform.DOMove(_hand.GetTransform.position, 0.5f));

            yield return drawSequence.WaitForCompletion();

            _hand.Cards.Add(this);
            _transform.SetParent(_hand.GetTransform);
            _hand.ArrangeCardDeck();
            _collider.enabled = true;
            IsClickable = true;
        }
    }
}