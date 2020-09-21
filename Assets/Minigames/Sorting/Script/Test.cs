using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;

namespace Methodyca.Minigames.SortGame{

    public class Test : MonoBehaviour  //IPointerDownHandler 
    {   

        //The Ossicaltor. 
        public DragSlot bling;
        public GameManager gameManager;
        SoundManager soundMan; 

        [Range(1,20000)]  //Creates a slider in the inspector
        public float frequency1;
    
        [Range(1,20000)]  //Creates a slider in the inspector
        public float frequency2;

        [Range (0,1f)]
        public float volume;//the main volume. 

        [Range(0,1)]
        public float volumeAux;// the volume of the automation. 

        public float sampleRate = 44100;
        public float waveLengthInSeconds = 2.0f;

        public float y;//to convert vector to float 
        public float tick;//for the speed.
        public float gain;//like a compressor
      

       
        public Vector3 temp = new Vector3();
        public RectTransform rig;
        
        
        


    
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
            //rig = this.gameObject.GetComponent<RectTransform>();
            soundMan = FindObjectOfType<SoundManager>();
            
        }
    
        void Update()
        {   
            audioSource.volume = volume;// the volume of the track. 
            
            
            if(bling.done == true)
            
            {
                    
                if(!audioSource.isPlaying)
                {
                    audioSource.Play();
                    timeIndex = 0;  //resets timer before playing sound
                    Debug.Log("Keys are playing");
                }
            }
            if(gameManager.completed==true)
            {
                audioSource.Stop();
            }
            Ossci();
            Shake(audioSource);
            
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
        
        //Matches the wave with the game object
        public void Shake(AudioSource audioSource) 
        {
            y = rig.position.y;
            volumeAux = y;
            audioSource.volume = volumeAux;
        }  
        
        public void Ossci()
        {
            temp.y = gain * Mathf.Sin (tick* Time.fixedTime);
            rig.position = temp; //removing this makes the position stay the same.
        }

        
    }   
}