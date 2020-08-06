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
        [SerializeField] Camera sceneCamera;
        [SerializeField] CardInfoUI cardInfoGUI;
        [SerializeField] CardHolder hand;
        [SerializeField] CardHolder table;
        [SerializeField] Transform deck;
        [SerializeField] List<CardBase> cards;
        [SerializeField] List<string> topics;

        public event Action OnGameOver = delegate { };
        public event Action<string> OnTopicChanged = delegate { };
        public event Action<Question> OnQuestionAsked = delegate { };
        public event Action<int> OnActionPointUpdated = delegate { };
        public event Action<float> OnInterestPointUpdated = delegate { };
        public event Action<string, string> OnStoryPointRaised = delegate { };
        public event Action<string, string, bool> OnChartUpdated = delegate { };
        public event Action<string> OnMessageRaised = delegate { };
        public event Action<byte> OnDeckUpdated = delegate { };

        string _currentTopicName;
        CardBase _currentCard;
        List<CardBase> _cardsInDeck;
        bool[,] _quizAnswerSheet;

        public CardInfoUI CardInfoGUI => cardInfoGUI;
        public int ActionPoint { get; private set; } = 5;
        public int InterestPoint { get; private set; } = 2;
        public void RaiseGameMessage(string message) => OnMessageRaised?.Invoke(message);

        public void EndTurn()
        {
            DiscardCards();
            DrawRandomCardFromDeck(5); //Add validation check
            OnActionPointUpdated?.Invoke(ActionPoint += 5);
        }

        public void SwitchInterestToActionPoint(int interestPoint, int actionPoint)
        {
            OnActionPointUpdated?.Invoke(ActionPoint += actionPoint);
            OnInterestPointUpdated?.Invoke(InterestPoint += interestPoint);
        }

        public void SetRandomTopic()
        {
            if (topics.Count <= 0)
                return;

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
                    if (itemCard.Questions[i].TopicName == _currentTopicName)
                    {
                        itemCard.Questions[i].IsAnswerCorrect = true;
                        TickAnswerSheet(_currentTopicName, _currentCard.Name);

                        if (GetCorrectAnswerCountFor(_currentTopicName) >= 2)
                            OnStoryPointRaised?.Invoke(_currentTopicName, _currentCard.Name);
                    }
                }
            }
        }

        void CardThrownHandler(object sender, CardBase.OnCardThrownEventArgs e)
        {
            if (sender is ItemCard itemCard)
            {
                _currentCard = itemCard;
                OnActionPointUpdated?.Invoke(ActionPoint -= e.Card.ActionPoint);

                foreach (var q in itemCard.Questions)
                {
                    if (_currentTopicName == q.TopicName && !q.IsAnswerCorrect)
                        OnQuestionAsked?.Invoke(q);
                    else if (q.IsAnswerCorrect)
                        OnMessageRaised?.Invoke("The proper option was already selected before");
                }
            }

            if (sender is ActionCard actionCard)
            {
                // Add validation check
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
                if (topicName == topics[i])
                    for (int j = 0; j < cards.Count; j++)
                        if (cardName == cards[j].Name)
                            _quizAnswerSheet[i, j] = true;
        }

        byte GetCorrectAnswerCountFor(string topicName)
        {
            byte num = 0;
            for (int i = 0; i < topics.Count; i++)
                if (topicName == topics[i])
                    for (int j = 0; j < cards.Count; j++)
                        if (_quizAnswerSheet[i, j])
                            num++;
            return num;
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