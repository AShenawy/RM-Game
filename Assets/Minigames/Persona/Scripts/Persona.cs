using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Persona : MonoBehaviour
{
    public Button button;
    public Text buttonText;
    public string playerSide;
    public void SetSpace()
    {
        buttonText.text = playerSide;
        button.interactable = false;
    }
}
