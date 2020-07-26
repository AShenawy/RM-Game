using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Methodyca.Minigames.Questioniser
{
    [Serializable]
    public struct Question
    {
        public string QuestionText;
        public Answer[] Answers;
    }

    [Serializable]
    public struct Answer
    {
        public string AnswerText;
        public int Score;
        public bool IsCorrect;
    }

    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] Camera sceneCamera;
        [SerializeField] GameObject quizObject;
        [SerializeField] CardBase cardPrefab;
        [SerializeField] CardHolder hand;
        [SerializeField] CardHolder table;
        [SerializeField] Transform deck;
        [SerializeField] int actionPoint = 10;
        [SerializeField] List<CardData> cardData;

        public int GetDeckSize => _cardsInDeck.Count;
        public List<CardData> CardData => cardData;
        public event Action<int, float> OnScoreChanged = delegate { };
        public event Action<Question> OnQuestionAsked = delegate { };

        int _interestPoint = 0;
        readonly int _maxInterestPoint = 100;
        List<CardBase> _cardsInDeck;

        //Find a better way
        public void UpdateTurn(CardBase cardBase)
        {
            quizObject.SetActive(true); // Can be animated

            if (cardBase.ActionPoint > 0)
                actionPoint -= cardBase.ActionPoint;

            OnQuestionAsked?.Invoke(cardBase.Question);
        }

        public void SelectAnswer(Answer answer)
        {
            if (_interestPoint < _maxInterestPoint)
                _interestPoint += answer.Score;

            OnScoreChanged?.Invoke(actionPoint, _interestPoint / _maxInterestPoint);

            if (answer.IsCorrect)
                DrawRandomCardFromDeck();

            quizObject.SetActive(false); // Can be animated
        }

        void Start()
        {
            _cardsInDeck = GetSpawnedCards().ToList();
            DrawRandomCardFromDeck(4);
        }

        void DrawRandomCardFromDeck(int size = 1)
        {
            if (_cardsInDeck.Count <= 0)
                return;

            StartCoroutine(DrawInDelay(size));
        }

        IEnumerator DrawInDelay(int size)
        {
            for (int i = 0; i < size; i++)
            {
                int index = UnityEngine.Random.Range(0, _cardsInDeck.Count);
                _cardsInDeck[index].Draw();
                _cardsInDeck.RemoveAt(index);
                yield return new WaitForSeconds(0.75f);
            }
        }

        IEnumerable<CardBase> GetSpawnedCards()
        {
            for (int j = 0; j < cardData.Count; j++)
            {
                for (int i = 0; i < cardData[j].SpawnSize; i++)
                {
                    var spawned = Instantiate(cardPrefab, deck);
                    spawned.transform.position = deck.position;
                    spawned.InitializeCard(sceneCamera, cardData[j], hand, table);
                    yield return spawned;
                }
            }
        }
    }
}