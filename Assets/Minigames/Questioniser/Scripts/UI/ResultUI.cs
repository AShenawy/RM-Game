using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Methodyca.Minigames.Questioniser
{
    public class ResultUI : MonoBehaviour
    {
        [SerializeField] GameObject root;
        [SerializeField] Image gameOverPanel;
        [SerializeField] Sprite winTheGame;
        [SerializeField] Sprite outOfCard;
        [SerializeField] Sprite outOfInterestPoint;
        [SerializeField] TextMeshProUGUI questionsAskedCorrectly;
        [SerializeField] TextMeshProUGUI mistakesMade;
        [SerializeField] TextMeshProUGUI interestPointGained;
        [SerializeField] TextMeshProUGUI storyEventCompleted;

        // main game connection
        [SerializeField] GameObject winAndQuitButton;

        public Sound GameWin;
        public Sound GameOver;

        void Start()
        {
            GameManager.Instance.OnGameOver += GameOverHandler;
        }

        void GameOverHandler()
        {
            root.SetActive(true);

            if (GameManager.Instance.StoryEventsCompleted >= 4)
            {
                gameOverPanel.sprite = winTheGame;
                SoundManager.instance.PlayBGM(GameWin);
                winAndQuitButton.SetActive(true);
            }
            else if (GameManager.Instance.InterestPoint < 0)
            {
                gameOverPanel.sprite = outOfInterestPoint;
                SoundManager.instance.PlayBGM(GameOver);
            }
            else
            {
                gameOverPanel.sprite = outOfCard;
                SoundManager.instance.PlayBGM(GameOver);
            }

            questionsAskedCorrectly.text = GameManager.Instance.QuestionsAskedCorrectly.ToString();
            mistakesMade.text = GameManager.Instance.QuestionsAskedIncorrectly.ToString();
            interestPointGained.text = GameManager.Instance.GainedInterestPoints.ToString();
            storyEventCompleted.text = GameManager.Instance.StoryEventsCompleted.ToString() + "/4";
            
        }

        void OnDestroy()
        {
            if (GameManager.InstanceExists)
                GameManager.Instance.OnGameOver -= GameOverHandler;
        }
    }
}