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

    private GameObject door;



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
        speechOut.Speak("Find the Key");
        Invoke("unlockSound", 10);
    }

    public void objectPlaced(string name, Vector3 position)
    {
    
        if (name == "key" && door!= null && position == door.transform.position)
        {
            finished = true;
            Invoke("levelFinished", 1);
        }

    }

    public void foundKey()
    {
        speechOut.Speak("You found the key. Now select by turning the lower handle.");
    }

    // public void foundObject(int objectId)
    // {
    //     if (finished) return;

    //     Debug.Log("found object");
    //     objectsFound[objectId] = true;

    //     foreach (bool objectFound in objectsFound)
    //     {
    //         if (!objectFound)
    //         {
    //             return;
    //         }
    //     }
    //     finished = true;
    //     Invoke("levelFinished", 1);
    // }
    private void levelFinished()
    {
         speechOut.Speak("Well done! Move the handles in the middle to continue to the next level");
        //speechOut.Speak("Sehr gut! Bewege beide Griffe in die Mitte um in das nächste Level zu kommen.");
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


}