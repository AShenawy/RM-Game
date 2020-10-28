using UnityEngine;
using UnityEngine.UI;

namespace Methodyca.Minigames.Protoescape
{
    public class UIBaseTool : MonoBehaviour
    {
        [SerializeField] protected Image toolbarIcon;

        private readonly Color _enabledColor = Color.white;
        private readonly Color _disabledColor = new Color(1, 1, 1, 0.5f);

        protected virtual void OnEnable()
        {
            GameManager_Protoescape.OnSelected += SelectedHandler;
        }

        protected virtual void Start()
        {
            Disable();
        }

        private void SelectedHandler(GameObject selection)
        {
            SelectionTriggered(selection);
        }

        protected virtual void SelectionTriggered(GameObject selection) { }

        protected void Enable()
        {
            toolbarIcon.color = _enabledColor;
            toolbarIcon.raycastTarget = true;
        }

        protected void Disable()
        {
            toolbarIcon.color = _disabledColor;
            toolbarIcon.raycastTarget = false;
        }

        protected virtual void OnDisable()
        {
            GameManager_Protoescape.OnSelected -= SelectedHandler;
        }
    }
}