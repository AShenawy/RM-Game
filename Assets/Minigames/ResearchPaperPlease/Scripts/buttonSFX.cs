using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonSFX : MonoBehaviour
{
    public AudioSource myFx; // Reference to the AudioSource component
    public AudioClip clickFx; // Audio clip to play when the button is clicked

    // Method to play the click sound effect
    public void ClickSound()
    {
        myFx.PlayOneShot(clickFx); // Play the click sound effect
    }
}
