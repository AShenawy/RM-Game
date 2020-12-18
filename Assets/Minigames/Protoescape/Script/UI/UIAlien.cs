using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Methodyca.Minigames.Protoescape
{
    public class UIAlien : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Image spriteImage;
        [SerializeField] private Image flippedImage;
        private Mover _mover;

        public void OnPointerEnter(PointerEventData eventData)
        {
            _mover.Pause();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _mover.Play();
        }

        private void Awake()
        {
            _mover = GetComponent<Mover>();
            spriteImage.raycastTarget = flippedImage.raycastTarget = false;
        }

        private void OnEnable()
        {
            GameManager_Protoescape.OnPrototypeInitiated += PrototypeInitiatedHandler;
            PrototypeTester.OnPrototypeTestInitiated += PrototypeTestInitiatedHandler;
        }

        private void PrototypeTestInitiatedHandler(string[] obj)
        {
            spriteImage.raycastTarget = flippedImage.raycastTarget = false;
            _mover.Pause();
        }

        private void PrototypeInitiatedHandler()
        {
            spriteImage.raycastTarget = flippedImage.raycastTarget = true;
            _mover.Pause();
        }

        private void OnDisable()
        {
            GameManager_Protoescape.OnPrototypeInitiated -= PrototypeInitiatedHandler;
            PrototypeTester.OnPrototypeTestInitiated -= PrototypeTestInitiatedHandler;
        }
    }
}