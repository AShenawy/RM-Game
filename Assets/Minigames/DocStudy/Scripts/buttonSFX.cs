using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Methodyca.Minigames.Utils; // Import the namespace containing AudioController

public class buttonSFX : MonoBehaviour
{
    public AudioSource myFx; // Reference to the AudioSource component on the button
    public AudioClip hoverFx; // Audio clip to play when the button is hovered over
    public AudioClip clickFx; // Audio clip to play when the button is clicked

    // Method to play the hover sound effect
    public void HoverSound()
    {
        if (myFx != null && hoverFx != null)
        {
            myFx.PlayOneShot(hoverFx); // Play the hover sound effect
        }
        else
        {
            Debug.LogWarning("AudioSource or hover sound effect is missing!");
        }
    }

    // Method to play the click sound effect
    public void ClickSound()
    {
        if (myFx != null && clickFx != null)
        {
            myFx.PlayOneShot(clickFx); // Play the click sound effect
        }
        else
        {
            Debug.LogWarning("AudioSource or click sound effect is missing!");
        }
    }
}


