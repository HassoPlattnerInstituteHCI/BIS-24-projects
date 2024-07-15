using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpeechIO;

public class Level2 : MonoBehaviour
{
    private GameObject[] objects;

    private bool finished = false;

    public GameObject levelManagerObject;

    private ObjectSelector oS;

    private SpeechOut speechOut;

    private string name1 = "";

    private string name2 = "";

    void Start()
    {
        speechOut = new SpeechOut();

        oS = GameObject.FindObjectsOfType<ObjectSelector>()[0];
        oS.objectsSelectable = true;
        oS.objectsPlaceable = true;
        oS.wallPlaceable = false;
        oS.removeToolActivated = false;
        oS.doorToolActivated = false;

        // speechOut.Speak("Level 2... Turn the lower handle to select objects... Place three different objects by turning the upper handle.");
        oS.soundLocked = true;
        speechOut.Speak("Level 2 . Mit dem unteren Griff kannst du mit leichtem Drehen durch eine Liste an Objekten iterieren. Also Ojekte auswählen.");
        Invoke("unlockSound", 10);
        Invoke("saySecondPart", 20);
    }

    public void objectPlaced(string name)
    {
        if (name1 == "")
        {
            name1 = name;
        } else if (name2 == "")
        {
            name2 = name;
        }
        else if (!finished && name1 != name && name2 != name)
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

    private void saySecondPart()
    {
        speechOut.Speak("Bewege den oberen Griff an eine Position im Raum und platziere dort das ausgewählte Objekt durch leichtes drehen des oberen Griffs.");
    }
}
