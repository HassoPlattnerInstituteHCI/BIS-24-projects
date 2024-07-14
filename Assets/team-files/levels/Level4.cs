using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpeechIO;

public class Level4 : MonoBehaviour
{
    private SpeechOut speechOut;
    private ObjectSelector oS;
    private bool finished = false;
    
    void Start()
    {
        speechOut = new SpeechOut();

        oS = GameObject.FindObjectsOfType<ObjectSelector>()[0];
        oS.objectsSelectable = true;
        oS.objectsPlaceable = true;
        oS.wallPlaceable = true;
        oS.removeToolActivated = true;
        oS.doorToolActivated = false;

        speechOut.Speak("Level 4... Select the remove tool... Then remove all objects and then all walls");
    }

    void Update()
    {
        if (!finished && GameObject.FindGameObjectsWithTag("PlacedObject").Length + GameObject.FindGameObjectsWithTag("Wall").Length <= 0)
        {
            finished = true;
            Invoke("levelFinished", 1);
        }
    }

    private void levelFinished()
    {
        speechOut.Speak("Well done! Move the handles in the middle to continue to the next level");
        Invoke("finish", 5);
    }

    private void finish()
    {
        GameObject.FindGameObjectsWithTag("PlayArea")[0].transform.position = new Vector3(0,0,-10);
    }
}
