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
            cardBase.OnInterestValueChanged += InterestValueChangedHandler;
            interestPointText.text = cardBase.InterestPoint.ToString();
        }

        void InterestValueChangedHandler(int value)
        {
            interestPointText.text = value.ToString();
        }

        void OnDisable()
        {
            cardBase.OnInterestValueChanged -= InterestValueChangedHandler;
        }
    }
}