﻿using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Methodyca.Minigames.Questioniser
{
    public class UIMenuButtonTween : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
    {
        private RectTransform _transform;
        private Button _button;

        [Header("SFX")]
        public Sound ClickSFX;
        public Sound HoverSFX;

        private void Awake()
        {
            _transform = GetComponent<RectTransform>();
            _button = GetComponent<Button>();
        }

        public void OnPointerDown(PointerEventData eventData)//ClickSound
        {
            _transform.DOScale(0.9f, 0.15f);
            SoundManager.instance.PlaySFX(ClickSFX);
            //Debug.Log("Click ");
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _transform.DOScale(1, 0.15f);
         
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _transform.DOScale(1.1f, 0.15f);
            SoundManager.instance.PlaySFX(HoverSFX,true);
            //Debug.Log("Hello there");
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _transform.DOScale(1, 0.15f);
        }
    }
}