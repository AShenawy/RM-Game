using UnityEngine;
using UnityEngine.UI;

public class UIButtonsBehaviour : MonoBehaviour
{
    private Sprite mutedSprite;
    private Sprite unmutedSprite;
    private GameObject muteBtn;

    void Start()
    {
        muteBtn = GameObject.Find("MuteBtn");
        unmutedSprite = Resources.Load<Sprite>("Images/Icons/unmute");
        mutedSprite = Resources.Load<Sprite>("Images/Icons/mute");
        if (Sound.muted)
        {
            muteBtn.GetComponent<Image>().sprite = mutedSprite;
        }
        else
        {
            muteBtn.GetComponent<Image>().sprite = unmutedSprite;
        }
        
        muteBtn.GetComponent<Button>().onClick.AddListener(delegate
        {
            if (Sound.muted)
            {
                Sound.muted = false;
                AudioListener.volume = 1;
                muteBtn.GetComponent<Image>().sprite = unmutedSprite;
            }
            else
            {
                Sound.muted = true;
                AudioListener.volume = 0;
                muteBtn.GetComponent<Image>().sprite = mutedSprite;
            }
        });
    }
}
