using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Methodyca.Minigames.Protoescape
{
    [RequireComponent(typeof(Image))]
    public class ColorReplacer : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Color color;

        private GameObject _replaceable;
        private Image _image;

        private void Awake()
        {
            _image = GetComponent<Image>();
            _image.color = color;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _replaceable = GameManager_Protoescape.SelectedEntity;

            if (_replaceable.GetComponent<IReplaceable<Color>>() != null)
            {
              _replaceable.GetComponent<IReplaceable<Color>>().Replace(color);
            }
        }
    }
}