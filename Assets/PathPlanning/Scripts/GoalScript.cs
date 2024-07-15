using System;
using System.Collections;
using System.Collections.Generic;
using SpeechIO;
using UnityEngine;

public class GoalScript : MonoBehaviour
{
    private SpeechOut _speech; 

    void Start()
    {
        Debug.Log("Start called");
        _speech = new SpeechOut();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ItHandle"))
        {
            _speech.Speak("You have reached the goal. Yay.");
            Destroy(this.gameObject);
        }
    }
}
