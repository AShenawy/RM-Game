using UnityEngine;
using UnityEngine.UI;

namespace Methodyca.Minigames.Protoescape
{

    [RequireComponent(typeof(Image))]
    public class Icon : BaseEntity, IReplaceable<Sprite>, IReplaceable<Color>
    {
        private Image _image;

        protected override void Awake()
        {
            base.Awake();
            _image = GetComponent<Image>();
        }

        public void Replace(Color value)
        {
            _image.color = value;
        }

        public void Replace(Sprite value)
        {
            _image.sprite = value;
        }
    }
}