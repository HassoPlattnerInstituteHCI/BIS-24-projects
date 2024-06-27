using UnityEngine;
// using SpeechIO;
public class PlayerSoundEffect : MonoBehaviour
{

    public AudioClip paddleClip;
    public AudioClip wallClip;
    public AudioClip scoreClip;
    public AudioClip positiveScoreClip;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayPaddleClip()
    {
        audioSource.PlayOneShot(paddleClip);
    }

    public void PlayWallClip()
    {
        audioSource.PlayOneShot(wallClip,1.0f);
    }

    public void PlayScoreClip()
    {
        audioSource.PlayOneShot(scoreClip);
    }

    public void PlayPositiveScoreClip()
    {
        audioSource.PlayOneShot(positiveScoreClip);
    }
}