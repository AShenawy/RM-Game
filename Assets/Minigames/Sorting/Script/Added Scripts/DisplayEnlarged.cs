using UnityEngine;
using UnityEngine.EventSystems;

namespace Methodyca.Minigames.SortGame
{
    // this script handles instantiating the front view larger image prefab
    public class DisplayEnlarged : MonoBehaviour, IPointerClickHandler
    {
        [Tooltip("The zoomed-in enlarged version of the object")]
        public GameObject enlargedPrefab;
        private SoundManager soundMan;

        void Start()
        {
            soundMan = FindObjectOfType<SoundManager>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (enlargedPrefab)
                Instantiate(enlargedPrefab);

            if (soundMan)
                soundMan.Play("click");
        }
    }
}