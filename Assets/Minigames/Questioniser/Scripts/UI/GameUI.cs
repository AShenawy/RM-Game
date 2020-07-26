using TMPro;
using UnityEngine;

namespace Methodyca.Minigames.Questioniser
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI actionPointText;

        void OnEnable()
        {
            GameManager.Instance.OnScoreChanged += ScoreChangedHandler;
        }

        void ScoreChangedHandler(int actionPoint, float interestPoint)
        {
            actionPointText.text = actionPoint.ToString();
        }

        void OnDisable()
        {
            GameManager.Instance.OnScoreChanged -= ScoreChangedHandler;
        }
    }
}