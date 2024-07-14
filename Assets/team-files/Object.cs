using System;
using System.Collections;
using System.Collections.Generic;
using DualPantoToolkit;
using UnityEngine;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;
using SpeechIO;

public class Object : MonoBehaviour
{
    private SoundManager sM;
    private SpeechOut speechOut;

    public GameObject level;

    public string name;
    public string description;
    public int id;
    private bool soundLocked = false;
    
    void Start()
    {
        speechOut = new SpeechOut();
        sM = GameObject.FindObjectsOfType<SoundManager>()[0];
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "ItHandle") 
        {
            sM.startLoop();
            if (!soundLocked) {
                speechOut.Speak(name);
                soundLocked = true;
                Invoke("unlockSound", 2);
            }
            

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
        if (col.gameObject.tag == "ItHandle")
        {
            sM.stopLoop();
        }

        if (col.gameObject.tag == "MeHandle")
        {
           GameObject.FindObjectsOfType<ObjectHandler>()[0].resetHoveredObject(gameObject);
        }
    }

    void unlockSound()
    {
        soundLocked = false;
    }

    void OnDestroy()
    {
        sM.stopLoop();
    }
}
