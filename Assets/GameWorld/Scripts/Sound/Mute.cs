using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Methodyca.Core;

namespace Methodyca.core
{
    
    public class Mute : MonoBehaviour
    {
        [SerializeField] Toggle audioToggle;
        //[TextArea (3,5)]public string message;


        // Start is called before the first frame update
        void Awake()
        {
            AudioListener.volume = GameObject.FindGameObjectWithTag("audio").GetComponent<SoundManager>().BGMPlayer.volume;
            audioToggle = GetComponent<Toggle>();
            
        }
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void Ismute()
        {
            if(AudioListener.volume > 0)
            {
                AudioListener.volume = 0;
                print("Audio is muted");
                //audioToggle.isOn = true; 
            }
            
            
        }
        public void IsntMute()
        {
            if(AudioListener.volume < 0)
            {
                AudioListener.volume = 0.5f;
                print("Audio isn't muted");

            }
        }
    }

}

