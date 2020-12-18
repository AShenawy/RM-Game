using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Methodyca.Minigames.Protoescape
{ 

    public class BGM : MonoBehaviour
    {
        public Sound BGMUsic;

        void Start()
        {
            PlayBGM();
        }
        public void PlayBGM()
        {
            SoundManager.instance.PlayBGM(BGMUsic);
        }
    }
}