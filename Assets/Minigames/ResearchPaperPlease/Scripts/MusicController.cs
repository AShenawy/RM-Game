using UnityEngine;
using UnityEngine.UI;

public class MusicController : MonoBehaviour
{
    public Sprite offSprite;
    public Sprite onSprite;
    public AudioSource correctOrWrongAudioSource;
    public AudioSource backgroundMusicAudioSource;

    public Image globalMusicImage;
    public Image correctOrWrongSoundImage;
    public Image backgroundMusicImage;
    private bool isMuted = false;
    private bool isCorrectOrWrongMuted = false;

    private bool isBackgroundMusicMuted = false;
    void Start()
    {
        UpdateButtonSprite();
    }

    public void ToggleGlobalMusic()
    {
        // Toggle the global mute state
        isMuted = !isMuted;

        // Set the global volume to 0 if muted, 1 if unmuted
        AudioListener.volume = isMuted ? 0 : 1;

        // Update the button sprite based on the current state
        UpdateButtonSprite();
    }

    public void ToggleTargetMusic()
    {
        isCorrectOrWrongMuted = !isCorrectOrWrongMuted;

        if (correctOrWrongAudioSource != null)
        {
            correctOrWrongAudioSource.mute = isCorrectOrWrongMuted;
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
        if (globalMusicImage != null)
        {
            globalMusicImage.sprite = isMuted ? offSprite : onSprite;
        }
        if (correctOrWrongSoundImage != null)
        {
            correctOrWrongSoundImage.sprite = isCorrectOrWrongMuted ? offSprite : onSprite;
        }
        if (backgroundMusicImage != null)
        {
            backgroundMusicImage.sprite = isBackgroundMusicMuted ? offSprite : onSprite;
        }
    }
}

