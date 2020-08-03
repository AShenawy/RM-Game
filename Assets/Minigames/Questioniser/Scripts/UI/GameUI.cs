using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Methodyca.Minigames.Questioniser
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] RectTransform actionPoint;
        [SerializeField] TextMeshProUGUI actionPointText;

        void Start()
        {
            GameManager.Instance.OnActionPointChanged += ActionPointChangedHandler;
            GameManager.Instance.OnInterestPointChanged += InterestPointChangedHandler;
        }

        void InterestPointChangedHandler(float point)
        {

        }

        void ActionPointChangedHandler(int point)
        {
            actionPoint.DOShakeScale(duration: 0.5f);
            actionPointText.text = point.ToString();
        }

        void OnDisable()
        {
            if (GameManager.InstanceExists)
            {
                GameManager.Instance.OnActionPointChanged -= ActionPointChangedHandler;
                GameManager.Instance.OnInterestPointChanged -= InterestPointChangedHandler;
            }
        }
    }
}