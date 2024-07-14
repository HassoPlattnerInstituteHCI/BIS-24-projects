using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using SpeechIO;

public class Ball : MonoBehaviour
{
    GameManager gameManager;
    bool isInsideHole = false;
    SpeechOut speechOut;
    AudioSource audioSource;
    public AudioClip reflectionSound;
    public AudioClip shootSound;
    public AudioClip holeSound;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Panto").GetComponent<GameManager>();
        speechOut = new SpeechOut();
        speechOut.SetLanguage(SpeechBase.LANGUAGE.ENGLISH); // alternatively GERMAN or JAPANESE
        audioSource = GameObject.Find("SoundSource").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public async Task Shoot(Vector3 targetPosition)
    {
        audioSource.PlayOneShot(shootSound);
        Vector3 direction = targetPosition - transform.position;
        direction.y = 0;
        direction.Normalize();
        GetComponent<Rigidbody>().AddForce(direction * 400);

        await Task.Delay(100);
        while (GetComponent<Rigidbody>().velocity.magnitude > 1)
        {
            await Task.Delay(100);
        }
        GetComponent<Rigidbody>().velocity = Vector3.zero;

        return;
    }

    async void OnTriggerEnter(Collider collision)
    {
        if (collision.GetComponent<Collider>().CompareTag("Hole"))
        {
            isInsideHole = true;
            audioSource.PlayOneShot(holeSound);
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }

    async void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Wall"))
        {
            // reflect the ball off the wall

            audioSource.PlayOneShot(reflectionSound);
        }
    }

    public bool CheckBallOnSand() {
        GameObject[] sands = GameObject.FindGameObjectsWithTag("Sand");
        Vector3 ballPosition = transform.position;
        ballPosition.y = 0;
        foreach (GameObject sand in sands) {
            if (sand.GetComponent<Collider>().bounds.Contains(ballPosition)) {
                return true;
            }
        }
        return false;
    }

    public bool CheckBallInsideHole() {
        return isInsideHole;
    }

    public void ResetBallInsideHole() {
        isInsideHole = false;
    }
}
