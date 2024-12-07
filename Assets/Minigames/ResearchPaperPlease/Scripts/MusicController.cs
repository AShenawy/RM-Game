using UnityEngine;
using UnityEngine.UI;

public class MusicController : MonoBehaviour
{
    public Sprite offSprite;
    public Sprite onSprite;
    public AudioSource[] soundAudioSource;
    public AudioSource backgroundMusicAudioSource;

    public Image SoundImage;
    public Image backgroundMusicImage;
    private bool isSoundMuted = false;

    private bool isBackgroundMusicMuted = false;
    void Start()
    {
        UpdateButtonSprite();
    }

    // public void ToggleGlobalMusic()
    // {
    //     // Toggle the global mute state
    //     isMuted = !isMuted;

    //     // Set the global volume to 0 if muted, 1 if unmuted
    //     AudioListener.volume = isMuted ? 0 : 1;

    //     // Update the button sprite based on the current state
    //     UpdateButtonSprite();
    // }

    public void ToggleTargetMusic()
    {
        isSoundMuted = !isSoundMuted;

        if (soundAudioSource != null)
        {
            for (int i = 0; i < soundAudioSource.Length; i++)
            {
                soundAudioSource[i].mute = isSoundMuted;
            }
        }
        UpdateButtonSprite();
    }

    public void ToggleBackgroundMusic()
    {
        isBackgroundMusicMuted = !isBackgroundMusicMuted;

        if (backgroundMusicAudioSource != null)
        {
            backgroundMusicAudioSource.mute = !backgroundMusicAudioSource.mute;
        }
        UpdateButtonSprite();
    }

    private void UpdateButtonSprite()
    {
        if (SoundImage != null)
        {
            SoundImage.sprite = isSoundMuted ? offSprite : onSprite;
        }
        if (backgroundMusicImage != null)
        {
            backgroundMusicImage.sprite = isBackgroundMusicMuted ? offSprite : onSprite;
        }
    }
}

