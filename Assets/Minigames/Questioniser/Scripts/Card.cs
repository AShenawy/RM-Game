using DG.Tweening;
using System;
using TMPro;
using UnityEngine;

namespace Methodyca.Minigames.Questioniser
{
    public class Card : MonoBehaviour
    {
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

        public static event Action<Card> OnCardThrown = delegate { };

        void Awake()
        {
            _transform = transform;
            _collider = GetComponent<Collider2D>();
            _renderer = GetComponent<SpriteRenderer>();
        }

        public void InitializeCard(Camera camera, CardData data, CardHolder hand, CardHolder table)
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

        public void SetHolder(CardHolder holder)
        {
            if (!holder.Cards.Contains(this))
                holder.Cards.Add(this);

            _collider.enabled = false;
            transform.DOMove(holder.transform.position, 0.5f).OnComplete(() => _collider.enabled = true);
        }

        void OnMouseDown()
        {
            _hand.Cards.Remove(this);
        }

        void OnMouseDrag()
        {
            var inputPosition = (Vector2)_camera.ScreenToWorldPoint(Input.mousePosition);
            _transform.position = inputPosition;
        }

        void OnMouseUp()
        {
            var inputPosition = (Vector2)_camera.ScreenToViewportPoint(Input.mousePosition);

            if (inputPosition.y > 0.5f && inputPosition.y < 0.75f && inputPosition.x > 0.3f && inputPosition.x < 0.7f)
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

                cardsOnTable[i].transform.DOMove(horizontalPosition, 1f).OnComplete(() => OnCardThrown?.Invoke(this));
            }
        }
    }
}