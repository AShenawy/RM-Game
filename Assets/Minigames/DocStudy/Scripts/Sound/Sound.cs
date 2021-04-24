using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Methodyca.Minigames.DocStudy
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

        [Range(0.1f, 0.7f)]
        public float pan;

        [HideInInspector]
        public AudioSource Source;
}
}

