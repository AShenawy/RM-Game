using UnityEngine.Audio;
using System;
using UnityEngine;

namespace Methodyca.Minigames.SortGame
{
    
    public class SoundManager : MonoBehaviour
    {
       public Sound[] sounds;

       void Awake()//for preloading the sound
       {
           foreach(Sound s in sounds)//looping sound called from the SOUND SCRIPT
           {
               s.source = gameObject.AddComponent<AudioSource>();
               s.source.clip = s.clip;

               s.source.volume = s.amp;
               s.source.pitch = s.pitch;
               s.source.loop = s.loop;
               s.source.panStereo = s.pan;
           }
       }public void Play (string name)//Created a method to play the sound/
       {
           Sound s =Array.Find(sounds, sound => sound.name ==name);//finding the name of the sound
           if (s == null)
                return;
           s.source.Play(); 
       }
       void Start()
       {
           Play("Fraud Full");
       }
    } 

}

