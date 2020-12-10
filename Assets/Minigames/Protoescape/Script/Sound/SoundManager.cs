using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Methodyca.Minigames.Protoescape
{ 
    //The script handling in-game sound playback
    public sealed class SoundManager : MonoBehaviour
    {
        public static SoundManager instance;
        [HideInInspector]
        public Sound mainBGM;

        private AudioSource BGMPlayer;
        private List<Sound> SFXPlayers = new List<Sound>();


        void Awake()
        {
            // Make it Singelton
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this);
            }
            else if (instance != this)
                Destroy(gameObject);

            BGMPlayer = gameObject.AddComponent<AudioSource>();
        }

        void Start()
        {
            StartCoroutine(CheckEndedSFX());
        }

        public void PlayBGM(Sound soundClip)
        {
            BGMPlayer.clip = soundClip.clip;
            BGMPlayer.volume = soundClip.volume;
            BGMPlayer.pitch = soundClip.pitch;
            // force looping for BGM tracks
            BGMPlayer.loop = true;
            BGMPlayer.panStereo = soundClip.pan;
            BGMPlayer.Play();
        }

        public void PlayMainBGM()
        {
            PlayBGM(mainBGM);
        }

        public void PlaySFX(Sound sfxClip, bool doMouseImaging = false)
        {
            // if the SFX is already playing then just reset it and play from the start
            if (SFXPlayers.Exists(x => x.Name == sfxClip.Name))
            {
                SFXPlayers.Find(x => x.Name == sfxClip.Name).source.Play();
                return;
            }

            SFXPlayers.Add(sfxClip);
            sfxClip.source = gameObject.AddComponent<AudioSource>();

            sfxClip.source.clip = sfxClip.clip;
            sfxClip.source.volume = sfxClip.volume;
            sfxClip.source.pitch = sfxClip.pitch;
            sfxClip.source.loop = sfxClip.loop;

            // check if either the clip has panning set or imaging is required
            if (sfxClip.pan != 0)
                sfxClip.source.panStereo = sfxClip.pan;
            else if (doMouseImaging)
                sfxClip.source.panStereo = SFXImaging();

            sfxClip.source.Play();
        }

        public void PlaySFXOneShot(Sound sfxClip, bool doMouseImaging = false)
        {
            // create a new child game object and add an audioSource component to it
            GameObject sfxSource = new GameObject();
            sfxSource.transform.SetParent(gameObject.transform);
            sfxSource.name = "SFX Player";
            sfxClip.source = sfxSource.AddComponent<AudioSource>();

            sfxClip.source.clip = sfxClip.clip;
            sfxClip.source.volume = sfxClip.volume;
            sfxClip.source.pitch = sfxClip.pitch;
            sfxClip.source.loop = false;       // force loop to false as this method should play sfx only once

            // check if either the clip has panning set or imaging is required
            if (sfxClip.pan != 0)
                sfxClip.source.panStereo = sfxClip.pan;
            else if (doMouseImaging)
                sfxClip.source.panStereo = SFXImaging();

            sfxClip.source.Play();

            // attach the self-destructor on sfx ended component
            sfxSource.AddComponent<DestroyOnSFXEnded>();
        }

        private float SFXImaging()
        {
            // get the horizontal component of the mouse
            float mouseX = Input.mousePosition.x;

            // compare the ratio between mouse horizontal position relative to the screen, then lerp it between range of values for stereo pan
            float panner = Mathf.Lerp(-1, 1, mouseX / Screen.width);
            return panner;
        }

        public void StopBGM()
        {
            BGMPlayer.Stop();
        }

        public void StopSFX(string clipName)
        {
            if (SFXPlayers.Exists(x => x.Name == clipName))
            {
                Sound SFXSound = SFXPlayers.Find(x => x.Name == clipName);
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