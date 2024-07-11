using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DualPantoToolkit;
using SpeechIO;

public class IntroSpeak : MonoBehaviour
{
    private SpeechOut speechOut = new SpeechOut();

    public string text;
    // Start is called before the first frame update
    void Start()
    {
        speechOut.SetLanguage(SpeechBase.LANGUAGE.GERMAN);
        speechOut.Speak(text);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
