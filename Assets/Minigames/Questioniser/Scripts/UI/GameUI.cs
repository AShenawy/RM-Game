using TMPro;
using UnityEngine;

namespace Methodyca.Minigames.Questioniser
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI actionPointText;

        void OnEnable() => GameManager.OnScoreChanged += ScoreChangedHandler;
        void OnDisable() => GameManager.OnScoreChanged -= ScoreChangedHandler;

        void ScoreChangedHandler(int actionPoint, float interestPoint)
        {
            actionPointText.text = actionPoint.ToString();
        }
    }
}