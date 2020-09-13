using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Methodyca.Minigames.Questioniser
{
    public class CardInfoUI : MonoBehaviour
    {
        [SerializeField] GameObject root;
        [SerializeField] RectTransform infoPanel;
        [SerializeField] Image card;
        [SerializeField] Image info;

        void OnEnable()
        {
            CardInfo.OnCardInfoCalled += CardInfoCalledHandler;
        }

        void CardInfoCalledHandler(bool isEnabled, CardBase cardBase)
        {
            if (isEnabled)
            {
                root.SetActive(isEnabled);

                card.sprite = cardBase.CardSprite;
                info.sprite = cardBase.InfoSprite;

                infoPanel.localScale = Vector3.zero;
               
                DOTween.Sequence().Append(infoPanel.DOScale(Vector3.one, 0.2f))
                    .Append(infoPanel.DOShakeRotation(duration: 0.25f, strength: 20, vibrato: 5, fadeOut: false));
            }
            else
            {
                root.SetActive(false);
            }
        }

        void OnDisable()
        {
            CardInfo.OnCardInfoCalled -= CardInfoCalledHandler;
        }
    }
}