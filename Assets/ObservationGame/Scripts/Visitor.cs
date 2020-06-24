using DG.Tweening;
using UnityEngine;

public enum VisitorType { CoffeeDrinker, DogWalker, KidPlayer }

public class Visitor : MonoBehaviour
{
    public VisitorType Type;

    SpriteRenderer _renderer;
    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _renderer.color = new Color(1,1,1,0);
    }
    private void OnEnable()
    {
        _renderer.DOFade(1, 1);
    }
}