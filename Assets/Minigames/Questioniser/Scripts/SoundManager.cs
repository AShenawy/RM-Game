using System;
using UnityEngine;

namespace Methodyca.Minigames.Questioniser
{
    public class SoundManager : MonoBehaviour
    {
        public Sound[] sounds;
        public static SoundManager instance;
        private float x;//for stearo imaging 
        public Vector3 posX;//for stearo imaging.

        void Awake()
        {
            //for audio instance 
            if (instance == null)
                instance = this;
            else
            {
                Destroy(gameObject);
                    return;
            }
            DontDestroyOnLoad(gameObject);

            //for the sound
            foreach(Sound s in sounds)
            {
                s.Source = gameObject.AddComponent<AudioSource>();
                s.Source.clip = s.clip;
                s.Source.volume = s.volume;
                s.Source.pitch = s.pitch;
                s.Source.loop = s.loop;
                s.Source.panStereo = s.pan;
            }
         }
        public void Play(string name)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            if (s == null)
                return;
            s.Source.Play();
        }
        public void Stop(string name)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            s.Source.Stop();
        }
        public void StereoImaging(string name)//fix this later. 
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            posX = Camera.main.ScreenToViewportPoint(Input.mousePosition);//x position of the mouse
            x = posX.x -0.5f;//converting the vec2 to float 
            s.Source.panStereo = x;//float adjusting the pan of the audio clip.
            
        }
        void Start()
        {
            Play("Theme");
        }
    }
}