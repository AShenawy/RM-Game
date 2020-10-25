using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Methodyca.Core
{
    public class BGM : MonoBehaviour
    {

        public Sound BGMUsic;
        // Start is called before the first frame update
        void Start()
        {
            SoundManager.instance.PlayBGM(BGMUsic);
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void BackToMain()
        {
            SoundManager.instance.PlayBGM(BGMUsic);
            //Debug.Log("EggFriedRice");
        }
    }
}
