using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Methodyca.Minigames.Questioniser
{
    [Serializable]
    public class Topic
    {
        public string Name;
        public bool IsStoryInitiated;
        public Sprite CardSprite;

        public Topic(string name, bool isStoryInitiated, Sprite cardSprite)
        {
            Name = name;
            IsStoryInitiated = isStoryInitiated;
            CardSprite = cardSprite;
        }
    }

    [Serializable]
    public struct Question
    {
        public bool IsAnswerCorrect;
        public string TopicName;
        public string OptionDescription;
        public Option[] Options;
    }

    [Serializable]
    public struct Option
    {
        public bool IsCorrect;
        public int Id;
        public int Point;
        [Multiline] public string Text;
    }

    public class GameManager : Singleton<GameManager>
    {
        const byte DRAW_COUNT_PER_TURN = 5;
        const byte ACTION_POINT_PER_TURN = 5;

        [SerializeField] Camera sceneCamera;
        [SerializeField] CardInfoUI cardInfoGUI;
        [SerializeField] CardHolder hand;
        [SerializeField] CardHolder table;
        [SerializeField] Transform deck;
        [SerializeField] List<CardBase> cards;
        [SerializeField] List<Topic> topics;

        public event Action OnTopicClosed = delegate { };
        public event Action OnGameOver = delegate { };
        public event Action<Topic> OnTopicChanged = delegate { };
        public event Action<Question> OnQuestionAsked = delegate { };
        public event Action<int> OnActionPointUpdated = delegate { };
        public event Action<float> OnInterestPointUpdated = delegate { };
        public event Action<bool> OnStoryInitiated = delegate { };
        public event Action<string, string, bool> OnChartUpdated = delegate { };
        public event Action<string> OnMessageRaised = delegate { };
        public event Action<byte> OnDeckUpdated = delegate { };

        int _actionPoint = 5;
        int _interestPoint = 2;
        int _extraPointsForNextTurn = 0;
        Topic _currentTopic = new Topic("", false, null);
        CardBase _currentCard;
        List<CardBase> _cardsInDeck;
        HashSet<ItemCard> _correctItemCardsPerTurn;
        bool[,] _quizAnswerSheet;

        public int GetCorrectOptionCountPerTurn => _correctItemCardsPerTurn.Count;
        public CardInfoUI CardInfoGUI => cardInfoGUI;
        public int ActionPoint
        {
            get => _actionPoint;
            set
            {
                _actionPoint = value;
                if (_actionPoint <= 0)
                    _actionPoint = 0;
                OnActionPointUpdated?.Invoke(_actionPoint);
            }
        }
        public int InterestPoint
        {
            get => _interestPoint;
            set
            {
                _interestPoint = value;
                if (_interestPoint <= 0)
                {
                    _interestPoint = 0;
                    OnGameOver?.Invoke();
                }
            }
        }
        public byte StoryPoint { get; private set; } = 0;
        public int GetTopicCount => topics.Count;
        public void RaiseGameMessage(string message) => OnMessageRaised?.Invoke(message);

        public void DrawRandomCardFromDeck(byte size = 1)
        {
            if (_cardsInDeck.Count <= 0)
                OnGameOver?.Invoke();

            StartCoroutine(DrawInDelay(size));
        }

        public void EndTurn()
        {
            _correctItemCardsPerTurn = new HashSet<ItemCard>();
            DiscardCards();

            if (_cardsInDeck.Count < DRAW_COUNT_PER_TURN)
                DrawRandomCardFromDeck((byte)_cardsInDeck.Count);
            else if (_cardsInDeck.Count <= 0)
                OnGameOver?.Invoke();
            else
                DrawRandomCardFromDeck(DRAW_COUNT_PER_TURN);

            OnActionPointUpdated?.Invoke(ACTION_POINT_PER_TURN + _extraPointsForNextTurn);
            _extraPointsForNextTurn = 0;
        }

        public void SwitchInterestToActionPoint(int interestPoint, int actionPoint)
        {
            OnActionPointUpdated?.Invoke(ActionPoint += actionPoint);
            InterestPoint += interestPoint;
            OnInterestPointUpdated?.Invoke(InterestPoint);
        }

        public void SetRandomTopic()
        {
            if (topics.Count <= 0)
                return;

            var topic = topics[UnityEngine.Random.Range(0, topics.Count)];

            while (_currentTopic.Name == topic.Name)
                topic = topics[UnityEngine.Random.Range(0, topics.Count)];

            _currentTopic = topic;
            OnTopicChanged?.Invoke(_currentTopic);
        }

        public void HandleHighFlayer()
        {
            _extraPointsForNextTurn += _correctItemCardsPerTurn.Count;
        }

        //public void HandleImproviser()
        //{

        //}

        public void HandleStoryDialog()
        {
            topics.Remove(_currentTopic);
            SetRandomTopic();
            OnTopicClosed?.Invoke();
        }

        public void HandleItemCardQuestionFor(Option answer)
        {
            CardBase.IsClickable = true;
            InterestPoint += answer.Point;
            OnInterestPointUpdated?.Invoke(InterestPoint);

            var itemCard = _currentCard.GetComponent<ItemCard>();
            if (answer.IsCorrect)
            {
                for (int i = 0; i < itemCard.Questions.Length; i++)
                {
                    if (itemCard.Questions[i].TopicName == _currentTopic.Name)
                    {
                        itemCard.Questions[i].IsAnswerCorrect = true;
                        _correctItemCardsPerTurn.Add(itemCard);
                        TickAnswerSheet(_currentTopic.Name, _currentCard.Name);

                        if (GetCorrectAnswerCountFor(_currentTopic.Name) >= 2)
                            OnStoryInitiated?.Invoke(_currentTopic.IsStoryInitiated = true);
                    }
                }
            }
        }

        void CardThrownHandler(object sender, CardBase.OnCardThrownEventArgs e)
        {
            if (sender is ItemCard itemCard)
            {
                _currentCard = itemCard;
                OnActionPointUpdated?.Invoke((ActionPoint -= e.Card.ActionPoint) <= 0 ? 0 : ActionPoint);

                foreach (var q in itemCard.Questions)
                {
                    if (_currentTopic.Name == q.TopicName && !q.IsAnswerCorrect)
                        OnQuestionAsked?.Invoke(q);
                    else if (q.IsAnswerCorrect)
                        RaiseGameMessage("The proper option was already selected before");
                }
            }

            if (sender is ActionCard actionCard)
            {
                _currentCard = actionCard;
                InterestPoint -= e.Card.InterestPoint;
                OnInterestPointUpdated?.Invoke(InterestPoint);
            }
        }

        void Start()
        {
            _quizAnswerSheet = new bool[topics.Count, cards.Count];
            _cardsInDeck = GetSpawnedCards().ToList();

            SetRandomTopic();
            DrawRandomCardFromDeck(5);
        }

        void TickAnswerSheet(string topicName, string cardName)
        {
            for (int i = 0; i < topics.Count; i++)
                if (topicName == topics[i].Name)
                    for (int j = 0; j < cards.Count; j++)
                        if (cardName == cards[j].Name)
                            _quizAnswerSheet[i, j] = true;
        }

        byte GetCorrectAnswerCountFor(string topicName)
        {
            byte num = 0;
            for (int i = 0; i < topics.Count; i++)
                if (topicName == topics[i].Name)
                    for (int j = 0; j < cards.Count; j++)
                        if (_quizAnswerSheet[i, j])
                            num++;
            return num;
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
                OnDeckUpdated?.Invoke((byte)_cardsInDeck.Count);
                yield return new WaitForSeconds(0.5f);
            }
        }

        IEnumerable<CardBase> GetSpawnedCards()
        {
            for (int j = 0; j < cards.Count; j++)
            {
                for (int i = 0; i < cards[j].SpawnSize; i++)
                {
                    var spawned = Instantiate(cards[j], deck);
                    spawned.transform.position = deck.position;
                    spawned.InitializeCard(sceneCamera, hand, table);
                    spawned.OnCardThrown += CardThrownHandler;
                    yield return spawned;
                }
            }
        }
    }
}