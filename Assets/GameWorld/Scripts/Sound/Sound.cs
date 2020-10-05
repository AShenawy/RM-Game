using UnityEngine.Audio;
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
    
}
