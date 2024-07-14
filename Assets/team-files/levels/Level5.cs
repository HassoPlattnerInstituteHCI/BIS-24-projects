using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpeechIO;

public class Level5 : MonoBehaviour
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
        oS.doorToolActivated = true;

        speechOut.Speak("Level 5... Place a door in the left wall to get to the next room.");
    }

    public void levelFinished()
    {
        speechOut.Speak("Well done! Move the handles in the middle to continue to the next level");
        Invoke("finish", 5);
    }

    private void finish()
    {
        GameObject.FindGameObjectsWithTag("PlayArea")[0].transform.position = new Vector3(0,0,-10);
    }
}
