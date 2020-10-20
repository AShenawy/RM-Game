using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< HEAD
using System;

namespace Methodyca.Core
{
    public sealed class SoundManager : MonoBehaviour
    {
        public static SoundManager instance;
        private AudioSource BGMPlayer;
        private List<Sound> SFXPlayers = new List<Sound>();

        private void Awake()
        {
            if (instance == null)
                instance = this;
            BGMPlayer = gameObject.AddComponent<AudioSource>();
        }

        private void Start()
=======


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
>>>>>>> 98d7a18d1e6b0b4d16c16883d8cfe0758e26c969
        {
            StartCoroutine(CheckEndedSFX());
        }

        public void PlayBGM(Sound soundClip)
        {
            BGMPlayer.clip = soundClip.clip;
<<<<<<< HEAD
            BGMPlayer.volume = soundClip.volume;
=======
            BGMPlayer.volume = soundClip.amp;
>>>>>>> 98d7a18d1e6b0b4d16c16883d8cfe0758e26c969
            BGMPlayer.pitch = soundClip.pitch;
            BGMPlayer.loop = soundClip.loop;
            BGMPlayer.panStereo = soundClip.pan;
            BGMPlayer.Play();
        }

<<<<<<< HEAD
        public void PlaySFX(Sound sfxClip, bool doMouseImaging = false)
        {
            if (SFXPlayers.Exists(x => x.name == sfxClip.name))
            {
                SFXPlayers.Find(x => x.name == sfxClip.name).Source.Play();
                return;
            }
            SFXPlayers.Add(sfxClip);
            sfxClip.Source = gameObject.AddComponent<AudioSource>();
            sfxClip.Source.clip = sfxClip.clip;
            sfxClip.Source.volume = sfxClip.volume;
            sfxClip.Source.pitch = sfxClip.pitch;
            sfxClip.Source.loop = sfxClip.loop;

            if (sfxClip.pan != 0)
                sfxClip.Source.panStereo = sfxClip.pan;
            else if (doMouseImaging)
                sfxClip.Source.panStereo = sfxClip.pan;

            sfxClip.Source.Play();

=======
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
>>>>>>> 98d7a18d1e6b0b4d16c16883d8cfe0758e26c969
        }

        private float SFXImaging()
        {
<<<<<<< HEAD
            float mouseX = Input.mousePosition.x;
=======
            // get the horizontal component of the mouse
            float mouseX = Input.mousePosition.x;

            // compare the ratio between mouse horizontal position relative to the screen, then lerp it between range of values for stereo pan
>>>>>>> 98d7a18d1e6b0b4d16c16883d8cfe0758e26c969
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
<<<<<<< HEAD
                SFXSound.Source.Stop();
                SFXPlayers.Remove(SFXSound);
                Destroy(SFXSound.Source);
            }
        }

=======
                SFXSound.source.Stop();
                SFXPlayers.Remove(SFXSound);
                Destroy(SFXSound.source);
            }
        }

        // Check every 0.2 seconds if any SFX stopped playing (besides looping). If yes, then kill it 
>>>>>>> 98d7a18d1e6b0b4d16c16883d8cfe0758e26c969
        IEnumerator CheckEndedSFX()
        {
            foreach (Sound sfx in SFXPlayers.ToArray())
            {
<<<<<<< HEAD
                if (sfx.Source.isPlaying == false)
                {
                    SFXPlayers.Remove(sfx);
                    Destroy(sfx.Source);
                }
            }
            yield return new WaitForSeconds(0.25f);
=======
                if (sfx.source.isPlaying == false)
                {
                    SFXPlayers.Remove(sfx);
                    Destroy(sfx.source);
                }
            }

            yield return new WaitForSeconds(0.2f);
>>>>>>> 98d7a18d1e6b0b4d16c16883d8cfe0758e26c969
            StartCoroutine(CheckEndedSFX());
        }

        public void ChangeAllSFXVolume(float volume)
        {
<<<<<<< HEAD
            SFXPlayers.ForEach(s => s.Source.volume = Mathf.Clamp01(volume));
=======
            SFXPlayers.ForEach(s => s.source.volume = Mathf.Clamp01(volume));
>>>>>>> 98d7a18d1e6b0b4d16c16883d8cfe0758e26c969
        }

        public void StopAllSFX()
        {
            foreach (Sound sfx in SFXPlayers.ToArray())
            {
<<<<<<< HEAD
                Destroy(sfx.Source);
                SFXPlayers.Remove(sfx);
            }
        }
    }
=======
                Destroy(sfx.source);
                SFXPlayers.Remove(sfx);
            }
        }
    } 
>>>>>>> 98d7a18d1e6b0b4d16c16883d8cfe0758e26c969
}

