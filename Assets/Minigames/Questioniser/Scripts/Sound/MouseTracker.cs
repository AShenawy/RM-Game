using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTracker : MonoBehaviour
{

    public float mouseX;
    public float mouseY;
    public float screenHieght;
    public float screenWidth;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mouseInput = Input.mousePosition;
        mouseX = mouseInput.x;
        mouseY = mouseInput.y;
        screenHieght = Screen.height;
        screenWidth = Screen.width;
    }
}
