using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JJTarget : MonoBehaviour
{
    AudioSource audioSource;

    public AudioClip victory;

    AudioSource AddAudio()
    {
        AudioSource newAudio = gameObject.AddComponent<AudioSource>();
        newAudio.loop = false;
        newAudio.playOnAwake = false;
        newAudio.volume = 0.4f;
        return newAudio;
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = AddAudio();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void playSound(AudioSource source, AudioClip clip)
    {
        source.clip = clip;
        source.Play();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            playSound(audioSource, victory);
        }
    }
}
