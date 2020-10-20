<<<<<<< HEAD
﻿using UnityEngine.Audio;
using UnityEngine;

namespace Methodyca.Core
{
  
        [System.Serializable]
        public class Sound
        {
            public string name;
            public AudioClip clip;
            public bool loop;

            [Range(0f, 1f)]
            public float volume;

            [Range(.1f, 3f)]
            public float pitch = 1f;

            [Range(-1f, 1f)]
            public float pan;

            [HideInInspector]
            public AudioSource Source;
        }
    
=======
﻿using UnityEngine;

namespace Methodyca.Core
{
    // this class is to hold data about SFX or BGM clip
    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
        
        [Range(0f, 1f)] 
        public float amp = 1f;   //volume of the sound 
        
        [Range(-3f, 3f)]
        public float pitch = 1f; //pitch of the sound
    
        public bool loop;   //does track loop? 
        
        [Range(-1f, 1f)]
        public float pan = 0f;   //stereo imaging of the sound

        [HideInInspector]
        public AudioSource source;  //audio source of the sounds
    }   
>>>>>>> 98d7a18d1e6b0b4d16c16883d8cfe0758e26c969
}
