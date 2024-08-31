using UnityEngine;
using UnityEngine.UI;

public class MusicController : MonoBehaviour
{
    public Sprite offSprite;
    public Sprite onSprite;

    private Image buttonImage;
    private bool isMuted = false;

    void Start()
    {
        buttonImage = GetComponent<Image>();
        UpdateButtonSprite();
    }

    public void ToggleMusic()
    {
        // Toggle the mute state
        isMuted = !isMuted;

        // Set the global volume to 0 if muted, 1 if unmuted
        AudioListener.volume = isMuted ? 0 : 1;

        // Update the button sprite based on the current state
        UpdateButtonSprite();
    }

    private void UpdateButtonSprite()
    {
        if (buttonImage != null)
        {
            buttonImage.sprite = isMuted ? offSprite : onSprite;
        }
    }
}
