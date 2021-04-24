using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Methodyca.Minigames.Interview
{
    public class BGM : MonoBehaviour
    {
        public Sounds MainMenuBGM;

        void Start()
        {
            SoundManager.instance.PlayBGM(MainMenuBGM);
        }
    }
}

