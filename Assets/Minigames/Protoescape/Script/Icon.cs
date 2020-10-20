using UnityEngine;
using UnityEngine.UI;

namespace Methodyca.Minigames.Protoescape
{
    [RequireComponent(typeof(Image))]
    public class Icon : BaseEntity, IReplaceable<Sprite>, IReplaceable<Color>, IHighlighted
    {
        [SerializeField] private GameObject highlight;

        private Image _image;

        public bool IsHighlighted { get; set; } = false;

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

        public void SetHighlight()
        {
            IsHighlighted = !IsHighlighted;
            highlight.SetActive(IsHighlighted);
        }
    }
}