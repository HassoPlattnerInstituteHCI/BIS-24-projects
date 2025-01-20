using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    // Start is called before the first frame update
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
