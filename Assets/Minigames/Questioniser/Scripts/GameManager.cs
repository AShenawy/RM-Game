using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Methodyca.Minigames.Questioniser
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] int maxInterestPoint = 10;
        [SerializeField] Camera sceneCamera;
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
            _cardsInDeck = GetSpawnedCards().ToList();
            DrawRandomCardFromDeck(4);
        }

        void OnEnable()
        {
            //QuizManager.Instance.OnAnswerSelected += AnswerSelectedHandler;
        }

        void AnswerSelectedHandler(Answer answer)
        {
            if (InterestPoint < maxInterestPoint)
                InterestPoint += answer.Score;

            if (answer.IsCorrect)
                DrawRandomCardFromDeck();
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
                yield return new WaitForSeconds(1);
            }
        }

        IEnumerable<CardBase> GetSpawnedCards()
        {
            for (int j = 0; j < cardData.Count; j++)
            {
                for (int i = 0; i < cardData[j].SpawnSize; i++)
                {
                    var spawned = Instantiate(cardPrefab);
                    spawned.InitializeCard(sceneCamera, cardData[j], hand, table);
                    spawned.transform.position = deck.position;
                    yield return spawned;
                }
            }
        }

        void OnDisable()
        {
            //QuizManager.Instance.OnAnswerSelected -= AnswerSelectedHandler;
        }
    }
}