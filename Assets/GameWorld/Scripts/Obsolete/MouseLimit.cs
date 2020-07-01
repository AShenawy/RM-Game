using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLimit : MonoBehaviour
{

    public float minX, maxX, minY, maxY;
    public Texture2D crosshairTexture;
    
    void OnGUI () 
    {
        Vector2 mousePos = Input.mousePosition;

        mousePos.x = Mathf.Clamp(mousePos.x, minX, maxX);
        mousePos.y = Mathf.Clamp(mousePos.y, minY, maxY);
        GUI.DrawTexture(new Rect(mousePos.x, mousePos.y,crosshairTexture.width,crosshairTexture.height), crosshairTexture);


    }

}
