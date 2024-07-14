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

    public GameObject level;

    public string name;
    public string description;
    public int id;
    
    void Start()
    {
        speechOut = new SpeechOut();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "ItHandle") 
        {
            speechOut.Speak(name);

            if (level.name == "Level 1(Clone)")
            {
                level.GetComponent<Level1>().foundObject(id);
            }
        }

        if (col.gameObject.tag == "MeHandle")
        {
            GameObject.FindObjectsOfType<ObjectHandler>()[0].setHoveredObject(gameObject);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "MeHandle")
        {
           GameObject.FindObjectsOfType<ObjectHandler>()[0].resetHoveredObject(gameObject);
        }
    }
}
