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
        public Question Question;
    }

    [Serializable]
    public struct Question
    {
        public bool IsAnswerCorrect;
        public string QuestionText;
        public Answer[] Answers;

        public void SetAnswerAs(bool isCorrect)
        {
            IsAnswerCorrect = isCorrect;
        }
    }

    [Serializable]
    public struct Answer
    {
        public bool IsCorrect;
        public int Id;
        public int Point;
        [Multiline] public string AnswerText;
    }

    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] Camera sceneCamera;
        [SerializeField] CardInfoUI cardInfoGUI;
        [SerializeField] CardBase cardPrefab;
        [SerializeField] CardHolder hand;
        [SerializeField] CardHolder table;
        [SerializeField] Transform deck;
        [SerializeField] List<CardData> cardData;
        [SerializeField] List<string> topics;

        public event Action OnGameOver = delegate { };
        public event Action<string> OnTopicChanged = delegate { };
        public event Action<Question> OnQuestionAsked = delegate { };
        public event Action<int> OnActionPointChanged = delegate { };
        public event Action<float> OnInterestPointChanged = delegate { };
        public event Action<string, string> OnStoryPointRaised = delegate { };
        public event Action<string, string, bool> OnChartUpdated = delegate { };

        string _currentTopicName;
        CardData _currentCardData;
        List<CardBase> _cardsInDeck;
        Dictionary<string, byte> _availableTopics = new Dictionary<string, byte>();
        readonly float _maxInterestPoint = 100f;

        public CardInfoUI CardInfoGUI => cardInfoGUI;
        public int ActionPoint { get; private set; } = 5;
        public float InterestPoint { get; private set; } = 0;

        public void EndTurn()
        {
            DiscardCards();
            DrawRandomCardFromDeck(5); //Add validation check
            OnActionPointChanged?.Invoke(ActionPoint += 5);
        }

        public void SetRandomTopic()
        {
            var topic = topics[UnityEngine.Random.Range(0, topics.Count)];

            while (_currentTopicName == topic)
                topic = topics[UnityEngine.Random.Range(0, topics.Count)];

            _currentTopicName = topic;
            OnTopicChanged?.Invoke(_currentTopicName);
        }

        public void HandleStoryPointDialog()
        {
            topics.Remove(_currentTopicName);
            _currentTopicName = "";
            SetRandomTopic();
        }
        bool[][] topicCardAnswerSheet;

        public void HandleItemCardQuestionFor(Answer answer)
        {
            InterestPoint += answer.Point;

            if (answer.IsCorrect)
            {
                for (int i = 0; i < _currentCardData.Topics.Length; i++)
                {
                    if (_currentCardData.Topics[i].Name == _currentTopicName)
                    {

                    }
                }

                for (int i = 0; i < _currentCardData.Topics.Length; i++)
                {
                    var currentTopic = _currentCardData.Topics[i];
                    if (currentTopic.Name == _currentTopicName)
                    {
                        Debug.Log("Answer is " + currentTopic.Question.IsAnswerCorrect);
                        //currentTopic.Question.IsAnswerCorrect = true;
                        //var storyPoint = _availableTopics[_currentTopicName]++;

                        //OnChartUpdated?.Invoke(_currentTopicName, _currentCardData.Name, storyPoint >= 3);

                        //if (storyPoint >= 3)
                        //    OnStoryPointRaised?.Invoke(_currentTopicName, _currentCardData.Name);
                        //else if (storyPoint >= cardData.Count)
                        //    _availableTopics[_currentTopicName] = (byte)cardData.Count;
                    }
                }

                //foreach (var tq in _topicQuestionPairsOfCards)
                //{
                //    if (tq. == _currentCardData.name)
                //    {
                //        Debug.Log("answer is " + tq.Value.IsAnswerCorrect);

                //    }
                //}
            }

            if (InterestPoint >= _maxInterestPoint || InterestPoint <= 0)
                OnGameOver?.Invoke();
            else
                OnInterestPointChanged?.Invoke(InterestPoint / _maxInterestPoint);
        }

        void CardThrownHandler(object sender, CardBase.OnCardThrownEventArgs e)
        {
            if (sender is ItemCard itemCard)
            {
                OnActionPointChanged?.Invoke(ActionPoint -= e.Card.GetData.ActionPoint);
                _currentCardData = itemCard.GetData;

                foreach (var t in itemCard.GetData.Topics)
                    if (_currentTopicName == t.Name && !t.Question.IsAnswerCorrect)
                        OnQuestionAsked?.Invoke(t.Question);
            }

            if (sender is MetaCard metaCard)
            {
                // Add validation check
                InterestPoint -= e.Card.GetData.InterestPoint;
                OnInterestPointChanged?.Invoke(InterestPoint / _maxInterestPoint);
            }
        }
        List<KeyValuePair<CardData, Question>> _topicQuestionPairsOfCards = new List<KeyValuePair<CardData, Question>>();
        void Start()
        {
            foreach (var t in topics)
                if (!_availableTopics.ContainsKey(t))
                    _availableTopics.Add(t, 0);
            SetGrid();

            //var lookUp = _topicQuestionPairsOfCards.ToLookup(kvp => kvp.Key, kvp => kvp.Value.IsAnswerCorrect);

            _cardsInDeck = GetSpawnedCards().ToList();
            SetRandomTopic();
            DrawRandomCardFromDeck(5);
        }

        private void SetGrid()
        {
            
            //foreach (var card in cardData)
            //    foreach (var topic in card.Topics)
            //        _topicQuestionPairsOfCards.Add(new KeyValuePair<CardData, Question>(card, topic.Question));
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