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
        void Start() => GameManager.Instance.OnInterestPointUpdated += InterestPointChangedHandler;

        void OnDisable() {

            if (GameManager.InstanceExists)
                GameManager.Instance.OnInterestPointUpdated -= InterestPointChangedHandler;
        } 

        void InterestPointChangedHandler(int point)
        {
            bar.fillAmount = point;
            _transfom.DOShakePosition(duration: 1, strength: 3, vibrato: 20, fadeOut: false);
        }
    }
}