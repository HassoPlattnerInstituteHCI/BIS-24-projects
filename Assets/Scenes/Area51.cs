using UnityEngine;
using SpeechIO;

public class Area51 : MonoBehaviour
{
    public AudioClip ambientSound;
    public AudioClip successSound;
    private AudioSource audioSourceA;
    private AudioSource audioSourceS;
    private Game game;
    private bool finished = false;
    private bool foundHole = false;
    private SpeechOut speechOut;

    private void Awake()
    {
        speechOut = new SpeechOut();
    }

    void Start()
    {
        audioSourceA = gameObject.AddComponent<AudioSource>();
        audioSourceS = gameObject.AddComponent<AudioSource>();
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
        audioSourceA.playOnAwake = false;
        audioSourceA.clip = ambientSound;
        audioSourceA.volume = 0.5f;
        audioSourceS.playOnAwake = false;
        audioSourceS.clip = successSound;
        audioSourceS.volume = 0.5f;
        game = GameObject.Find("Game").GetComponent<Game>();
    }

    async void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Golfball")
        {
            if (!finished)
            {
                audioSourceS.Play();
                finished = true;
                await speechOut.Speak("Congratulations. You finished this level.");
            }
        }
        else if (collider.tag == "Player")
        {
            if (!finished)
            {
                audioSourceS.Play();
                if (!foundHole)
                {
                    foundHole = true;
                    await speechOut.Speak("Congratulations. This is the whole. You finished this level.");
                }
            }
        }
    }

  
    async void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Player")
        {
            audioSourceA.Stop();
        }
        if (collider.tag == "Golfball")
        {
            audioSourceS.Stop();
        }
    }
}
