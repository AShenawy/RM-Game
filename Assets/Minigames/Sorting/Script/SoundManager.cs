using UnityEngine.Audio;
using System;
using UnityEngine;

namespace Methodyca.Minigames.SortGame
{
    
    public class SoundManager : MonoBehaviour
    {
       public Sound[] sounds;
        
        private float x;//for stearo imaging 
        public Vector2 pos;//for stearo imaging.

        // RectTransform hmm;
        // private Vector3 levels = new Vector3();//for amp
        // private Vector3 slides = new Vector3();//for amp
        public float y;
        public float freq;
        public float gain; 

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
       }
       public void Play (string name)//Created a method to play the sound/
       {
           Sound s =Array.Find(sounds, sound => sound.name ==name);//finding the name of the sound
           if (s == null)
                return;
           s.source.Play(); 
       }
       void Start()
       {
           Play("Fraud Full");//Background Music 
       }
       
       public void Imaging(string name)//Panning
       {
           Sound s =Array.Find(sounds, sound => sound.name ==name);   
            pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);//x position of the mouse 
            x= pos.x/7.5f;//converting the vec2 to float 
            s.source.panStereo = x;//float adjusting the pan of the audio clip.      
       }
       public void Bounce(string name)
       {
           Sound s =Array.Find(sounds, sound => sound.name ==name);
            pos = new Vector2();
            pos.y = Mathf.Sin(Time.fixedTime * Mathf.PI* freq)* gain/10f;
            y= pos.y + 0.4f;
            s.source.volume = y;
       }
       public void Stop(string name)//Created to stop the sound. 
       {
           Sound s =Array.Find(sounds, sound => sound.name ==name);
           s.source.Stop();
       }
       void Update()
       {
           //Bounce("battery");
       }
    

    } 
    

}

