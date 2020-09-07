using System.Collections;
using UnityEngine;
using DG.Tweening;
using TMPro;

namespace Methodyca.Minigames.Questioniser
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] RectTransform gameOverBackground;
        [SerializeField] RectTransform messagePanel;
        [SerializeField] RectTransform mulliganPanel;
        [SerializeField] RectTransform actionPoint;
        [SerializeField] RectTransform interestPoint;
        [SerializeField] RectTransform endTurnButton;
        [SerializeField] TextMeshPro deckCountText;
        [SerializeField] TextMeshProUGUI messageText;

        [Header("Point Attributes")]
        [SerializeField] TextMeshProUGUI actionPointText;
        [SerializeField] TextMeshProUGUI interestPointText;
        [SerializeField] TextMeshProUGUI pointTextPrefab;
        [SerializeField] RectTransform actionPointEffectPlace;
        [SerializeField] RectTransform interestPointEffectPlace;

        RectTransform _pointSpawnLocation;
        RectTransform _mainPointObject;
        readonly Vector2 _messagePanelPaddingSize = new Vector2(15, 0);
        readonly Color _positivePointTextColor = new Color(.35f, 0.9f, 0.3f, 1);
        readonly Color _negativePointTextColor = new Color(0.9f, 0.35f, 0.35f, 1);

        void Start()
        {
            GameManager.Instance.OnDeckUpdated += DeckUpdatedHandler;
            GameManager.Instance.OnMessageRaised += MessageRaisedHandler;
            GameManager.Instance.OnActionPointUpdated += ActionPointUpdatedHandler;
            GameManager.Instance.OnInterestPointUpdated += InterestPointUpdatedHandler;
            GameManager.Instance.OnMulliganStated += MulliganStatedHandler;
            GameManager.Instance.OnGameOver += GameOverHandler;
        }

        void DeckUpdatedHandler(byte cardCount) => deckCountText.text = cardCount.ToString();
        void MulliganStatedHandler(bool isOn) => mulliganPanel.gameObject.SetActive(isOn);

        void GameOverHandler()
        {
            messagePanel.gameObject.SetActive(false);
            actionPoint.gameObject.SetActive(false);
            endTurnButton.gameObject.SetActive(false);
            mulliganPanel.gameObject.SetActive(false);
            DOTween.Sequence().SetDelay(1).Append(interestPoint.DOAnchorPos(new Vector2(0, 300), 1)).Join(interestPoint.DOScale(2, 1)); // Change AcnhorPos later
        }

        void MessageRaisedHandler(string message)
        {
            StopAllCoroutines();
            StartCoroutine(MessageCoroutine(message));
        }

        IEnumerator MessageCoroutine(string message)
        {
            messagePanel.gameObject.SetActive(true);
            messageText.text = message;
            messageText.ForceMeshUpdate();

            Vector2 textSize = messageText.GetRenderedValues(false);
            messagePanel.sizeDelta = textSize + _messagePanelPaddingSize;

            yield return new WaitForSeconds(3f);
            messagePanel.gameObject.SetActive(false);
        }

        void InterestPointUpdatedHandler(int currentValue, int lastValue)
        {
            _pointSpawnLocation = interestPointEffectPlace;
            _mainPointObject = interestPoint;
            SpawnPointText(currentValue, lastValue, interestPointText);
        }

        void ActionPointUpdatedHandler(int currentValue, int lastValue)
        {
            _pointSpawnLocation = actionPointEffectPlace;
            _mainPointObject = actionPoint;
            SpawnPointText(currentValue, lastValue, actionPointText);
        }

        void SpawnPointText(int currentValue, int lastValue, TextMeshProUGUI updatedText)
        {
            var difference = currentValue - lastValue;
            if (difference == 0)
                return;

            var point = Instantiate(pointTextPrefab, _pointSpawnLocation.position, Quaternion.identity, _pointSpawnLocation);

            if (difference > 0)
            {
                point.color = _positivePointTextColor;
                point.text = "+" + difference.ToString();
            }
            else
            {
                point.color = _negativePointTextColor;
                point.text = difference.ToString();
            }

            DOTween.Sequence().SetDelay(0.75f)
                    .Append(point.rectTransform.DOMove(_mainPointObject.position, 0.25f).SetEase(Ease.InSine))
                    .Append(point.rectTransform.DOScale(0, 0.2f))
                    .Join(_mainPointObject.DOShakeScale(duration: 0.2f, strength: 0.5f, vibrato: 20))
                    .OnComplete(() =>
                    {
                        updatedText.text = currentValue.ToString();
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
                GameManager.Instance.OnGameOver -= GameOverHandler;
            }
        }
    }
}