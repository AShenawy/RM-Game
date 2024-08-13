using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace Methodyca.Core
{
    // script to be placed on badges in the badges app
    public class BadgeBehaviour : MonoBehaviour, IPointerEnterHandler
    {
        public Minigames minigameID;
        public string minigameName;
        public event System.Action<BadgeBehaviour> cursorOverBadge;
        public string badgeTitle;
        [TextArea] public string badgeDescription;
        public bool isBadgeWon;
        [SerializeField] Image image;

        private void Awake()
        {
            //image.alphaHitTestMinimumThreshold = 0.5f;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            cursorOverBadge?.Invoke(this);
        }

        public void SetBadgeActive(bool value)
        {
            if (value)
                image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);
            else
                image.color = new Color(image.color.r, image.color.g, image.color.b, 0.5f);
        }
    }
}