using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using SpeechIO;

public class SpeechHandler : MonoBehaviour
{
    private SpeechOut speechOut;
    private bool locked;

    void Start()
    {
        speechOut = new SpeechOut();
    }

    public void Speak(string content)
    {
        if (!locked)
        {
            speechOut.Speak(content);
        }
    }
}