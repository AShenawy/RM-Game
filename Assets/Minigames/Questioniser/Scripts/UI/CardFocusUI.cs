using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Methodyca.Minigames.Questioniser
{
    public class CardFocusUI : MonoBehaviour
    {
        [SerializeField] Transform focusPanel;
        [SerializeField] TextMeshProUGUI detailText;

        void OnEnable()
        {
            //GameManager.Instance.OnCardFocus += CardFocusHandler;
            focusPanel.localScale = Vector3.zero;
            Sequence sequence = DOTween.Sequence();
            sequence.Append(focusPanel.DOScale(Vector3.one, 0.2f))
                .Append(focusPanel.DOShakeRotation(duration: 0.25f, strength: 20, vibrato: 5, fadeOut: false));
        }

        void CardFocusHandler(CardBase card)
        {
            detailText.text = card.GetData.Description;
        }

        void OnDisable()
        {
            //GameManager.Instance.OnCardFocus -= CardFocusHandler;
        }
    }
}