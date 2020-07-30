using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace Methodyca.Minigames.Questioniser
{
    public class ItemCard : CardBase
    {
        QuizUI _quiz;

        protected override void Throw()
        {
            StartCoroutine(ThrowCoroutine());
        }

        public override void Discard()
        {
            _transform.DOScale(0, 0.25f).OnComplete(() => Destroy(gameObject));
        }

        void OnEnable()
        {
            _quiz = GameManager.Instance.QuizGUI;
        }

        IEnumerator ThrowCoroutine()
        {
            if (!_table.Cards.Contains(this))
                _table.Cards.Add(this);

            _transform.SetParent(_table.GetTransform);
            _table.ArrangeCardDeck();
            _collider.enabled = false;
            _hand.ArrangeCardDeck();

            Sequence throwSequence = DOTween.Sequence();
            yield return throwSequence.Append(_transform.DOMove(_table.GetTransform.position, 0.5f))
                .Join(_transform.DORotate(new Vector3(45, 0, 0), 0.5f)).WaitForCompletion();

            GameManager.Instance.ActionPoint -= _data.ActionPoint;
            _quiz.gameObject.SetActive(true);

            foreach (var q in _data.Questions)
                if (q.Topic.Name == GameManager.Instance.CurrentTopic.Name)
                    _quiz.SetQuiz(q);
        }
    }
}