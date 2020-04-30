using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public string buttonRight;
    public string buttonLeft;
    
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(buttonRight))
            Turn("right");
        
        else if (Input.GetKeyDown(buttonLeft))
            Turn("left");
    }

    public void Turn(string direction)
    {
        if (direction == "right")
            transform.Rotate(0, 90, 0);
        else if (direction == "left")
            transform.Rotate(0, -90, 0);
    }

    public void SetCursor(Texture2D texture)
    {
        Cursor.SetCursor(texture, Vector2.zero,CursorMode.Auto);
    }
}
