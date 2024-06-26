using System;
using System.Collections;
using System.Collections.Generic;
using SpeechIO;
using UnityEngine;

public class GoalScript : MonoBehaviour
{
    private SpeechOut _speech; 
    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start called");
        _speech = new SpeechOut();
        _speech.Speak("Go find a path through the streets to reach the It-handle");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MeHandle")) _speech.Speak("You have reached the goal. Yay.");
    }
}
