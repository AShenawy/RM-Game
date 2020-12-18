
using UnityEngine;
using UnityEngine.Audio;

namespace Methodyca.Minigames.Questioniser
{
    [System.Serializable]
    public class Sound//Custom sound Class. 
    {
        public string name;

        public AudioClip clip;

        public bool loop;

        [Range(0f, 1f)]
        public float volume;

        [Range(.1f, 3f)]
        public float pitch;

        [Range(0.1f, 0.7f)]
        public float pan;

        [HideInInspector]
        public AudioSource Source;


    }

}

