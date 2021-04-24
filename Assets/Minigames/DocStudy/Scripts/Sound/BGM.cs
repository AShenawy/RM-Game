using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Methodyca.Minigames.DocStudy
{
    public class BGM : MonoBehaviour
    {
        public Sound MainMenuBGM;
        void Start()
        {
            SoundManager.instance.PlayBGM(MainMenuBGM);
        }

    }
}

