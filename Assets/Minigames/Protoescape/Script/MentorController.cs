using System;
using System.Collections.Generic;
using UnityEngine;

namespace Methodyca.Minigames.Protoescape
{
    public class MentorController : MonoBehaviour
    {
        [SerializeField] private string[] introductionLines;
        [SerializeField] private string[] prototypeTips;

        public static event Action<string> OnMentorTalked = delegate { };

        private bool _gameIsFinished;
        private LinkedList<string> _currentLines = new LinkedList<string>();
        private LinkedListNode<string> _currentLine;

        public void NextLine()
        {
            if (_currentLines.Count > 0)
            {
                OnMentorTalked?.Invoke(_currentLine.Value);
                _currentLine = _currentLine.NextOrFirst();
            }
        }

        public void NextLine(string text)
        {
            OnMentorTalked?.Invoke(text);
        }

        private void OnEnable()
        {
            GameManager_Protoescape.OnGameStarted += GameStartedHandler;
            GameManager_Protoescape.OnPrototypeInitiated += PrototypeInitiatedHandler;
            PrototypeTester.OnPrototypeTestCompleted += PrototypeTestCompletedHandler;
        }

        private void PrototypeTestCompletedHandler(bool isCompleted, string feedback)
        {
            _gameIsFinished = isCompleted;

            if (isCompleted)
            {
                _currentLines.Clear();
                NextLine("Congratulations!");
            }
        }

        private void PrototypeInitiatedHandler()
        {
            if (_gameIsFinished)
            {
                return;
            }

            _currentLines = new LinkedList<string>(prototypeTips);
            _currentLine = _currentLines.First;
            NextLine();
        }

        private void GameStartedHandler()
        {
            _currentLines = new LinkedList<string>(introductionLines);
            _currentLine = _currentLines.First;
            NextLine();
        }

        private void OnDisable()
        {
            GameManager_Protoescape.OnGameStarted -= GameStartedHandler;
            GameManager_Protoescape.OnPrototypeInitiated -= PrototypeInitiatedHandler;
            PrototypeTester.OnPrototypeTestCompleted -= PrototypeTestCompletedHandler;
        }
    }
}