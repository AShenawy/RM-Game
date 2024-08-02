using UnityEngine;
using UnityEngine.UI;

public class MusicController : MonoBehaviour
{
    public GameObject audioManager;
    public Sprite offSprite;
    public Sprite onSprite;

    private Image buttonImage;
    private bool isPlaying = true;

    public void ToggleMusic()
    {
        buttonImage = GetComponent<Image>();
        AudioSource backgroundMusic = audioManager.GetComponentInChildren<AudioSource>();

        if (backgroundMusic != null)
        {
            if (isPlaying)
            {
                backgroundMusic.Pause();
                buttonImage.sprite = offSprite;
            }
            else
            {
                backgroundMusic.Play();
                buttonImage.sprite = onSprite;
            }
            isPlaying = !isPlaying;
        }
    }
}
