using UnityEngine;


namespace Methodyca.Minigames.SortGame
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundManipulator : MonoBehaviour
    {   
        public SortingManager gameManager;
        public VerticalOscillator crystalOscillator;

        [Range(0, 20000)]
        public int frequency1;
    
        [Range(0, 20000)]
        public int frequency2;

        [Range (0f, 1f)]
        public float volume;    //the main volume. 

        [Range(0f, 1f)]
        public float volumeAux;     // the volume of the automation. 

        public int sampleRate = 44100;
        public float waveLengthInSeconds = 2.0f;

        float minOscillationPosition;
        float maxOscillationPosition;
        int timeIndex = 0;

        AudioSource audioSource;

        void Start()
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.spatialBlend = 0;       //force 2D sound
            audioSource.Stop();     //avoids audiosource from starting to play automatically
            audioSource.volume = volume;    // set initial track volume

            minOscillationPosition = crystalOscillator.minY;
            maxOscillationPosition = crystalOscillator.maxY;
        }
    
        void Update()
        {
            Shake(audioSource);
        }

        public void PlaySound()
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
                timeIndex = 0;           //resets timer before playing sound
                Debug.Log("Keys are playing");
            }
        }

        public void StopSound()
        {
            audioSource.Stop();
        }

        // Produces the humming effect 
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
    
        //Creates a sine wave
        float CreateSine(int timeIndex, float frequency, float sampleRate)
        {
            return Mathf.Sin(2 * Mathf.PI * timeIndex * frequency / sampleRate);
        }
        
        //Matches the wave with game object's vertical level
        void Shake(AudioSource audioSource) 
        {
            float motionRange = maxOscillationPosition - minOscillationPosition;
            float currentPos = crystalOscillator.GetComponent<RectTransform>().anchoredPosition.y;
            float ratio = (currentPos - minOscillationPosition) / motionRange;

            volumeAux = Mathf.Lerp(0, 1, ratio);
            audioSource.volume = volumeAux;
        }
    }
}