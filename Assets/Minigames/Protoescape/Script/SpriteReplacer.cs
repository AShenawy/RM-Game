using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Methodyca.Minigames.Protoescape
{
    public class SpriteReplacer : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Sprite sprite;

        private Image _image;
        private GameObject _replaceable;

        private void Awake()
        {
            _image = GetComponent<Image>();
            _image.sprite = sprite;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _replaceable = GameManager_Protoescape.SelectedEntity;

            if (_replaceable.GetComponent<IReplaceable<Sprite>>() != null)
            {
                _replaceable.GetComponent<IReplaceable<Sprite>>().Replace(sprite);
            }
        }
    }
}