using UnityEngine.Audio;
using UnityEngine;

namespace Methodyca.Minigames.SortGame
{
    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
        
        [Range(0f,1f)] 
        public float amp;   //volume of the sound 
        
        [Range(.1f,3f)]
        public float pitch; //pitch of the sound
    
        public bool loop;   //does track loop? 
    
        [Range(-1f,1f)]
        public float pan;   //stereo imaging of the sound
    
        [HideInInspector]
        public AudioSource source;  //audio source of the sounds
    }   
}
