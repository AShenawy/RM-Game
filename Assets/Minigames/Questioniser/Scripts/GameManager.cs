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
        public event Action<Topic> OnTopicChanged = delegate { };
        public event Action<Question> OnQuestionAsked = delegate { };
        public event Action<int> OnActionPointChanged = delegate { };
        public event Action<float> OnInterestPointChanged = delegate { };

        readonly float _maxInterestPoint = 100f;
        List<CardBase> _cardsInDeck;

        public CardInfoUI CardInfoGUI => cardInfoGUI;
        public Topic CurrentTopic { get; private set; }
        public int ActionPoint { get; private set; } = 5;
        public float InterestPoint { get; private set; } = 0;

        public void EndTurn()
        {
            DiscardCards();
            DrawRandomCardFromDeck(5); //Add validation check
            OnActionPointChanged?.Invoke(ActionPoint += 5);
        }

        public void SetRandomTopic() //Change Later
        {
            var topic = topics[UnityEngine.Random.Range(0, topics.Count)];
            if (!topic.IsCompleted)
            {
                CurrentTopic = topic;
                OnTopicChanged?.Invoke(topic);
            }
        }

        public void HandleItemCardQuestionFor(Answer answer)
        {
            InterestPoint += answer.Point;

            if (InterestPoint >= _maxInterestPoint || InterestPoint <= 0)
                OnGameOver?.Invoke();
            else
                OnInterestPointChanged?.Invoke(InterestPoint / _maxInterestPoint);

            quizGUI.gameObject.SetActive(false);
        }

        void CardThrownHandler(object sender, CardBase.OnCardThrownEventArgs e)
        {
            if (sender is ItemCard itemCard)
            {
                OnActionPointChanged?.Invoke(ActionPoint -= e.Card.GetData.ActionPoint);

                foreach (var q in itemCard.GetData.Questions)
                    if (q.Topic.Name == CurrentTopic.Name && !q.Topic.IsCompleted)
                        quizGUI.AskQuestion(q); // Replace it with event later
            }

            if (sender is MetaCard metaCard)
            {
                // Add validation check
                InterestPoint -= e.Card.GetData.InterestPoint;
                OnInterestPointChanged?.Invoke(InterestPoint / _maxInterestPoint);
            }
        }

        void Start()
        {
            _cardsInDeck = GetSpawnedCards().ToList();
            SetRandomTopic();
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
                yield return new WaitForSeconds(0.5f);
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
                    spawned.OnCardThrown += CardThrownHandler;
                    yield return spawned;
                }
            }
        }
    }
}