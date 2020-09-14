using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Methodyca.Minigames.Questioniser
{
    public class UIReviewCards : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI counter;
        [SerializeField] List<GameObject> reviewCards;

        byte _count = 1;
        LinkedList<GameObject> _cards;
        LinkedListNode<GameObject> _node;

        void Start()
        {
            _cards = new LinkedList<GameObject>(reviewCards);
            _node = _cards.First;
        }

        public void Right()
        {
            if (_node.Next == null)
            {
                _count = 0;
                _node.Value.SetActive(false);
                _node = _cards.First;
                _node.Value.SetActive(true);
            }
            else
            {
                _node.Value.SetActive(false);
                _node.Next.Value.SetActive(true);
                _node = _node.Next;
            }
            ++_count;
            counter.text = _count.ToString();
        }

        public void Left()
        {
            if (_node.Previous == null)
            {
                _count = 7;
                _node.Value.SetActive(false);
                _node = _cards.Last;
                _node.Value.SetActive(true);
            }
            else
            {
                _node.Value.SetActive(false);
                _node.Previous.Value.SetActive(true);
                _node = _node.Previous;
            }
            --_count;
            counter.text = _count.ToString();
        }
    }
}