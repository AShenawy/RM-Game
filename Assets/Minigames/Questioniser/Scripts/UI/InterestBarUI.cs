using DG.Tweening;
using Methodyca.Minigames.Questioniser;
using UnityEngine;
using UnityEngine.UI;

public class InterestBarUI : MonoBehaviour
{
    [SerializeField] Image bar;

    Transform _transfom;

    void Awake()
    {
        _transfom = transform;
    }

    void OnEnable()
    {
        GameManager.Instance.OnScoreChanged += ScoreChangedHandler;
    }

    void ScoreChangedHandler(int actionPoint, float interestPoint)
    {
        bar.fillAmount = interestPoint;
        _transfom.DOShakePosition(1);
        //Buzzy sound maybe
    }

    void OnDisable()
    {
        GameManager.Instance.OnScoreChanged -= ScoreChangedHandler;
    }
}