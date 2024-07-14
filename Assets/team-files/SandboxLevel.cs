using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpeechIO;

public class SandboxLevel : MonoBehaviour
{
    private SpeechOut speechOut;
    private ObjectSelector oS;

    void Start()
    {
        speechOut = new SpeechOut();

        oS = GameObject.FindObjectsOfType<ObjectSelector>()[0];
        oS.objectsSelectable = true;
        oS.objectsPlaceable = true;
        oS.wallPlaceable = true;
        oS.removeToolActivated = true;
        oS.doorToolActivated = true;

        speechOut.Speak("You have finished the Tutorial... You can now use all tools in this sandbox. If you want to redo the Tutorial, move both handles to the top.");
    }
}
