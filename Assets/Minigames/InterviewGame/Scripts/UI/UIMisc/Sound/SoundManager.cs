using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Methodyca.Minigames.Interview
{
        public sealed class SoundManager : MonoBehaviour
    {
        public static SoundManager instance;
        private AudioSource BGMPlayer;
        private List<Sounds> SFXPlayers = new List<Sounds>();

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                //DontDestroyOnLoad(this);
            }
            // else if (instance != this)
            //     Destroy(gameObject);

            BGMPlayer = gameObject.AddComponent<AudioSource>();
        }

        private void Start()
        {
            StartCoroutine(CheckEndedSFX());
        }

        public void PlayBGM(Sounds soundClip)
        {
            BGMPlayer.clip = soundClip.clip;
            BGMPlayer.volume = soundClip.volume;
            BGMPlayer.pitch = soundClip.pitch;
            BGMPlayer.loop = soundClip.loop;
            BGMPlayer.panStereo = soundClip.pan;
            BGMPlayer.Play();

        }

        public void PlaySFX(Sounds sfxClip, bool doMouseImaging = false)
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

        }

        private float SFXImaging()
        {
            float mouseX = Input.mousePosition.x;
            float panner = Mathf.Lerp(-1, 1, mouseX / Screen.width);
            return panner;
        }

        public void StopBGM()
        {
            BGMPlayer.Stop();
        }

        public void StopSFX(string clipName)
        {
            if(SFXPlayers.Exists(x=>x.name==clipName))
            {
                Sounds SFXSound = SFXPlayers.Find(x => x.name == clipName);
                SFXSound.Source.Stop();
                SFXPlayers.Remove(SFXSound);
                Destroy(SFXSound.Source);
            }
        }

        IEnumerator CheckEndedSFX()
        {
            foreach(Sounds sfx in SFXPlayers.ToArray())
            {
                if(sfx.Source.isPlaying == false)
                {
                    SFXPlayers.Remove(sfx);
                    Destroy(sfx.Source);
                }
            }
            yield return new WaitForSeconds(0.25f);
            StartCoroutine(CheckEndedSFX());
        }

        public void ChangeAllSFXVolume(float volume)
        {
            SFXPlayers.ForEach(s => s.Source.volume = Mathf.Clamp01(volume));
        }

        public void StopAllSFX()
        {
            foreach(Sounds sfx in SFXPlayers.ToArray())
            {
                Destroy(sfx.Source);
                SFXPlayers.Remove(sfx);
            }
        }
}
    }

