using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpeechIO;

public class JJAnnounce : MonoBehaviour
{
    public string message;

    SpeechOut speech;

    // Start is called before the first frame update
    void Start()
    {
        speech = new SpeechOut();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    async void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player" && GameObject.Find("JJManager").GetComponent<JJManager>().ready)
        {
            await speech.Speak(message, 1.0f, SpeechBase.LANGUAGE.GERMAN);
        }
    }
}
