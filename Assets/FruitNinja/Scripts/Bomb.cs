using System.Collections;
using UnityEngine;
using DualPantoToolkit;
using SpeechIO;

public class Bomb : MonoBehaviour
{
    SpeechOut speechOut = new SpeechOut();

    PantoHandle upperHandle;
    PantoHandle lowerHandle;
    public float lowerAngle = 0f;
    public float upperAngle = 0f;
    private float angleStep = 10f;
    private float waitTime = 0.005f;
    private bool hit_bomb = false;
    public AudioClip bomb_sound;
    private AudioSource audioSource;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        lowerHandle = GameObject.Find("Panto").GetComponent<LowerHandle>();
        upperHandle = GameObject.Find("Panto").GetComponent<UpperHandle>();
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //private async void OnMouseDown()
    //{
    //    Destroy(gameObject);
    //    await speechOut.Speak("Fruit destroyed");
    //}

    async void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MeHandle"))
        {
            upperHandle.Freeze();
            lowerHandle.Freeze();
            rb.constraints = RigidbodyConstraints.FreezePosition;
            StartCoroutine(RotateBackAndForth());
            

            if (!hit_bomb)
            {
                hit_bomb = true;

                audioSource.PlayOneShot(bomb_sound);
                await speechOut.Speak("you hit the bomb. game over!");
            }
                 

    }
        else if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
            GameObject itHandleGodObject = GameObject.FindWithTag("ItHandle");
            if (itHandleGodObject != null)
            {
                Destroy(itHandleGodObject);
            }
            if (!hit_bomb)
            {
                await speechOut.Speak("you avoided the bomb!");
            }
        }
        else if (other.CompareTag("ItHandle"))
        {
            Physics.IgnoreCollision(other, GetComponent<Collider>());
        }
        else if (other.CompareTag("MeHandle"))
        {
            Physics.IgnoreCollision(other, GetComponent<Collider>());
        }
    }

    IEnumerator RotateBackAndForth()
    {
        //float angle = 0f;

        for (int j = 0; j < 3; j++)
        {
            for (int i = 0; i < 10; i++)
            {
                lowerHandle.Rotate(lowerAngle);
                upperHandle.Rotate(upperAngle);
                lowerAngle += angleStep;
                upperAngle -= angleStep;
                yield return new WaitForSeconds(waitTime);
            }

            for (int i = 0; i < 10; i++)
            {
                lowerHandle.Rotate(lowerAngle);
                upperHandle.Rotate(upperAngle);
                lowerAngle -= angleStep;
                upperAngle += angleStep;
                yield return new WaitForSeconds(waitTime);
            }
        }
    }
}
