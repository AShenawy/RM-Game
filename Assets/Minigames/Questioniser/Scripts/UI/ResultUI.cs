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

        void Start()
        {
            GameManager.Instance.OnGameOver += GameOverHandler;
        }

        void GameOverHandler()
        {
            root.SetActive(true);
            GameManager.Instance.GameState = GameState.None;

            if (GameManager.Instance.StoryEventsCompleted >= 4)
            {
                gameOverPanel.sprite = winTheGame;
                FindObjectOfType<SoundManager>().Stop("Theme");
                FindObjectOfType<SoundManager>().Stop("GameTheme");
                FindObjectOfType<SoundManager>().Play("GameWin");
            }
            else if (GameManager.Instance.InterestPoint < 0)
            {
                gameOverPanel.sprite = outOfInterestPoint;
                FindObjectOfType<SoundManager>().Stop("Theme");
                FindObjectOfType<SoundManager>().Stop("GameTheme");
                FindObjectOfType<SoundManager>().Play("GameOverLose");
            }
            else
            {
                gameOverPanel.sprite = outOfCard;
                FindObjectOfType<SoundManager>().Stop("Theme");
                FindObjectOfType<SoundManager>().Stop("GameTheme");
                FindObjectOfType<SoundManager>().Play("GameOverLose");
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