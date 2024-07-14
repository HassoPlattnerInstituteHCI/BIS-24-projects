using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpeechIO;

public class Level3 : MonoBehaviour
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
        oS.removeToolActivated = false;
        oS.doorToolActivated = false;

        // speechOut.Speak("Level 3... You can now place walls... Do so by placing both ends... Now finish the room.");
        oS.soundLocked = true;
        speechOut.Speak("Level 3 . Wähle das Wandobjekt aus. Platziere eine Wand, indem du beide Enden der Wand mit dem oberen Griff platzierst.");
        Invoke("unlockSound", 7);
    }

    void Update()
    {
        
    }

    public void wallPlaced()
    {
        if (!finished)
        {
            finished = true;
            Invoke("levelFinished", 1);
        }
    }

    private void levelFinished()
    {
        // speechOut.Speak("Well done! Move the handles in the middle to continue to the next level");
        speechOut.Speak("Sehr gut! Bewege beide Griffe in die Mitte um in das nächste Level zu kommen.");
        Invoke("finish", 5);
    }

    private void finish()
    {
        GameObject.FindGameObjectsWithTag("PlayArea")[0].transform.position = new Vector3(0,0,-10);
    }

    private void unlockSound()
    {
        oS.soundLocked = false;
    }
}
