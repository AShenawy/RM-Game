using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Methodyca.Minigames.Questioniser
{
    public class CardHolder : MonoBehaviour
    {
        [SerializeField] Transform holder;

        [HideInInspector] public Transform GetTransform;
        public List<CardBase> Cards = new List<CardBase>();

        void Awake()
        {
            GetTransform = holder;
        }

        void Start()
        {
            GameManager.Instance.OnGameOver += GameOverHandler;
        }

        void GameOverHandler()
        {
            gameObject.SetActive(false);
        }

        public int GetCardCount()
        {
            return Cards.Count;
        }

        public void AddCard(CardBase card)
        {
            if (!Cards.Contains(card))
            //Debug.Log("Adding Cards");
            FindObjectOfType<SoundManager>().Play("CardShuffledSFX");
                Cards.Add(card);
        }

        public void RemoveCard(CardBase card)
        {
            if (Cards.Contains(card))
                Cards.Remove(card);
        }

        public void RemoveAllCards()
        {
            Cards.Clear();
        }

        public void ArrangeCards()
        {
            if (Cards.Count > 0)
            {
                Cards.RemoveAll(c => c == null);
                for (int i = 0; i < Cards.Count; i++)
                    Cards[i].transform.DOMoveX(holder.position.x + ((Cards.Count * Cards[i].SpriteSizeX * 0.5f) - (i * Cards[i].SpriteSizeX)) - 1, 0.5f);
                //Debug.Log("Card Set");
            }
        }

        void OnDestroy()
        {
            if (GameManager.InstanceExists)
            {
                GameManager.Instance.OnGameOver -= GameOverHandler;
            }
        }
    }
}