using TMPro;
using UnityEngine;

namespace Methodyca.Minigames.Questioniser
{
    public class ActionCardUI : MonoBehaviour
    {
        [SerializeField] CardBase cardBase;
        [SerializeField] TextMeshPro interestPointText;

        void OnEnable()
        {
            cardBase.OnCostChanged += CostChangedHandler;
            interestPointText.text = cardBase.CostPoint.ToString();
        }

        void CostChangedHandler(int value)
        {
            interestPointText.text = value.ToString();
        }

        void OnDisable()
        {
            cardBase.OnCostChanged -= CostChangedHandler;
        }
    }
}