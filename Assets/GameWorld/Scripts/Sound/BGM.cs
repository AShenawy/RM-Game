using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Methodyca.Core
{
    public class BGM : MonoBehaviour
    {
        public Sound BGMUsic;


        void Start()
        {
            BackToMain();
        }

        public void BackToMain()
        {
            SoundManager.instance.PlayBGM(BGMUsic);
        }
    }
}
