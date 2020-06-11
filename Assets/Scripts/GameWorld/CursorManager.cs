using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [Header("Cursor Styles")]
    [SerializeField] private Texture2D cursorDefault;
    [SerializeField] private Texture2D cursorInteract;
    [SerializeField] private Texture2D cursorPointLeft;
    [SerializeField] private Texture2D cursorPointRight;

    // Make Cursor Manager a singleton
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
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void SetDefaultCursor()
    {
        SetCursor(CursorTypes.Default);
    }

    public void SetCursor(CursorTypes cursorType)
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
            
            default:
                Cursor.SetCursor(cursorDefault, Vector2.zero, CursorMode.Auto);
                break;
        }
    }
}

// enum for different situations the cursor can be in
public enum CursorTypes { Default, Interact, turnLeft, turnRight }
