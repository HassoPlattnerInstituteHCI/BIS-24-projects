using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DualPantoToolkit;
using SpeechIO;

public class ME : MonoBehaviour {

    SpeechOut sp;
    
    void Start() {
        // OPTIONAL TODO: 
        // speechIn = new SpeechIn(onRecognized); 	
        // speechIn.StartListening();
        // SpeedUpListener();
        sp = new SpeechOut();
        //handle = (PantoHandle)GameObject.Find("Panto").GetComponent<LowerHandle>();
        
        //Reset();
    }

    void OnTriggerEnter(Collider other) {
        if(other.CompareTag("White")) {
            sp.Speak("White");
        }
    }
    
}