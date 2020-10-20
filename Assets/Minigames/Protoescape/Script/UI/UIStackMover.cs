using UnityEngine;
using UnityEngine.EventSystems;

namespace Methodyca.Minigames.Protoescape
{
    public class UIStackMover : MonoBehaviour, IPointerClickHandler
    {
        private bool _isActivated;

        public void OnPointerClick(PointerEventData eventData)
        {
            GameManager_Protoescape.IsStacksMovable = _isActivated = !_isActivated;
        }
    }
}