using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Methodyca.Minigames.Protoescape
{
    public class UIEntityHighlighter : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Sprite activeOn;
        [SerializeField] private Sprite activeOff;
        [SerializeField] private Sprite passive;

        private IHighlighted _selection;
        private Image _image;

        private void Start()
        {
            _image = GetComponent<Image>();
            GameManager_Protoescape.OnSelected += SelectedHandler;
        }

        private void SelectedHandler(GameObject selection)
        {
            if (selection.GetComponent<IHighlighted>() != null)
            {
                _selection = selection.GetComponent<IHighlighted>();

                if (_selection.IsHighlighted)
                {
                    _image.sprite = activeOn;
                }
                else
                {
                    _image.sprite = activeOff;
                }
            }
            else
            {
                _selection = null;
                _image.sprite = passive;
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_selection == null)
            {
                return;
            }

            _selection.SetHighlight();
        }

        private void OnDestroy()
        {
            GameManager_Protoescape.OnSelected -= SelectedHandler;
        }
    }
}