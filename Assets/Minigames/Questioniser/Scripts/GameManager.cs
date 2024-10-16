﻿using System;
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
        public Dialog StoryDialog;


        public Topic(string name, bool isStoryInitiated, Sprite cardSprite)
        {
            Name = name;
            IsStoryInitiated = isStoryInitiated;
            CardSprite = cardSprite;
        }
    }

    [Serializable]
    public class Question
    {
        public bool IsAnswerCorrect;
        public string TopicName;
        public Sprite Header;
        public Option[] Options;

        public void ResetOption() => IsAnswerCorrect = false;
    }

    [Serializable]
    public class Option
    {
        public bool IsCorrect;
        public int Id;
        public int Point;
        public Sprite Feedback;
        [TextArea(4, 10)] public string Text;
    }

    public enum GameState
    {
        None = 0,
        Busy = 1,
        Playable = 2,
        Selectable = 3
    }

    public class GameManager : Singleton<GameManager>
    {
        const byte DRAW_COUNT_PER_TURN = 5;
        const byte ACTION_POINT_PER_TURN = 5;
        const byte POINT_TO_INITIATE_STORY = 4;
        const float GAME_START_DELAY_TIME = 0.5f;

        [Header ("SFX")]
        public Sound ActionsPointsSFX;
        public Sound NotEnoughPointsSFX;
        public Sound StoryPointsSFX;
        public Sound RechargePointsSFX;
        public Sound BGMLevel;

        [SerializeField] Camera sceneCamera;
        [SerializeField] CardHolder hand;
        [SerializeField] CardHolder table;
        [SerializeField] CardHolder deck;
        [SerializeField] List<CardBase> cards;
        [SerializeField] List<Topic> topics;

        [HideInInspector] public GameState GameState;
        [HideInInspector] public byte QuestionsAskedCorrectly = 0;
        [HideInInspector] public byte QuestionsAskedIncorrectly = 0;
        [HideInInspector] public int GainedInterestPoints = 0;
        [HideInInspector] public int StoryEventsCompleted = 0;

        public event Action OnGameOver = delegate { };
        public event Action<bool> OnStoryInitiated = delegate { };
        public event Action<bool> OnRedrawStated = delegate { };
        public event Action<bool> OnImproviserRaised = delegate { };
        public event Action<bool> OnCardInfoCalled = delegate { };
        public event Action<byte> OnDeckUpdated = delegate { };
        public event Action<string> OnMessageRaised = delegate { };
        public event Action<int, int> OnActionPointUpdated = delegate { };
        public event Action<int, int> OnInterestPointUpdated = delegate { };
        public event Action<string, string> OnChecklistUpdated = delegate { };
        public event Action<Topic> OnTopicClosed = delegate { };
        public event Action<Topic> OnTopicChanged = delegate { };
        public event Action<Question> OnQuestionAsked = delegate { };

        int _initialTopicCount;
        int _storyPoint = 5;
        int _actionPoint = 5;
        int _interestPoint = 2;
        int _lastActionPointValue;
        int _lastInterestPointValue;
        int _extraPointsForNextTurn = 0;
        bool _isImproviserTurn;
        bool[,] _quizAnswerSheet;
        byte _questionsAskedCorrectlyPerTurn = 0;
        CardBase _currentCard;
        Topic _currentTopic = new Topic("", false, null);
        HashSet<CardBase> _allAvailableCards;

        public int ActionPoint
        {
            get => _actionPoint;
            set
            {
                _lastActionPointValue = _actionPoint;
                _actionPoint = value;
                if (_actionPoint <= 0)
                {
                    _actionPoint = 0;
                    OnActionPointUpdated?.Invoke(_actionPoint, _lastActionPointValue);
                }
                else
                {
                    OnActionPointUpdated?.Invoke(_actionPoint, _lastActionPointValue);
                }
            }
        }
        public int InterestPoint
        {
            get => _interestPoint;
            set
            {
                _lastInterestPointValue = _interestPoint;
                _interestPoint = value;

                int difference = _interestPoint - _lastInterestPointValue;

                if (difference > 0)
                {
                    GainedInterestPoints += difference;
                }

                if (_interestPoint < 0)
                {
                    OnInterestPointUpdated?.Invoke(_interestPoint, _lastInterestPointValue);
                    GameOver();
                }
                else
                {
                    OnInterestPointUpdated?.Invoke(_interestPoint, _lastInterestPointValue);
                }
            }
        }

        public void SendGameMessage(string message) => OnMessageRaised?.Invoke(message);

        public void TriggerRedraw()
        {
            if (GameState != GameState.Playable)
                return;

            StartCoroutine(RedrawCoroutine());
        }

        public void DrawRandomCardFromDeck(byte size = 1)
        {
            if (deck.Cards.Count > 0)
                Draw(size);
            else if (hand.Cards.Count >= 5)
                SendGameMessage("Cannot hold more than 5 cards");
            else
                GameOver();
        }

        public void EndTurn()
        {
            if (GameState != GameState.Playable)
                return;

            DiscardAllCards();
            OnRedrawStated?.Invoke(true);
            _questionsAskedCorrectlyPerTurn = 0;

            if (deck.Cards.Count < DRAW_COUNT_PER_TURN)
                DrawRandomCardFromDeck((byte)deck.Cards.Count);
            else if (deck.Cards.Count <= 0)
                GameOver();
            else
                DrawRandomCardFromDeck(DRAW_COUNT_PER_TURN);

            if (_isImproviserTurn)
                OnImproviserRaised?.Invoke(_isImproviserTurn = false);

            ActionPoint = ACTION_POINT_PER_TURN + _extraPointsForNextTurn;
            SoundManager.instance.PlaySFX(ActionsPointsSFX);
            _extraPointsForNextTurn = 0;
        }
        

        public void InitiateStoryDialog()
        {
            if (InterestPoint >= _storyPoint)
            {
                InterestPoint -= _storyPoint;
                DialogManager.Instance.StartDialog(_currentTopic.StoryDialog);
            }
            else
            {
                SendGameMessage("Not enough interest point");
                SoundManager.instance.PlaySFX(NotEnoughPointsSFX);
            }
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

        public void HandleCompromiser(int interestPoint, int actionPoint)
        {
            GameState = GameState.Playable;
            ActionPoint += actionPoint;
            InterestPoint += interestPoint;
        }

        public void HandleHighFlayer()
        {
            GameState = GameState.Playable;
            _extraPointsForNextTurn += _questionsAskedCorrectlyPerTurn;
            SendGameMessage($"Added extra <b>{_questionsAskedCorrectlyPerTurn}</b> action points for the next turn");
        }

        public void HandleImproviser()
        {
            GameState = GameState.Playable;
            OnImproviserRaised?.Invoke(_isImproviserTurn = true);
            SendGameMessage("Interest points can be used as action points for this turn");
        }

        public void HandleStoryDialog()
        {
            GameState = GameState.Playable;
            ActionPoint += GetCorrectAnswerCountFor(_currentTopic.Name);
            _storyPoint += 3;
            OnTopicClosed?.Invoke(_currentTopic);
            topics.Remove(_currentTopic);
            StoryEventsCompleted = _initialTopicCount - topics.Count;
            SetRandomTopic();
        }

        public void HandleItemCardQuestionFor(Option option)
        {
            GameState = GameState.Playable;
            InterestPoint += option.Point;

            var itemCard = _currentCard as ItemCard;

            if (option.IsCorrect)
            {
                for (int i = 0; i < itemCard.Questions.Length; i++)
                {
                    if (itemCard.Questions[i].TopicName == _currentTopic.Name)
                    {
                        ItemCard c = GetPrefabOf(itemCard) as ItemCard;
                        c.Questions[i].IsAnswerCorrect = true;

                        QuestionsAskedCorrectly++;
                        _questionsAskedCorrectlyPerTurn++;

                        TickAnswerSheet(_currentTopic.Name, _currentCard.Name);
                        OnChecklistUpdated?.Invoke(_currentTopic.Name, _currentCard.Name);

                        if (GetCorrectAnswerCountFor(_currentTopic.Name) >= POINT_TO_INITIATE_STORY)
                        {
                            OnStoryInitiated?.Invoke(_currentTopic.IsStoryInitiated = true);
                            SoundManager.instance.PlaySFX(StoryPointsSFX);
                        }
                    }
                }
            }
            else
            {
                QuestionsAskedIncorrectly++;
            }
        }

        void CardThrownHandler(object sender, CardBase.OnCardThrownEventArgs e)
        {
            hand.ArrangeCards();
            OnRedrawStated?.Invoke(false);

            if (e.Card is ItemCard itemCard)
            {
                _currentCard = itemCard;
                ItemCard c = GetPrefabOf(itemCard) as ItemCard;

                if (_isImproviserTurn)
                    InterestPoint -= e.Card.CostPoint;
                else
                    ActionPoint -= e.Card.CostPoint;

                foreach (var q in c.Questions)
                {
                    if (_currentTopic.Name == q.TopicName)
                    {
                        if (!q.IsAnswerCorrect)
                            OnQuestionAsked?.Invoke(q);
                        else
                        {
                            GameState = GameState.Playable;
                            SendGameMessage("The proper option was already selected before");
                        }
                    }
                }
            }

            if (e.Card is ActionCard actionCard)
            {
                _currentCard = actionCard;
                InterestPoint -= e.Card.CostPoint;
            }
        }

        IEnumerator Start()
        {
            deck.Cards = GetSpawnedCards().ToList();
            _allAvailableCards = new HashSet<CardBase>(GetSpawnedCards());
            _quizAnswerSheet = new bool[topics.Count, cards.Count];
            _initialTopicCount = topics.Count;

            yield return new WaitForSeconds(GAME_START_DELAY_TIME);
            SetRandomTopic();
            DrawRandomCardFromDeck(DRAW_COUNT_PER_TURN);

            SoundManager.instance.PlayBGM(BGMLevel);
        }

        void GameOver()
        {
            GameState = GameState.None;
            OnGameOver?.Invoke();

        }

        void TickAnswerSheet(string topicName, string cardName)
        {
            for (int i = 0; i < topics.Count; i++)
                if (topicName == topics[i].Name)
                    for (int j = 0; j < cards.Count; j++)
                        if (cardName == cards[j].Name)
                            _quizAnswerSheet[i, j] = true;

            SoundManager.instance.PlaySFX(RechargePointsSFX);
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

            hand.RemoveAllCards();
            table.RemoveAllCards();
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

        IEnumerator RedrawCoroutine()
        {
            ActionPoint--;
            OnRedrawStated?.Invoke(false);

            foreach (var card in hand.Cards)
                card.ReturnDeck();

            yield return new WaitForSeconds(1);
            DrawRandomCardFromDeck(DRAW_COUNT_PER_TURN);
        }

        void ResetGame()
        {
            for (int i = 0; i < cards.Count; i++)
                if (cards[i] is ItemCard card)
                    for (int j = 0; j < card.Questions.Length; j++)
                        card.Questions[j].ResetOption();
        }

        void UnsubscribeCardEvents()
        {
            foreach (var c in _allAvailableCards)
                if (c != null)
                    c.OnCardThrown -= CardThrownHandler;
        }

        void OnDisable()
        {
            UnsubscribeCardEvents();
            ResetGame();
        }
    }
}