using System;
using UnityEngine;
using DG.Tweening;

namespace Methodyca.Minigames.Questioniser
{
    public class CardBase : MonoBehaviour
    {
        const float SELECTION_SCALE = 1.2F;
        const float SELECTION_SCALE_PACE_IN_SEC = 0.25F;
        readonly int outlineColor = Shader.PropertyToID("_OutlineColor");

        [Header("Base Attributes")]
        [SerializeField] new string name;
        [SerializeField] int costPoint;
        [SerializeField] int spawnSize;
        [SerializeField] Sprite sprite;
        [SerializeField] Sprite infoSprite;
        [SerializeField] GameObject cardBack;

        public string Name => name;
        public int CostPoint { get => costPoint; protected set { costPoint = value; OnCostChanged?.Invoke(costPoint); } }
        public int SpawnSize => spawnSize;
        public Sprite CardSprite => sprite;
        public Sprite InfoSprite => infoSprite;
        public float SpriteSizeX => sprite.bounds.size.x;

        protected bool _isThrown;
        protected Camera _camera;
        protected Transform _transform;
        protected Collider2D _collider;
        protected CardHolder _hand;
        protected CardHolder _table;
        protected CardHolder _deck;
        protected GameManager _gameManager;

        int _currentGamePoint;
        SpriteRenderer _renderer;

        public Tweener DiscardTweener { get; private set; }
        public event Action<int> OnCostChanged = delegate { };
        public event Action<CardBase> OnCardClicked = delegate { };
        public event EventHandler<OnCardThrownEventArgs> OnCardThrown;
        public class OnCardThrownEventArgs : EventArgs { public CardBase Card; }
        protected void TriggerCardIsThrown(CardBase card) => OnCardThrown?.Invoke(this, new OnCardThrownEventArgs { Card = card });
        protected virtual void Throw() { }

        public void InitializeCard(Camera camera, CardHolder hand, CardHolder table, CardHolder deck)
        {
            _camera = camera;
            _hand = hand;
            _table = table;
            _deck = deck;
        }

        public void Draw()
        {
            DOTween.Sequence().OnStart(() =>
                {
                    _gameManager.GameState = GameState.Busy;
                })
                .Append(_transform.DOMoveY(_collider.bounds.extents.y, 0.25f))
                .Append(_transform.DORotate(Vector3.zero, 0.5f))
                .InsertCallback(0.55f,()=> cardBack.SetActive(false))
                .Join(_transform.DOMove(_hand.GetTransform.position, 0.5f)).AppendCallback(() =>
                {
                    _isThrown = false;
                    _hand.AddCard(this);
                    _hand.ArrangeCards();
                    _transform.SetParent(_hand.GetTransform);
                    _gameManager.GameState = GameState.Playable;
                });
        }

        public void ReturnDeck()
        {
            DOTween.Sequence().OnStart(() =>
                {
                    _gameManager.GameState = GameState.Busy;
                })
                .Append(_transform.DOMoveY(_collider.bounds.extents.y, 0.25f))
                .AppendCallback(() => _hand.RemoveCard(this))
                .Join(_transform.DORotate(new Vector3(0, -180, 0), 0.25f))
                .InsertCallback(.3f, () => cardBack.SetActive(true))
                .Append(_transform.DOMove(_deck.GetTransform.position, 0.3f))
                .AppendCallback(() =>
                {
                    _deck.AddCard(this);
                    _transform.SetParent(_deck.GetTransform);
                });
        }

        public void ReturnHand()
        {
            DOTween.Sequence().Append(_transform.DOMove(_hand.GetTransform.position, 0.25f)
                .OnStart(() =>
                {
                    _gameManager.GameState = GameState.Busy;
                    _hand.AddCard(this);
                    _hand.ArrangeCards();
                })
                .OnComplete(() =>
                {
                    _gameManager.GameState = GameState.Playable;
                }))
                .Join(_transform.DOScale(1, 0.25f));
        }

        public void Discard()
        {
            DiscardTweener.Restart();
        }

        public void SelectCard()
        {
            _transform.DOScale(1.25f, 0.2f);
            Debug.Log("Card Selected");
        }

        public void DeselectCard()
        {
            _transform.DOScale(1f, 0.2f);
        }

        public void SetOutlineColorAs(Color color)
        {
            _renderer.material.SetColor(outlineColor, color);
            Debug.Log("Setting Outline Colors");
            
        }

        void Awake()
        {
            _transform = transform;
            _collider = GetComponent<Collider2D>();
            _renderer = GetComponent<SpriteRenderer>();
            _renderer.sprite = sprite;

            if (GameManager.TryGetInstance(out GameManager manager))
                _gameManager = manager;

            SetDiscardTweener();
        }

        void SetDiscardTweener()
        {
            DiscardTweener = _transform.DOScale(0, 0.25f).OnComplete(() =>
                                {
                                    if (_hand.Cards.Contains(this))
                                        _hand.Cards.Remove(this);
                                    else if (_table.Cards.Contains(this))
                                        _table.Cards.Remove(this);

                                    Destroy(gameObject);
                                }).SetAutoKill(false);

            DiscardTweener.Pause();
        }

        void OnMouseEnter()
        {
            if (_gameManager.GameState != GameState.Playable || _isThrown)
                return;

            _renderer.material.SetColor(outlineColor, Color.red);
            FindObjectOfType<SoundManager>().StereoImaging("CardHighSFX");
            FindObjectOfType<SoundManager>().Play("CardHighSFX");
            //Debug.Log("Sighted");
        }

        void OnMouseExit()
        {
            if (_gameManager.GameState != GameState.Playable || _isThrown)
                return;

            _renderer.material.SetColor(outlineColor, Color.clear);
        }

        void OnMouseDown()
        {
            if (_gameManager.GameState != GameState.Playable || _isThrown)
                return;

            _hand.RemoveCard(this);
            _hand.ArrangeCards();
            _transform.DOScale(SELECTION_SCALE, SELECTION_SCALE_PACE_IN_SEC);
        }

        void OnMouseUp()
        {
            if (_gameManager.GameState != GameState.Playable || _isThrown)
                return;

            _transform.DOScale(Vector2.one, SELECTION_SCALE_PACE_IN_SEC);
            var inputPosition = (Vector2)_camera.ScreenToViewportPoint(Input.mousePosition);

            if (this is ItemCard)
                _currentGamePoint = _gameManager.ActionPoint;
            if (this is ActionCard)
                _currentGamePoint = _gameManager.InterestPoint;

            if (inputPosition.y < 0.45f || inputPosition.y > 0.9f || inputPosition.x < 0.25f || inputPosition.x > 0.75f)
                ReturnHand();
            else if (_currentGamePoint >= CostPoint)
                Throw();
            else
            {
                ReturnHand();
                _gameManager.SendGameMessage("Not enough points");
            }
        }

        void OnMouseUpAsButton()
        {
            if (_gameManager.GameState != GameState.Selectable || _isThrown)
                return;

            _renderer.material.SetColor(outlineColor, Color.green);
            OnCardClicked?.Invoke(this);
        }

        void OnMouseDrag()
        {
            if (_gameManager.GameState != GameState.Playable || _isThrown)
                return;

            var distance = Vector3.Distance(_transform.position, _camera.transform.position);
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            Vector2 rayPoint = ray.GetPoint(distance);
            _transform.position = rayPoint;
        }
    }
}