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
        void OnEnable() => GameManager.OnScoreChanged += ScoreChangedHandler;
        void OnDisable() => GameManager.OnScoreChanged -= ScoreChangedHandler;

        void ScoreChangedHandler(int actionPoint, float interestPoint)
        {
            bar.fillAmount = interestPoint;
            _transfom.DOShakePosition(duration: 1.5f, strength: 3, vibrato: 20, fadeOut: false);
            //Buzzy sound maybe
        }
    }
}