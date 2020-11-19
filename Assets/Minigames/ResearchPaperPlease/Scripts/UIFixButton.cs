using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Methodyca.Minigames.ResearchPaperPlease
{
    public class UIFixButton : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private char optionIndex;
        [SerializeField] private Sprite normalSprite;
        [SerializeField] private Sprite pressedSprite;

        private Image _image;
        private bool _isPressed;

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        private void Start()
        {
            GameManager.OnLevelInitiated += LevelInitiatedHandler;
            GameManager.OnPaperUpdated += PaperUpdatedHandler;
        }

        private void LevelInitiatedHandler(LevelData data)
        {
            if (_isPressed)
            {
                OnPointerClick(default);
            }

            gameObject.SetActive(false);

            foreach (var item in data.ActiveOptionsToFix)
            {
                if (item == optionIndex)
                {
                    gameObject.SetActive(true);
                }
            }
        }

        private void PaperUpdatedHandler(ResearchPaperData data)
        {
            if (_isPressed)
            {
                OnPointerClick(default);
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _isPressed = !_isPressed;

            if (_isPressed)
            {
                _image.sprite = pressedSprite;
            }
            else
            {
                _image.sprite = normalSprite;
            }

            GameManager.Instance.HandleFixingOption(optionIndex, _isPressed);
        }

        private void OnDestroy()
        {
            GameManager.OnLevelInitiated -= LevelInitiatedHandler;
            GameManager.OnPaperUpdated -= PaperUpdatedHandler;
        }
    }
}