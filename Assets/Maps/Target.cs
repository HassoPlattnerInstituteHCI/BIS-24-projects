using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpeechIO;


public class Target : MonoBehaviour
{
    // Start is called before the first frame update
    SpeechOut speech;
    void Start()
    {
        speech = new SpeechOut();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Well Done");
        speech.Speak("Well Done");
    }

    void OnApplicationQuit()
    {
        speech.Stop();
    }
}
