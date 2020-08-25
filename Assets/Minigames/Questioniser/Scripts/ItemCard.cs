using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace Methodyca.Minigames.Questioniser
{
    public class ItemCard : CardBase
    {
        [Header("ItemCard Attributes")]
        [SerializeField] Question[] questions;
        public Question[] Questions => questions;

        CardInfo _cardInfo;

        void Start() => _cardInfo = GetComponentInChildren<CardInfo>();

        protected override void Throw()
        {
            StopCoroutine(ThrowCor());
            StartCoroutine(ThrowCor());
        }

        IEnumerator ThrowCor()
        {
            yield return DOTween.Sequence().OnStart(() =>
            {
                _isThrown = true;
                _table.Cards.Add(this);
                _table.ArrangeCards();
                _transform.SetParent(_table.GetTransform);
                _gameManager.GameState = GameState.Busy;
                _cardInfo.gameObject.SetActive(false);
                SetOutlineColorAs(Color.clear);
            })
            .Append(_transform.DOMove(_table.GetTransform.position, 0.5f))
            .Join(_transform.DORotate(new Vector3(30, 0, 0), 0.5f)).WaitForCompletion();

            TriggerCardIsThrown(this);
        }
    }
}