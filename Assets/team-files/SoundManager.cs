using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] audioClipArray;
    public AudioSource audioSource;
  
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            playSelectSound();
        }
    }

    public void playSelectSound()
    {
        audioSource.PlayOneShot(audioClipArray[0]);
    }

    public void playPlaceSound()
    {
        audioSource.PlayOneShot(audioClipArray[1]);
    }

    public void playDestroySound()
    {
        audioSource.PlayOneShot(audioClipArray[2]);
    }
}
