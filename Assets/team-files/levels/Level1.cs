using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpeechIO;

public class Level1 : MonoBehaviour
{
    public LevelManager levelManager;
    private bool[] objectsFound;
    private SpeechOut speechOut;
    private ObjectSelector oS;
    private bool finished = false;

    void Start()
    {
        speechOut = new SpeechOut();

        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();

        objectsFound = new bool[1];
        objectsFound[0] = false;

        oS = GameObject.FindObjectsOfType<ObjectSelector>()[0];
        oS.objectsSelectable = false;
        oS.objectsPlaceable = false;
        oS.wallPlaceable = false;
        oS.removeToolActivated = false;
        oS.doorToolActivated = false;

        // speechOut.Speak("Level 1. Explore the room with the lower handle.");
        speechOut.Speak("Level 1 . You are in a room. Find the door!");
    }

    public void foundObject(int objectId) 
    {
        if (finished) return;

        Debug.Log("found object");
        objectsFound[objectId] = true;

        foreach (bool objectFound in objectsFound)
        {
            if (!objectFound)
            {
                return;
            }
        }
        finished = true;
        Invoke("levelFinished", 1);
    }

    private void levelFinished()
    {
        // speechOut.Speak("Well done! You have found all Objects. Move the handles in the middle to continue to the next level");
        speechOut.Speak("Du hast alle Objekte gefunden. Bewege beide Griffe in die Mitte um das nächste Level zu starten.");
        Invoke("finish", 6);
    }

    private void finish()
    {
        GameObject.FindGameObjectsWithTag("PlayArea")[0].transform.position = new Vector3(0,0,-10);
    }
}
