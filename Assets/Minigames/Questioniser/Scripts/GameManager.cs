using System;
using System.Collections.Generic;
using UnityEngine;

namespace Methodyca.Minigames.Questioniser
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] Camera sceneCamera;
        [SerializeField] int maxInterestPoint = 10;
        [SerializeField] int initialCardSize = 5;
        [SerializeField] Card cardPrefab;
        [SerializeField] CardHolder hand;
        [SerializeField] CardHolder table;
        [SerializeField] Transform deck;
        [SerializeField] List<CardData> cardData;

        public static event Action OnGameOver = delegate { };

        public int InterestPoint { get; set; } = 0;
        public List<CardData> CardData => cardData;
        public int GetDeckSize => _cardsInDeck.Count;

        List<Card> _cardsInDeck;

        void Start()
        {
            _cardsInDeck = new List<Card>(GetSpawnedCards());
            DrawRandomCardsOf(initialCardSize);
        }

        void OnEnable()
        {
            Card.OnCardThrown += CardThrownHandler;
            QuizManager.OnAnswerSelected += AnswerSelectedHandler;
        }

        void AnswerSelectedHandler(Answer answer)
        {
            if (InterestPoint < maxInterestPoint)
                InterestPoint += answer.Score;
            else
                OnGameOver?.Invoke();
        }

        void CardThrownHandler(Card card)
        {
            QuizManager.Instance.SetQuiz();
        }

        IEnumerable<Card> GetSpawnedCards()
        {
            for (int j = 0; j < cardData.Count; j++)
            {
                for (int i = 0; i < cardData[i].SpawnSize; i++)
                {
                    var card = Instantiate(cardPrefab);
                    card.InitializeCard(sceneCamera, cardData[i], hand, table);
                    card.transform.position = deck.position;
                    yield return card;
                }
            }
        }

        void DrawRandomCardsOf(int size)
        {
            for (int i = 0; i < size; i++)
            {
                int index = UnityEngine.Random.Range(0, _cardsInDeck.Count);
                var randomCard = _cardsInDeck[index];
                randomCard.SetHolder(hand);
                _cardsInDeck.RemoveAt(index);
            }
        }

        void OnDisable()
        {
            Card.OnCardThrown -= CardThrownHandler;
            QuizManager.OnAnswerSelected -= AnswerSelectedHandler;
        }
    }
}