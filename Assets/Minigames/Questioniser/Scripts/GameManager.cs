using System.Collections.Generic;
using UnityEngine;

namespace Methodyca.Minigames.Questioniser
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] Camera sceneCamera;
        [SerializeField] int maxInterestPoint = 10;
        [SerializeField] int initialCardSize = 5;
        [SerializeField] CardBase cardPrefab;
        [SerializeField] CardHolder hand;
        [SerializeField] CardHolder table;
        [SerializeField] Transform deck;
        [SerializeField] List<CardData> cardData;

        public int InterestPoint { get; set; } = 0;
        public int GetDeckSize => _cardsInDeck.Count;
        public List<CardData> CardData => cardData;

        List<CardBase> _cardsInDeck;

        void Start()
        {
            _cardsInDeck = new List<CardBase>(GetSpawnedCards());
            DrawRandomCardFromDeck(initialCardSize);
        }

        void OnEnable()
        {
            QuizManager.Instance.OnAnswerSelected += AnswerSelectedHandler;
        }

        void AnswerSelectedHandler(Answer answer)
        {
            if (InterestPoint < maxInterestPoint)
                InterestPoint += answer.Score;

            if (answer.IsCorrect)
                DrawRandomCardFromDeck();
        }

        IEnumerable<CardBase> GetSpawnedCards()
        {
            for (int j = 0; j < cardData.Count; j++)
            {
                for (int i = 0; i < cardData[i].SpawnSize; i++)
                {
                    var spawned = Instantiate(cardPrefab);
                    spawned.InitializeCard(sceneCamera, cardData[i], hand, table);
                    spawned.transform.position = deck.position;
                    yield return spawned;
                }
            }
        }

        void DrawRandomCardFromDeck(int size = 1)
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
            QuizManager.Instance.OnAnswerSelected -= AnswerSelectedHandler;
        }
    }
}