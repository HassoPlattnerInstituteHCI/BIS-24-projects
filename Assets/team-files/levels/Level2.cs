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

    public GameObject door;



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
        speechOut.Speak("Find the Key and turn the lower handle to select.");
        Invoke("unlockSound", 10);
        Invoke("saySecondPart", 20);
    }

    public void objectPlaced(string name, Vector3 position)
    {
    
        if (name == "key" && door!= null && position == door.transform.position)
        {
            finished = true;
            Invoke("levelFinished", 1);
        }

    }

     public void foundObject(string name)
    {
         if (name == "key")
        {
            speechOut.Speak("You found a key, now go back to the door and open it by turning the upper handle.");
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
        GameObject.FindGameObjectsWithTag("PlayArea")[0].transform.position = new Vector3(0, 0, -10);
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