using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTest : MonoBehaviour
{
    // Start is called before the first frame update
    AudioSource audioSource;
    public AudioClip clip;
    public int count = 0;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        clip = audioSource.clip;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate() {
        // if (count == 0) {
        //     audioSource.PlayOneShot(clip, 1.0f);
        // }
        // count ++;
        // count = count % 10;
    }
}
