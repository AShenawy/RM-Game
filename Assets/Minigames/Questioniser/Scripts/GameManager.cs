using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Methodyca.Minigames.Questioniser
{
    [Serializable]
    public struct Topic
    {
        public string Name;
        public bool IsCompleted;
    }

    [Serializable]
    public struct Question
    {
        public string QuestionText;
        public Topic Topic;
        public Answer[] Answers;
    }

    [Serializable]
    public struct Answer
    {
        public bool IsCorrect;
        public int Point;
        [Multiline] public string AnswerText;
    }

    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] Camera sceneCamera;
        [SerializeField] QuizUI quizGUI;
        [SerializeField] CardInfoUI cardInfoGUI;
        [SerializeField] CardBase cardPrefab;
        [SerializeField] CardHolder hand;
        [SerializeField] CardHolder table;
        [SerializeField] Transform deck;
        [SerializeField] List<CardData> cardData;
        [SerializeField] List<Topic> topics;

        public event Action OnGameOver = delegate { };
        public event Action<int> OnActionPointChanged = delegate { };
        public event Action<float> OnInterestPointChanged = delegate { };

        int _actionPoint = 5;
        float _interestPoint = 0;
        readonly float _maxInterestPoint = 100f;
        List<CardBase> _cardsInDeck;

        public CardInfoUI CardInfoGUI => cardInfoGUI;
        public QuizUI QuizGUI => quizGUI;
        public Topic CurrentTopic { get; set; }
        public float InterestPoint
        {
            get => _interestPoint;
            set
            {
                _interestPoint = value;
                if (_interestPoint >= _maxInterestPoint || _interestPoint <= 0)
                    OnGameOver?.Invoke();
                else
                    OnInterestPointChanged?.Invoke(_interestPoint / _maxInterestPoint);
            }
        }
        public int ActionPoint
        {
            get => _actionPoint;
            set
            {
                _actionPoint = value;
                OnActionPointChanged?.Invoke(_actionPoint);
            }
        }

        public void EndTurn()
        {
            DiscardCards();
            DrawRandomCardFromDeck(5);
            OnActionPointChanged?.Invoke(_actionPoint += 5);
        }

        void Start()
        {
            CurrentTopic = topics[UnityEngine.Random.Range(0, topics.Count)];
            _cardsInDeck = GetSpawnedCards().ToList();
            DrawRandomCardFromDeck(5);
        }

        void DrawRandomCardFromDeck(int size = 1)
        {
            if (_cardsInDeck.Count <= 0)
                OnGameOver?.Invoke();

            StartCoroutine(DrawInDelay(size));
        }

        void DiscardCards()
        {
            foreach (var card in hand.Cards)
                card.Discard();

            foreach (var card in table.Cards)
                card.Discard();

            hand.Cards.Clear();
            table.Cards.Clear();
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