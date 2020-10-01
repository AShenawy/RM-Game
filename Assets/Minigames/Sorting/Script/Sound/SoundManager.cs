﻿using System;
using UnityEngine;


namespace Methodyca.Minigames.SortGame
{
    // The script handling in-game sound playback
    public class SoundManager : MonoBehaviour
    {
        /******
         * Sounds can be devided to BGM and SFX audio sources
         * BGM audio source will play and switch BGMs, and same for SFX audio source
         * thus no need to make a list of all BGMs and all SFXs
         *******/
        public Sound[] sounds;      //******************** Check Play() ln34

        
        private float x;        //for stereo imaging 
        public Vector2 pos;     //for stereo imaging.
        
        public float y;
        public float freq;
        public float gain;

        private AudioSource SFXPlayer;
        

        void Awake()
        {
            //for preloading the sounds
            foreach (Sound s in sounds)       //looping sound called from the SOUND SCRIPT
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;
                s.source.volume = s.amp;
                s.source.pitch = s.pitch;
                s.source.loop = s.loop;
                s.source.panStereo = s.pan;
            }
        }

        void Start()
        {
            SFXPlayer = gameObject.AddComponent<AudioSource>();

            Play("Fraud Full");      //Background Music 
        }

        public void Play (string name)
        {
            //finding the name of the sound
            Sound s = Array.Find(sounds, sound => sound.name == name);

            if (s == null)
                 return;
            
            s.source.Play(); 
        }

        public void PlaySFX(AudioClip clip, bool doPan = false)
        {
            SFXPlayer.clip = clip;

            if (doPan)
                SFXPlayer.panStereo = SFXImaging();

            SFXPlayer.Play();
        }

        public float SFXImaging()
        {
            // get the horizontal component of the mouse
            float mouseX = Input.mousePosition.x;

            // compare the ratio between mouse horizontal position relative to the screen, then lerp it between range of values for stereo pan
            float panner = Mathf.Lerp(-1, 1, mouseX / Screen.width);
            return panner;
        }

        public void Imaging(string name)    //Panning
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);   
            pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);     //x position of the mouse 
            x= pos.x/7.5f;         //converting the vec2 to float 
            s.source.panStereo = x;        //float adjusting the pan of the audio clip

            /* Possible solution to panning/imaging
              
            float mouseX = Input.mousePosition.x;
            float panner = mouseX / Screen.width;
            s.source.panStereo = Mathf.Lerp(-1, 1, panner);
            
             */
        }


        //public void Bounce(string name)       // *********** not used. removed
        //{
        //    Sound s = Array.Find(sounds, sound => sound.name == name);
        //    pos = new Vector2();
        //    pos.y = Mathf.Sin(Time.fixedTime * Mathf.PI * freq) * gain / 10f; //******* why divide by 10f?
        //    y= pos.y + 0.4f;        //*********** why add 0.4f? Sin() will produce a value between -1 and 1; thus will be -0.6 and 1.4
        //    s.source.volume = y;
        //}

        public void Stop(string name)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            s.source.Stop();
        }
    } 
}

