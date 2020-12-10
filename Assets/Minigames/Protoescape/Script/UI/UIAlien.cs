using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Methodyca.Minigames.Protoescape
{
    public class UIAlien : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private Mover _mover;
        private Image _image;

        public void OnPointerEnter(PointerEventData eventData)
        {
            _mover.PlayPauseMoveTween();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _mover.PlayPauseMoveTween();
        }

        private void Awake()
        {
            _mover = GetComponent<Mover>();
            _image = GetComponentInChildren<Image>();
            _image.raycastTarget = false;
        }

        private void OnEnable()
        {
            GameManager_Protoescape.OnPrototypeInitiated += () => _image.raycastTarget = true;
        }
    }
}