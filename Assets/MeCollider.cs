using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpeechIO;
using System.Threading.Tasks;

public class MeCollider : MonoBehaviour
{
    GameManager gameManager;
    SpeechOut speechOut;
    bool holeFound = false;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Panto").GetComponent<GameManager>();
        speechOut = new SpeechOut();
        speechOut.SetLanguage(SpeechBase.LANGUAGE.GERMAN); // alternatively GERMAN or JAPANESE
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    async void OnTriggerEnter(Collider collision)
    {
        if (collision.GetComponent<Collider>().CompareTag("Hole"))
        {
            if (holeFound) {
                await speechOut.Speak("whole.");
            }
            holeFound = true;
        }
    }

    async void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Collider>().CompareTag("Sand"))
        {
            await speechOut.Speak("Sand.");
        }
    }

    public async Task WaitForHoleFound()
    {
        holeFound = false;
        while (!holeFound)
        {
            await Task.Delay(100);
        }

        return;
    }
}
