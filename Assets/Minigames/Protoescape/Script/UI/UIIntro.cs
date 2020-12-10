using UnityEngine;
using DG.Tweening;

namespace Methodyca.Minigames.Protoescape
{
    public class UIIntro : MonoBehaviour
    {
        [SerializeField] private Transform alienEye;
        [SerializeField] private int strength = 50;
        [SerializeField] private int vibrato = 1;

        public void DisplayAlienEye()
        {
            alienEye.gameObject.SetActive(true);
            alienEye.DOShakePosition(3, strength * Vector2.one, vibrato);
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}