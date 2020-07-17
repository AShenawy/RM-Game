using DG.Tweening;
using Methodyca.Minigames.Questioniser;
using UnityEngine;
using UnityEngine.UI;

public class InterestBarUI : MonoBehaviour
{
    [SerializeField] Slider bar;

    Transform _transfom;

    void Awake()
    {
        _transfom = transform;
        bar.value = 0;
    }

    void OnEnable()
    {
        QuizManager.Instance.OnAnswerSelected += AnswerSelectedHandler;
    }

    void AnswerSelectedHandler(Answer answer)
    {
        bar.value = GameManager.Instance.InterestPoint;
        _transfom.DOShakePosition(1);
        //Buzzy sound maybe
    }

    void OnDisable()
    {
        QuizManager.Instance.OnAnswerSelected -= AnswerSelectedHandler;
    }
}