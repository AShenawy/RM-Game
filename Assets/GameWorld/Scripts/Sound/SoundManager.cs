using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Methodyca.Core
{
    // The script handling in-game sound playback
    public sealed class SoundManager : MonoBehaviour
    {
        public static SoundManager instance;

        private AudioSource BGMPlayer;
        private List<Sound> SFXPlayers = new List<Sound>();


        void Awake()
        {
            // Make it Singelton
            if (instance == null)
                instance = this;

            BGMPlayer = gameObject.AddComponent<AudioSource>();
        }

        void Start()
        {
            StartCoroutine(CheckEndedSFX());
        }

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

