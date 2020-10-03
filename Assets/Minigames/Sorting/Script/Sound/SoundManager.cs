﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Methodyca.Minigames.SortGame
{
    // The script handling in-game sound playback
    public sealed class SoundManager : MonoBehaviour
    {
        public static SoundManager instance;

        /******
         * Sounds can be devided to BGM and SFX audio sources
         * BGM audio source will play and switch BGMs, and same for SFX audio source
         * thus no need to make a list of all BGMs and all SFXs
         *******/
        //public Sound[] sounds;      //******************** Check Play() ln34


        //private float x;        //for stereo imaging      //******** unused
        //public Vector2 pos;     //for stereo imaging.     //******** unused

        //public float y;                      //************ unused
        //public float freq;                   //************ unused
        //public float gain;                   //************ unused

        private AudioSource BGMPlayer;
        private List<Sound> SFXPlayers = new List<Sound>();


        void Awake()
        {
            // Make it Singelton
            if (instance == null)
                instance = this;


            //for preloading the sounds
            //foreach (Sound s in sounds)       //looping sound called from the SOUND SCRIPT
            //{
            //    s.source = gameObject.AddComponent<AudioSource>();
            //    s.source.clip = s.clip;
            //    s.source.volume = s.amp;
            //    s.source.pitch = s.pitch;
            //    s.source.loop = s.loop;
            //    s.source.panStereo = s.pan;
            //}
            BGMPlayer = gameObject.AddComponent<AudioSource>();
        }

        void Start()
        {
            StartCoroutine(CheckEndedSFX());

            //Play("Fraud Full");      //Background Music 
        }

        //public void Play (string name)
        //{
        //    //finding the name of the sound
        //    Sound s = Array.Find(sounds, sound => sound.name == name);

        //    if (s == null)
        //         return;
            
        //    s.source.Play(); 
        //}

        public void PlayBGM(Sound soundClip)
        {
            BGMPlayer.clip = soundClip.clip;
            BGMPlayer.volume = soundClip.amp;
            BGMPlayer.pitch = soundClip.pitch;
            BGMPlayer.loop = soundClip.loop;
            BGMPlayer.panStereo = soundClip.pan;
            BGMPlayer.Play();
        }

        public void PlaySFX(Sound sfxCLip, bool doMouseImaging = false)
        {
            // if the SFX is already playing then just reset it and play from the start
            if (SFXPlayers.Exists(x => x.name == sfxCLip.name))
            {
                SFXPlayers.Find(x => x.name == sfxCLip.name).source.Play();
                return;
            }

            SFXPlayers.Add(sfxCLip);
            sfxCLip.source = gameObject.AddComponent<AudioSource>();

            sfxCLip.source.clip = sfxCLip.clip;
            sfxCLip.source.volume = sfxCLip.amp;
            sfxCLip.source.pitch = sfxCLip.pitch;
            sfxCLip.source.loop = sfxCLip.loop;

            // check if either the clip has panning set or imaging is required
            if (sfxCLip.pan != 0)
                sfxCLip.source.panStereo = sfxCLip.pan;
            else if (doMouseImaging)
                sfxCLip.source.panStereo = SFXImaging();

            sfxCLip.source.Play();
        }

        private float SFXImaging()
        {
            // get the horizontal component of the mouse
            float mouseX = Input.mousePosition.x;

            // compare the ratio between mouse horizontal position relative to the screen, then lerp it between range of values for stereo pan
            float panner = Mathf.Lerp(-1, 1, mouseX / Screen.width);
            return panner;
        }

        //public void Imaging(string name)    //Panning     //************** Moved to SFXImaging()
        //{
        //    Sound s = Array.Find(sounds, sound => sound.name == name);   
        //    pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);     //x position of the mouse 
        //    x= pos.x/7.5f;         //converting the vec2 to float 
        //    s.source.panStereo = x;        //float adjusting the pan of the audio clip

        //    /* Possible solution to panning/imaging
              
        //    float mouseX = Input.mousePosition.x;
        //    float panner = mouseX / Screen.width;
        //    s.source.panStereo = Mathf.Lerp(-1, 1, panner);
            
        //     */
        //}


        //public void Bounce(string name)       // *********** not used. removed
        //{
        //    Sound s = Array.Find(sounds, sound => sound.name == name);
        //    pos = new Vector2();
        //    pos.y = Mathf.Sin(Time.fixedTime * Mathf.PI * freq) * gain / 10f; //******* why divide by 10f?
        //    y= pos.y + 0.4f;        //*********** why add 0.4f? Sin() will produce a value between -1 and 1; thus will be -0.6 and 1.4
        //    s.source.volume = y;
        //}

        //public void Stop(string name)
        //{
        //    Sound s = Array.Find(sounds, sound => sound.name == name);
        //    s.source.Stop();
        //}

        public void StopBGM()
        {
            BGMPlayer.Stop();
        }

        public void StopSFX(string clipName)
        {
            if (SFXPlayers.Exists(x => x.name == clipName))
            {
                Sound SFXSound = SFXPlayers.Find(x => x.name == clipName);
                SFXSound.source.Stop();
                SFXPlayers.Remove(SFXSound);
                Destroy(SFXSound.source);
            }
        }

        // Check every 0.2 seconds if any SFX stopped playing (besides looping). If yes, then kill it 
        IEnumerator CheckEndedSFX()
        {
            foreach (Sound sfx in SFXPlayers.ToArray())
            {
                if (sfx.source.isPlaying == false)
                {
                    SFXPlayers.Remove(sfx);
                    Destroy(sfx.source);
                }
            }

            yield return new WaitForSeconds(0.2f);
            StartCoroutine(CheckEndedSFX());
        }

        public void ChangeAllSFXVolume(float volume)
        {
            SFXPlayers.ForEach(s => s.source.volume = Mathf.Clamp01(volume));
        }

        public void StopAllSFX()
        {
            foreach (Sound sfx in SFXPlayers.ToArray())
            {
                Destroy(sfx.source);
                SFXPlayers.Remove(sfx);
            }
        }
    } 
}

