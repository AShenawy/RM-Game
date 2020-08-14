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

        [Header("Point Attributes")]
        [SerializeField] TextMeshProUGUI actionPointText;
        [SerializeField] TextMeshProUGUI interestPointText;
        [SerializeField] TextMeshProUGUI pointTextPrefab;
        [SerializeField] RectTransform actionPointEffectPlace;
        [SerializeField] RectTransform interestPointEffectPlace;

        RectTransform _pointUI;
        TextMeshProUGUI _pointText;
        RectTransform _pointSpawnLocation;

        void Start()
        {
            GameManager.Instance.OnDeckUpdated += DeckUpdatedHandler;
            GameManager.Instance.OnMessageRaised += MessageRaisedHandler;
            GameManager.Instance.OnActionPointUpdated += ActionPointUpdatedHandler;
            GameManager.Instance.OnInterestPointUpdated += InterestPointUpdatedHandler;
            GameManager.Instance.OnMulliganStated += MulliganStatedHandler;
        }

        void DeckUpdatedHandler(byte cardCount) => deckCountText.text = cardCount.ToString();

        void MessageRaisedHandler(string message)
        {
            StopAllCoroutines();
            StartCoroutine(MessageCoroutine(message));
        }

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

        void InterestPointUpdatedHandler(int currentValue, int lastValue)
        {
            _pointSpawnLocation = interestPointEffectPlace;
            _pointUI = interestPoint;
            _pointText = interestPointText;
            SpawnPointText(currentValue, lastValue);
        }

        void ActionPointUpdatedHandler(int currentValue, int lastValue)
        {
            _pointSpawnLocation = actionPointEffectPlace;
            _pointUI = actionPoint;
            _pointText = actionPointText;
            SpawnPointText(currentValue, lastValue);
        }

        void SpawnPointText(int currentValue, int lastValue)
        {
            var diff = currentValue - lastValue;

            if (diff == 0)
                return;

            Sequence seq = DOTween.Sequence();
            var point = Instantiate(pointTextPrefab, _pointSpawnLocation.position, Quaternion.identity, _pointSpawnLocation);

            if (diff > 0)
            {
                point.color = new Color(.35f, 0.9f, 0.3f, 1);
                point.text = "+" + diff.ToString();
            }
            else
            {
                point.color = new Color(0.9f, 0.25f, 0.25f, 1);
                point.text = diff.ToString();
            }

            seq.SetDelay(0.75f);
            seq.Append(point.rectTransform.DOMove(_pointUI.position, 0.25f).SetEase(Ease.InSine))
                    .Append(point.rectTransform.DOScale(0, 0.2f))
                    .Join(_pointUI.DOShakeScale(duration: 0.2f, strength: 0.5f, vibrato: 20))
                    .OnComplete(() =>
                    {
                        _pointText.text = currentValue.ToString();
                        Destroy(point);
                    });
        }

        void OnDestroy()
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