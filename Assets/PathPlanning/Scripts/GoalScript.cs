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
        _speech.Speak("Go find a path through the streets to reach the It-handle");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MeHandle")) _speech.Speak("You have reached the goal. Yay.");
    }
}
