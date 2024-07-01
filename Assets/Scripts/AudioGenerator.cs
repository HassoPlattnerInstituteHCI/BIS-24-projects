using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class AudioGenerator: MonoBehaviour
{
    [Range(1,20000)]  //Creates a slider in the inspector
    public float frequency1 = 261.63f;
 
    [Range(1,20000)]  //Creates a slider in the inspector
    public float frequency2 = 392.0f;
 
    public float sampleRate = 44100;
    public float waveLengthInSeconds = 2.0f;
 
    AudioSource audioSource;
    int timeIndex = 0;
    private float stereoPan = 0;
 
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 1; //force 2D sound
        // audioSource.Stop(); //avoids audiosource from starting to play automatically
        
    }
   
    void Update()
    {
        // if(Input.GetKeyDown(KeyCode.Space))
        // {
        //     if(!audioSource.isPlaying)
        //     {
        //         timeIndex = 0;  //resets timer before playing sound
        //         audioSource.Play();
        //     }
        //     else
        //     {
        //         audioSource.Stop();
        //     }
        // }
        
          //resets timer before playing sound

        stereoPan = transform.position.x / 10.0f;
    }
   
    void OnAudioFilterRead(float[] data, int channels)
    {
        for(int i = 0; i < data.Length; i+= channels)
        {          
            
            if (stereoPan<0) data[i] = stereoPan*CreateSine(timeIndex, frequency1, sampleRate);
           
            if(channels == 2)
                if (stereoPan>0) data[i+1] = stereoPan*CreateSine(timeIndex, frequency2, sampleRate);
           
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
}
