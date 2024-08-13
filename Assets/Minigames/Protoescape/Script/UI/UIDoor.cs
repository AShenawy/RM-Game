using Methodyca.Core;
using Methodyca.Minigames.SortGame;
using UnityEngine;
using UnityEngine.UI;

namespace Methodyca.Minigames.Protoescape
{
    public class UIDoor: MonoBehaviour
    {
        [SerializeField] private WinMinigame winMinigame;
        [SerializeField] private ReturnToMainGame returnToMainGame;
        [SerializeField] private MentorController mentorController;
        [SerializeField] private string mentorFeedback;

        private Button _button;
        private bool _gameIsCompleted;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            PrototypeTester.OnPrototypeTestCompleted += PrototypeTestCompletedHandler;
            _button.onClick.AddListener(ClickEventHandler);
        }

        private void ClickEventHandler()
        {
            if (_gameIsCompleted)
            {
                winMinigame.CompleteSingleLoadedMinigame();
                returnToMainGame.QuitMinigame();
            }
            else
            {
                mentorController.NextLine(mentorFeedback);
            }
        }

        private void PrototypeTestCompletedHandler(bool isCompleted, string feedback)
        {
            Debug.Log(gameObject.name);
            _gameIsCompleted = isCompleted;
        }

        private void OnDestroy()
        {
            PrototypeTester.OnPrototypeTestCompleted -= PrototypeTestCompletedHandler;
        }
    }
}