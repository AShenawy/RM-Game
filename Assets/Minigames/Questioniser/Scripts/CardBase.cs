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

        protected Camera _camera;
        protected CardData _data;
        protected Transform _transform;
        protected Collider2D _collider;
        protected CardHolder _hand;
        protected CardHolder _table;
        protected SpriteRenderer _renderer;

        public virtual void Discard() { }
        protected virtual void Throw() { }
        public CardData GetData => _data;
        public void Draw() => StartCoroutine(DrawCoroutine());

        public event EventHandler<OnCardThrownEventArgs> OnCardThrown;
        public class OnCardThrownEventArgs : EventArgs { public CardBase Card; }
        protected void TriggerCardIsThrown(CardBase card) => OnCardThrown?.Invoke(this, new OnCardThrownEventArgs { Card = card });

        public void InitializeCard(Camera camera, CardData data, CardHolder hand, CardHolder table)
        {
            _camera = camera;
            _data = data;
            _hand = hand;
            _table = table;
            _renderer.sprite = data.Sprite;
            _collider.enabled = false;
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
            var distance = Vector3.Distance(_transform.position, _camera.transform.position);
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            Vector2 rayPoint = ray.GetPoint(distance);
            _transform.position = rayPoint;
        }

        void OnMouseUp()
        {
            _transform.DOScale(Vector2.one, SELECTION_SCALE_PACE_IN_SEC);
            var inputPosition = (Vector2)_camera.ScreenToViewportPoint(Input.mousePosition);

            if (inputPosition.y < 0.5f || inputPosition.y > 0.9f || inputPosition.x < 0.25f || inputPosition.x > 0.75f)
                ReturnHand();
            else if (GameManager.Instance.ActionPoint >= _data.ActionPoint)
                Throw();
            else
                ReturnHand();
        }

        void ReturnHand()
        {
            _hand.Cards.Add(this);
            _transform.DOMove(_hand.GetTransform.position, 0.25f)
                .OnStart(() => _collider.enabled = false)
                .OnComplete(() =>
                {
                    _hand.ArrangeCardDeck();
                    _collider.enabled = true;
                });
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
        }
    }
}