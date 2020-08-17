using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;

namespace Methodyca.Minigames.SortGame{

    public class Test : MonoBehaviour  //IPointerDownHandler 
    {   
        [Range(1,20000)]  //Creates a slider in the inspector
        public float frequency1;
    
        [Range(1,20000)]  //Creates a slider in the inspector
        public float frequency2;

        [Range (0,1f)]
        public float volume;

        [Range(0,1)]
        public float volumeMain;

        public float sampleRate = 44100;
        public float waveLengthInSeconds = 2.0f;

        public float y;//to convert vector to float 
        public float tick;//for the speed.
        public float gain;//like a compressor
        public float duh;

        public Vector2 pos; //for the bounce. 


    
        public AudioSource audioSource;
        int timeIndex = 0;


        void Awake()
        {
            
        }
        void Start()
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.spatialBlend = 0; //force 2D sound
            audioSource.Stop(); //avoids audiosource from starting to play automatically
            
        }
    
        void Update()
        {   
            
            audioSource.volume = volume;

            if(Input.GetKeyDown(KeyCode.Space))
            {
                if(!audioSource.isPlaying)
                {
                    timeIndex = 0;  //resets timer before playing sound
                    audioSource.Play();
                    Debug.Log("Keys are playing");
                }
                else
                {
                    audioSource.Stop();
                    Debug.Log("Stop Keys");
                
                }   
            }

            Shake(audioSource);
            
            
            Limiter(volumeMain);
            
            
        }
    
        void OnAudioFilterRead(float[] data, int channels)
        {
            for(int i = 0; i < data.Length; i+= channels)
            {          
                data[i] = CreateSine(timeIndex, frequency1, sampleRate);
            
                if(channels == 2)
                    data[i+1] = CreateSine(timeIndex, frequency2, sampleRate);
            
                timeIndex++;
            
                //if timeIndex gets too big, reset it to 0
                if(timeIndex >= (sampleRate * waveLengthInSeconds))
                {
                    timeIndex = 0;
                }
            }
        }
    
        //Creates a sinewave
        public float CreateSine(int timeIndex, float frequency, float sampleRate)
        {
            return Mathf.Sin(2 * Mathf.PI * timeIndex * frequency / sampleRate);
        }
        public void Shake(AudioSource audioSource)//The automatied shake
        {
            pos =  new Vector2();
            pos.y = Mathf.Sin(Time.fixedTime * Mathf.PI* tick)* gain/10f;
            y = pos.y + 0.4f;
            volume = y;
        }
        public float Limiter(float soo)
        {
            return duh = volume;// real time volume.
            
        }
    }   
}