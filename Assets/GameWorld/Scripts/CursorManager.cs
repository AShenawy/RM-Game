using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Methodyca.Core
{
    // This script handles the mouse cursor and mouse context menu
    public class CursorManager : MonoBehaviour
    {
        // Event called whenever state of context menu changes
        public delegate void OnContextMenu(bool value);
        public event OnContextMenu contextMenuDisabled;

        [SerializeField] private bool lockCursor;
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
          if(instance == null)
              instance = this;
      }
      #endregion
    
        // Start is called before the first frame update
        void Start()
        {
            if (lockCursor)
                Cursor.lockState = CursorLockMode.Confined;
        }
    
        // shows the mouse's context menu
        public void ShowContextMenu()
        {
            menuContext.SetActive(true);    // show the menu
            menuContext.transform.position = Input.mousePosition;   // show the menu at the mouse's location
            
            contextMenuDisabled?.Invoke(false);    // invoke menu state event
        }
    
        // Hides the mouse's context menu
        public void HideContextMenu()
        {
            menuContext.SetActive(false);       // hide the menu

            contextMenuDisabled?.Invoke(true);   // invoke menu state event
        }
    
        // Allows to directly set cursor to default without giving enum arguments (SetCursor method).
        // Useful when invoked by buttons and GUI, since it doesn't support enum arguments.
        public void SetDefaultCursor()
        {
            SetCursor(CursorTypes.Default, null);
        }

        public void ShowCursor(bool value)
        {
            Cursor.visible = value;
        }
    
        public void SetCursor(CursorTypes cursorType, Texture2D heldItemImage)
        {
            switch(cursorType)
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
                
                // If player is holding item
                case CursorTypes.ItemHeld:
                    Cursor.SetCursor(heldItemImage, new Vector2(heldItemImage.width / 2, heldItemImage.height / 2), CursorMode.Auto);
                    break;
    
                default:
                    Cursor.SetCursor(cursorDefault, Vector2.zero, CursorMode.Auto);
                    break;
            }
        }
    }
    
    // enum for different situations the cursor can be in
    public enum CursorTypes { Default, Interact, turnLeft, turnRight, ItemHeld }
}