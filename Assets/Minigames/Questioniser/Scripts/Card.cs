using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
namespace Methodyca.Minigames.Questioniser
{
    public class Card : CardBase
    {
        const float SELECTION_SCALE = 1.2F;
        const float SELECTION_SCALE_PACE_IN_SEC = 0.25F;

        [SerializeField] TextMeshPro actionPoint;
        [SerializeField] TextMeshPro interestPoint;
        [SerializeField] TextMeshPro description;

        Camera _camera;
        CardData _data;
        Transform _transform;
        Collider2D _collider;
        CardHolder _hand;
        CardHolder _table;
        SpriteRenderer _renderer;

        public int ActionPoint => _data.ActionPoint;
        public int InterestPoint => _data.InterestPoint;

        public override event Action<CardBase> OnCardThrown = delegate { };

        public override void InitializeCard(Camera camera, CardData data, CardHolder hand, CardHolder table)
        {
            _camera = camera;
            _data = data;
            _hand = hand;
            _table = table;
            _renderer.sprite = data.Sprite;

            actionPoint.text = data.ActionPoint.ToString();
            interestPoint.text = data.InterestPoint.ToString();
            description.text = data.Description;
        }

        public override void SetHolder(CardHolder holder)
        {
            if (!holder.Cards.Contains(this))
                holder.Cards.Add(this);

            _transform.DOMove(holder.Transform.position, 0.5f).OnStart(() => _collider.enabled = false).OnComplete(() => _collider.enabled = true);
        }

        protected override void TriggerActionAfterThrown()
        {
            QuizManager.Instance.SetQuizQuestion();
            OnCardThrown?.Invoke(this);
        }

        void Awake()
        {
            _transform = transform;
            _collider = GetComponent<Collider2D>();
            _renderer = GetComponent<SpriteRenderer>();
        }

        void OnMouseDown()
        {
            _hand.Cards.Remove(this);
            _transform.DOScale(SELECTION_SCALE, SELECTION_SCALE_PACE_IN_SEC);
        }

        void OnMouseDrag()
        {
            var inputPosition = (Vector2)_camera.ScreenToWorldPoint(Input.mousePosition);
            _transform.position = inputPosition;
        }

        void OnMouseUp()
        {
            _transform.DOScale(Vector2.one, SELECTION_SCALE_PACE_IN_SEC);
            var inputPosition = (Vector2)_camera.ScreenToViewportPoint(Input.mousePosition);

            if (inputPosition.y > 0.5f && inputPosition.y < 0.75f && inputPosition.x > 0.4f && inputPosition.x < 0.7f)
                ThrowCard();
            else
                ReturnHand();
        }

        void ReturnHand()
        {
            if (!_hand.Cards.Contains(this))
                _hand.Cards.Add(this);

            _collider.enabled = false;
            transform.DOMove(_hand.Transform.position, 0.5f).OnComplete(() => _collider.enabled = true);
        }

        void ThrowCard()
        {
            _collider.enabled = false;
            _hand.Cards.Remove(this);

            var cardsOnTable = _table.Cards;

            if (!cardsOnTable.Contains(this))
                cardsOnTable.Add(this);

            for (int i = 0; i < cardsOnTable.Count; i++)
            {
                var offset = new Vector2(_collider.bounds.extents.x, _table.Transform.position.y);
                var horizontalPosition = offset;

                cardsOnTable[i].transform.DOMove(horizontalPosition, 1f).OnComplete(() => TriggerActionAfterThrown());
            }
        }
    }
}