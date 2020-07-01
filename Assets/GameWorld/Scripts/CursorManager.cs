using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameWorld
{
    // This script handles the mouse cursor and mouse context menu
    public class CursorManager : MonoBehaviour
    {
        [SerializeField] private GameObject menuContext;    // ref to the context menu GO

        [Header("Cursor Styles")]
        [SerializeField] private Texture2D cursorDefault;
        [SerializeField] private Texture2D cursorInteract;
        [SerializeField] private Texture2D cursorPointLeft;
        [SerializeField] private Texture2D cursorPointRight;


        // Make this class a singleton
        #region Singleton
        public static CursorManager instance;
        private void Awake()
        {
            if (instance == null)
                instance = this;
        }
        #endregion

        // Start is called before the first frame update
        void Start()
        {
            Cursor.lockState = CursorLockMode.Confined;
            HideContextMenu();
        }

        // shows the mouse's context menu
        public void ShowContextMenu()
        {
            menuContext.SetActive(true);    // show the menu
            menuContext.transform.position = Input.mousePosition;   // show the menu at the mouse's location
            GameManager.instance.RotationTriggersActive(false);     // prevent player from turning when context menu is up
        }

        // Hides the mouse's context menu
        public void HideContextMenu()
        {
            menuContext.SetActive(false);       // hide the menu
            GameManager.instance.RotationTriggersActive(true);      // return player ability to turn
        }

        // Allows to directly set cursor to default without giving enum arguments (SetCursor method).
        // Useful when invoked by buttons and GUI, since it doesn't support enum arguments.
        public void SetDefaultCursor()
        {
            SetCursor(CursorTypes.Default);
        }

        public void SetCursor(CursorTypes cursorType)
        {
            switch (cursorType)
            {
                // General default state cursor is in
                case CursorTypes.Default:
                    Cursor.SetCursor(cursorDefault, Vector2.zero, CursorMode.Auto);
                    break;

                // If cursor is pointing at something player can interact with
                case CursorTypes.Interact:
                    Cursor.SetCursor(cursorInteract, Vector2.zero, CursorMode.Auto);
                    break;

                // If cursor is at left edge of screen to turn
                case CursorTypes.turnLeft:
                    Cursor.SetCursor(cursorPointLeft, Vector2.zero, CursorMode.Auto);
                    break;

                // If cursor is at right edge of screen to turn
                case CursorTypes.turnRight:
                    Cursor.SetCursor(cursorPointRight, new Vector2(cursorPointRight.width, 0), CursorMode.Auto);
                    break;

                default:
                    Cursor.SetCursor(cursorDefault, Vector2.zero, CursorMode.Auto);
                    break;
            }
        }
    }

    // enum for different situations the cursor can be in
    public enum CursorTypes { Default, Interact, turnLeft, turnRight }
}