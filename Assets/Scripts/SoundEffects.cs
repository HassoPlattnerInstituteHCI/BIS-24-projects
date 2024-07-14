using UnityEngine;
using SpeechIO;
using Task = System.Threading.Tasks.Task;

public class SoundEffects : MonoBehaviour
{

    public AudioClip wallClip;
    public AudioClip coinClip;
    public AudioClip bombAvoidedClip;
    public AudioClip negativeClip;
    public AudioClip levelFinishedClip;
    private AudioSource audioSource;
    private SpeechOut speechOut;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        speechOut = new SpeechOut();
    }



    public void SpawnCoinClip() {
        speechOut.Speak("coin", 1.2f);
    }

    public void SpawnBombClip() {
        speechOut.Speak("bomb", 1.2f);
    }

    public void PlayWallClip()
    {
        audioSource.PlayOneShot(wallClip,1.0f);
    }

    public void PlayPositiveCoinClip() {
        audioSource.PlayOneShot(coinClip, 1.0f);
    }

    public void PlayNegativeCoinClip() {
        audioSource.PlayOneShot(negativeClip, 1.0f);
    }

    public void PlayNegativeBombClip() {
        audioSource.PlayOneShot(negativeClip, 1.0f);
    }

    public void PlayPositiveBombClip() {
        audioSource.PlayOneShot(bombAvoidedClip, 1.0f);
    }

    async public Task PlayFinishedLevelClip() {
        audioSource.PlayOneShot(levelFinishedClip, 1.0f);
    }
}
