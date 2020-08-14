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
        const byte POINT_TO_INITIATE_STORY = 4;

        [SerializeField] Camera sceneCamera;
        [SerializeField] CardInfoUI cardInfoGUI;
        [SerializeField] CardHolder hand;
        [SerializeField] CardHolder table;
        [SerializeField] CardHolder deck;
        [SerializeField] List<CardBase> cards;
        [SerializeField] List<Topic> topics;

        public event Action OnTopicClosed = delegate { };
        public event Action OnGameOver = delegate { };
        public event Action<Topic> OnTopicChanged = delegate { };
        public event Action<Question> OnQuestionAsked = delegate { };
        public event Action<byte> OnDeckUpdated = delegate { };
        public event Action<int, int> OnActionPointUpdated = delegate { };
        public event Action<int, int> OnInterestPointUpdated = delegate { };
        public event Action<bool> OnStoryInitiated = delegate { };
        public event Action<bool> OnMulliganStated = delegate { };
        public event Action<bool> OnImproviserRaised = delegate { };
        public event Action<string> OnMessageRaised = delegate { };
        public event Action<string, string> OnQuizGridUpdated = delegate { };

        int _lastValue;
        int _actionPoint = 5;
        int _interestPoint = 2;
        int _storyPoint = 5;
        int _extraPointsForNextTurn = 0;
        bool _isImproviserTurn;
        CardBase _currentCard;
        Topic _currentTopic = new Topic("", false, null);
        HashSet<ItemCard> _correctItemCardsPerTurn = new HashSet<ItemCard>();
        bool[,] _quizAnswerSheet;

        public CardInfoUI CardInfoGUI => cardInfoGUI;
        public int ActionPoint
        {
            get => _actionPoint;
            set
            {
                _lastValue = _actionPoint;
                _actionPoint = value;
                if (_actionPoint <= 0)
                {
                    _actionPoint = 0;
                    OnActionPointUpdated?.Invoke(_actionPoint, _lastValue);
                }
                else
                {
                    OnActionPointUpdated?.Invoke(_actionPoint, _lastValue);
                }
            }
        }
        public int InterestPoint
        {
            get => _interestPoint;
            set
            {
                _lastValue = _interestPoint;
                _interestPoint = value;
                if (_interestPoint <= 0)
                {
                    _interestPoint = 0;
                    OnInterestPointUpdated?.Invoke(_interestPoint, _lastValue);
                    OnGameOver?.Invoke();
                }
                else
                {
                    OnInterestPointUpdated?.Invoke(_interestPoint, _lastValue);
                }
            }
        }

        public int GetTopicCount => topics.Count;
        public void RaiseGameMessage(string message) => OnMessageRaised?.Invoke(message);

        public void DrawRandomCardFromDeck(byte size = 1)
        {
            if (deck.Cards.Count > 0)
                Draw(size);
            else if (hand.Cards.Count >= 5)
                RaiseGameMessage("Cannot hold more than 5 cards");
            else
                OnGameOver?.Invoke();
        }

        public void TriggerMulligan()
        {
            StartCoroutine(MulliganCoroutine());
        }

        public void EndTurn()
        {
            DiscardAllCards();
            OnMulliganStated?.Invoke(true);
            _correctItemCardsPerTurn = new HashSet<ItemCard>();

            if (deck.Cards.Count < DRAW_COUNT_PER_TURN)
                DrawRandomCardFromDeck((byte)deck.Cards.Count);
            else if (deck.Cards.Count <= 0)
                OnGameOver?.Invoke();
            else
                DrawRandomCardFromDeck(DRAW_COUNT_PER_TURN);

            if (_isImproviserTurn)
                OnImproviserRaised?.Invoke(_isImproviserTurn = false);

            ActionPoint = ACTION_POINT_PER_TURN + _extraPointsForNextTurn;
            _extraPointsForNextTurn = 0;
        }

        public void SwitchInterestToActionPoint(int interestPoint, int actionPoint)
        {
            ActionPoint += actionPoint;
            InterestPoint += interestPoint;
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
            OnStoryInitiated?.Invoke(_currentTopic.IsStoryInitiated);
        }

        public void HandleHighFlayer()
        {
            _extraPointsForNextTurn += _correctItemCardsPerTurn.Count;
            RaiseGameMessage($"You will start extra {_correctItemCardsPerTurn.Count} action points next turn");
        }

        public void HandleImproviser()
        {
            OnImproviserRaised?.Invoke(_isImproviserTurn = true);
            RaiseGameMessage("Interest points can be used as action points for this turn");
        }

        public void HandleStory()
        {
            if (InterestPoint >= _storyPoint)
            {
                InterestPoint -= _storyPoint;
                _storyPoint += 3;
            }
            else
            {
                RaiseGameMessage("Not enough interest point");
            }
        }

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

            var itemCard = _currentCard as ItemCard;
            if (answer.IsCorrect)
            {
                for (int i = 0; i < itemCard.Questions.Length; i++)
                {
                    if (itemCard.Questions[i].TopicName == _currentTopic.Name)
                    {
                        ItemCard c = GetPrefabOf(itemCard) as ItemCard;
                        c.Questions[i].IsAnswerCorrect = true;

                        _correctItemCardsPerTurn.Add(itemCard);
                        TickAnswerSheet(_currentTopic.Name, _currentCard.Name);
                        OnQuizGridUpdated?.Invoke(_currentTopic.Name, _currentCard.Name);

                        if (GetCorrectAnswerCountFor(_currentTopic.Name) >= POINT_TO_INITIATE_STORY)
                            OnStoryInitiated?.Invoke(_currentTopic.IsStoryInitiated = true);
                    }
                }
            }
        }

        void CardThrownHandler(object sender, CardBase.OnCardThrownEventArgs e)
        {
            hand.ArrangeCardDeck();
            OnMulliganStated?.Invoke(false);

            if (sender is ItemCard itemCard)
            {
                _currentCard = itemCard;
                ItemCard c = GetPrefabOf(itemCard) as ItemCard;

                if (_isImproviserTurn)
                    InterestPoint -= e.Card.ActionPoint;
                else
                    ActionPoint -= e.Card.ActionPoint;

                foreach (var q in c.Questions)
                {
                    if (_currentTopic.Name == q.TopicName)
                    {
                        if (!q.IsAnswerCorrect)
                            OnQuestionAsked?.Invoke(q);
                        else
                            RaiseGameMessage("The proper option was already selected before");
                    }
                }
            }

            if (sender is ActionCard actionCard)
            {
                _currentCard = actionCard;
                InterestPoint -= e.Card.InterestPoint;
            }
        }

        void Start()
        {
            _quizAnswerSheet = new bool[topics.Count, cards.Count];
            deck.Cards = GetSpawnedCards().ToList();

            SetRandomTopic();
            DrawRandomCardFromDeck(DRAW_COUNT_PER_TURN);
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

        void DiscardAllCards()
        {
            foreach (var card in hand.Cards)
                card.Discard();

            foreach (var card in table.Cards)
                card.Discard();

            hand.Cards.Clear();
            table.Cards.Clear();
        }

        void Draw(int size)
        {
            for (int i = 0; i < size; i++)
            {
                int index = UnityEngine.Random.Range(0, deck.Cards.Count);
                deck.Cards[index].Draw();
                deck.Cards.RemoveAt(index);
                OnDeckUpdated?.Invoke((byte)deck.Cards.Count);
            }
        }
        CardBase GetPrefabOf(CardBase card)
        {
            foreach (var c in cards)
                if (card.Name == c.Name)
                    return c;

            return null;
        }

        IEnumerable<CardBase> GetSpawnedCards()
        {
            for (int j = 0; j < cards.Count; j++)
            {
                for (int i = 0; i < cards[j].SpawnSize; i++)
                {
                    var spawned = Instantiate(cards[j], deck.GetTransform);
                    spawned.transform.position = deck.GetTransform.position;
                    spawned.InitializeCard(sceneCamera, hand, table, deck);
                    spawned.OnCardThrown += CardThrownHandler;
                    yield return spawned;
                }
            }
        }

        IEnumerator MulliganCoroutine()
        {
            ActionPoint--;
            OnMulliganStated?.Invoke(false);

            foreach (var card in hand.Cards)
                card.ReturnDeck();

            yield return new WaitForSeconds(1);
            DrawRandomCardFromDeck(DRAW_COUNT_PER_TURN);
        }
    }
}