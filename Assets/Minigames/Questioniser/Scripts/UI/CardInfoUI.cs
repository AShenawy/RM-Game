using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Methodyca.Minigames.Questioniser
{
    public class CardInfoUI : MonoBehaviour
    {
        [SerializeField] GameObject root;
        [SerializeField] RectTransform infoPanel;
        [SerializeField] TextMeshProUGUI detailText;

        void Start()
        {
            CardInfo.OnCardInfoCalled += CardInfoCalledHandler;
        }

        void CardInfoCalledHandler(bool isEnabled, string description)
        {
            if (isEnabled)
            {
                root.SetActive(isEnabled);
                detailText.text = description;
                infoPanel.localScale = Vector3.zero;

                Sequence sequence = DOTween.Sequence();
                sequence.Append(infoPanel.DOScale(Vector3.one, 0.2f))
                    .Append(infoPanel.DOShakeRotation(duration: 0.25f, strength: 20, vibrato: 5, fadeOut: false));
            }
            else
                root.SetActive(false);
        }

        void OnDestroy()
        {
            CardInfo.OnCardInfoCalled -= CardInfoCalledHandler;
        }
    }
}