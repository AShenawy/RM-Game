using UnityEngine;
using DG.Tweening;

namespace Methodyca.Minigames.Protoescape
{
    public class UIAlienEye : MonoBehaviour
    {
        [SerializeField] private int strength = 50;
        [SerializeField] private int vibrato = 1;

        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        private void OnEnable()
        {
            _transform.DOShakePosition(5, strength * Vector2.one, vibrato);
        }
    }
}