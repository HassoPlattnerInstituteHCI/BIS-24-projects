using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DualPantoToolkit;
using SpeechIO;

public class ME : MonoBehaviour {

    SpeechOut sp;
    bool player;
    Dictionary <string, int> dic = new Dictionary<string, int>();
    
    void Start() {
        // OPTIONAL TODO: 
        // speechIn = new SpeechIn(onRecognized); 	
        // speechIn.StartListening();
        // SpeedUpListener();
        sp = new SpeechOut();
        player = true;
        sp.Speak("Find the pieces on the board with both handles!");
        //handle = (PantoHandle)GameObject.Find("Panto").GetComponent<LowerHandle>();
        foreach(GameObject go in GameObject.FindGameObjectsWithTag("Black")) {
            string name = go.name;
            int pos = name[2] - '0';
            pos--;
            int row = name[1] - '0';
            row--;
            dic[name] = pos + row * 4;
            
        }
        foreach(GameObject go in GameObject.FindGameObjectsWithTag("White")) {
            string name = go.name;
            int pos = name[2] - '0';
            pos--;
            int row = name[1] - '0';
            row--;
            dic[name] = 20 + pos + row * 4;
        }

        //Reset();
    }

    void OnTriggerEnter(Collider other) {
        if(other.CompareTag("White") && player) {
            sp.Speak("White");
        } else if(other.CompareTag("Black") && !player) {
            sp.Speak("Black");
        }
    }
    
}