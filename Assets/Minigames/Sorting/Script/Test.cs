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
        public float volume;//the main volume. 

        [Range(0,1)]
        public float volumeAux;// the volume of the automation. 

        public float sampleRate = 44100;
        public float waveLengthInSeconds = 2.0f;

        public float y;//to convert vector to float 
        public float tick;//for the speed.
        public float gain;//like a compressor
        public float duh ;

        //public Vector3 saz;
        public Vector3 pos; //for the bounce. 
        public Vector3 temp = new Vector3();
        public RectTransform rig;
        public RectTransform him;
        
        public GameObject bling;


    
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
            rig = this.gameObject.GetComponent<RectTransform>();
            
        }
    
        void Update()
        {   
            audioSource.volume = volume;// the volume of the track. 
            
            
            if(Input.GetKeyDown(KeyCode.C))
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
            duh = y /20.94f ;
            volumeAux = duh - 9.37f;
            audioSource.volume = volumeAux;
        }
        
        // public void levi()
        // {
        //    temp = him.position;
        //    him.transform.Translate(Vector3.up * tick * Mathf.Sin(Time.timeSinceLevelLoad *gain));
        //    rig.position = him.position;
           
        // }

        
    }   
}