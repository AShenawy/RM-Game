using UnityEngine;
using UnityEngine.EventSystems;
using Methodyca.Core;

namespace Methodyca.Minigames.SortGame
{
    // this script handles instantiating the front view larger image prefab
    public class DisplayEnlarged : MonoBehaviour, IPointerClickHandler
    {
        [Tooltip("The zoomed-in enlarged version of the object")]
        public GameObject enlargedPrefab;
        public Sound clickSFX;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (enlargedPrefab)
                Instantiate(enlargedPrefab);

            SoundManager.instance.PlaySFX(clickSFX);
        }
    }
}