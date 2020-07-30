using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Methodyca.Minigames.Questioniser
{
    public class InterestBarUI : MonoBehaviour
    {
        [SerializeField] Image bar;

        Transform _transfom;

        void Awake() => _transfom = transform;
        void OnEnable() => GameManager.Instance.OnInterestPointChanged += InterestPointChangedHandler;
        void OnDisable() => GameManager.Instance.OnInterestPointChanged -= InterestPointChangedHandler;

        void InterestPointChangedHandler(float point)
        {
            bar.fillAmount = point;
            _transfom.DOShakePosition(duration: 1, strength: 3, vibrato: 20, fadeOut: false);
        }
    }
}