using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Methodyca.Minigames.Questioniser
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] RectTransform messagePanel;
        [SerializeField] RectTransform mulliganPanel;
        [SerializeField] RectTransform actionPoint;
        [SerializeField] RectTransform interestPoint;
        [SerializeField] TextMeshPro deckCountText;
        [SerializeField] TextMeshProUGUI messageText;
        [SerializeField] TextMeshProUGUI actionPointText;
        [SerializeField] TextMeshProUGUI interestPointText;

        void Start()
        {
            GameManager.Instance.OnDeckUpdated += DeckUpdatedHandler;
            GameManager.Instance.OnMessageRaised += MessageRaisedHandler;
            GameManager.Instance.OnActionPointUpdated += ActionPointUpdatedHandler;
            GameManager.Instance.OnInterestPointUpdated += InterestPointUpdatedHandler;
            GameManager.Instance.OnMulliganStated += MulliganStatedHandler;
        }

        void DeckUpdatedHandler(byte cardCount) => deckCountText.text = cardCount.ToString();
        void MessageRaisedHandler(string message) => StartCoroutine(MessageCoroutine(message));

        IEnumerator MessageCoroutine(string message)
        {
            messagePanel.gameObject.SetActive(true);
            messageText.text = message;
            yield return new WaitForSeconds(3f);
            messagePanel.gameObject.SetActive(false);
        }

        void MulliganStatedHandler(bool isOn)
        {
            mulliganPanel.gameObject.SetActive(isOn);
        }

        void InterestPointUpdatedHandler(int point)
        {
            interestPoint.DOShakeScale(duration: 0.2f);
            interestPointText.text = point.ToString();
        }

        void ActionPointUpdatedHandler(int point)
        {
            actionPoint.DOShakeScale(duration: 0.2f);
            actionPointText.text = point.ToString();
        }

        void OnDisable()
        {
            if (GameManager.InstanceExists)
            {
                GameManager.Instance.OnDeckUpdated -= DeckUpdatedHandler;
                GameManager.Instance.OnMessageRaised -= MessageRaisedHandler;
                GameManager.Instance.OnActionPointUpdated -= ActionPointUpdatedHandler;
                GameManager.Instance.OnInterestPointUpdated -= InterestPointUpdatedHandler;
                GameManager.Instance.OnMulliganStated -= MulliganStatedHandler;
            }
        }
    }
}