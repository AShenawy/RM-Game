using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Methodyca.Minigames.Protoescape
{
    public class UIIntro : MonoBehaviour
    {
        [SerializeField] private Transform alienEye;
        [SerializeField] private int strength = 50;
        [SerializeField] private int vibrato = 1;
        [SerializeField] private Button skipIntro;
        [SerializeField] private GameObject[] skippedGameObjects;

        private void Start()
        {
            skipIntro.onClick.AddListener(SkipIntro);
        }

        public void DisplayAlienEye()
        {
            alienEye.gameObject.SetActive(true);
            alienEye.DOShakePosition(3, strength * Vector2.one, vibrato);
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }

        public void SkipIntro()
        {
            foreach (var obj in skippedGameObjects)
            {
                Destroy(obj);
            }
        }
    }
}