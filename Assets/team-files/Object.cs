using System;
using System.Collections;
using System.Collections.Generic;
using DualPantoToolkit;
using UnityEditor.VersionControl;
using UnityEngine;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;
using SpeechIO;

public class Object : MonoBehaviour
{
    private SpeechOut speechOut;
    public string name;
    public string description;
    
    void Start()
    {
        speechOut = new SpeechOut();
    }

    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ItHandle") {
            speechOut.Speak(name);
        }
    }
}
