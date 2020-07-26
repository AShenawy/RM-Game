using System.Collections;
using UnityEngine;
using DG.Tweening;

namespace Methodyca.Minigames.Questioniser
{
    public class Card : CardBase
    {
        const float SELECTION_SCALE = 1.2F;
        const float SELECTION_SCALE_PACE_IN_SEC = 0.25F;

        Camera _camera;
        CardData _data;
        Transform _transform;
        Collider2D _collider;
        CardHolder _hand;
        CardHolder _table;
        SpriteRenderer _renderer;
        Question _question;

        public override int ActionPoint => _data.ActionPoint;
        public override int InterestPoint => _data.InterestPoint;
        public override Question Question => _question;
        public override void Draw() => StartCoroutine(DrawCoroutine());

        public override void InitializeCard(Camera camera, CardData data, CardHolder hand, CardHolder table)
        {
            _camera = camera;
            _data = data;
            _hand = hand;
            _table = table;
            _question = data.Question;
            _renderer.sprite = data.Sprite;
            _collider.enabled = false;
        }

        protected override void TriggerActionAfterThrown()
        {
            GameManager.Instance.UpdateTurn(this);
            _transform.SetParent(_table.GetTransform);
            _table.ArrangeCardDeck();
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

            if (inputPosition.y > 0.5f && inputPosition.y < 0.75f && inputPosition.x > 0.4f && inputPosition.x < 0.7f)
                StartCoroutine(ThrowCoroutine());
            else
                ReturnHand();
        }

        void ReturnHand()
        {
            _hand.ArrangeCardDeck();
            transform.DOMove(_hand.GetTransform.position, 0.5f)
                .OnStart(() => _collider.enabled = false)
                .OnComplete(() => _collider.enabled = true);
        }

        IEnumerator DrawCoroutine()
        {
            Sequence drawSequence = DOTween.Sequence();
            drawSequence.Append(transform.DOMoveY(1, 0.5f))
                .Append(transform.DORotate(new Vector3(0, 0, 0), 0.5f))
                .Append(transform.DOMove(_hand.GetTransform.position, 1));

            yield return drawSequence.WaitForCompletion();

            if (!_hand.Cards.Contains(this))
                _hand.Cards.Add(this);

            _transform.SetParent(_hand.GetTransform);
            _collider.enabled = true;
            _hand.ArrangeCardDeck();
        }

        IEnumerator ThrowCoroutine()
        {
            if (!_table.Cards.Contains(this))
                _table.Cards.Add(this);

            _collider.enabled = false;
            _hand.ArrangeCardDeck();

            Sequence throwSequence = DOTween.Sequence();
            throwSequence.Append(_transform.DOMove(_table.GetTransform.position, 1f))
                .Append(transform.DORotate(new Vector3(45, 0, 0), 0.3f));

            yield return throwSequence.WaitForCompletion();
            TriggerActionAfterThrown();
        }
    }
}