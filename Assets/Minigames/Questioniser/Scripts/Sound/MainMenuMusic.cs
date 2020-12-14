using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Methodyca.Minigames.Questioniser
{
    public class MainMenuMusic : MonoBehaviour
    {
        // Start is called before the first frame update
        public Sound MainMenuBGM;

        void Start()
        {
            SoundManager.instance.PlayBGM(MainMenuBGM);
        }


    }
}
