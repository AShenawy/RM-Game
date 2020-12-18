using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Methodyca.Minigames.Protoescape
{
    // This class is added to one shot SFX sound objects to destroy them after playback ends
    public class DestroyOnSFXEnded : MonoBehaviour
    {
        AudioSource aud;
        private void Start()
        {
            aud = GetComponent<AudioSource>();
        }
        void Update()
        {
            if (aud.isPlaying == false)
            {
                Destroy(gameObject);
            }
        }
    }
}