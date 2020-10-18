using UnityEngine;
using UnityEngine.UI;

namespace Methodyca.Minigames.Interview
{
    public class MuteButtonBehaviour : MonoBehaviour
    {
        private Sprite mutedSprite;
        private Sprite unmutedSprite;

        void Start()
        {
            unmutedSprite = Resources.Load<Sprite>("Images/Icons/unmute");
            mutedSprite = Resources.Load<Sprite>("Images/Icons/mute");
            
            if (Sound.muted)
            {
                GetComponent<Image>().sprite = mutedSprite;
            }
            else
            {
                GetComponent<Image>().sprite = unmutedSprite;
            }

            GetComponent<Button>().onClick.AddListener(delegate
            {
                if (Sound.muted)
                {
                    Sound.muted = false;
                    AudioListener.volume = 1;
                    GetComponent<Image>().sprite = unmutedSprite;
                }
                else
                {
                    Sound.muted = true;
                    AudioListener.volume = 0;
                    GetComponent<Image>().sprite = mutedSprite;
                }
            });
        }
    }
}