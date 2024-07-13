using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DualPantoToolkit;
using SpeechIO;

public class IT : MonoBehaviour {

    SpeechOut sp;
    bool player;
    
    void Start() {
        // OPTIONAL TODO: 
        // speechIn = new SpeechIn(onRecognized); 	
        // speechIn.StartListening();
        // SpeedUpListener();
        sp = new SpeechOut();
        player = true;
        //handle = (PantoHandle)GameObject.Find("Panto").GetComponent<LowerHandle>();
        
        //Reset();
    }

    void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Black") && ME.player) {
            sp.Speak("Black");
        } else if(other.CompareTag("White") && !ME.player) {
            sp.Speak("White");
        }
    }
    
}