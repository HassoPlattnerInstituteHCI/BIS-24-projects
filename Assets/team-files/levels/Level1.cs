using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpeechIO;

public class Level1 : MonoBehaviour
{
    public LevelManager levelManager;

    public GameObject door;
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
        if (door != null)
        {
            Vector3 doorPosition = door.transform.position;
            Debug.Log("Door position: " + doorPosition);
        }
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

    void Update()
    {
    // Überprüft, ob alle Objekte gefunden wurden
    foreach (bool objectFound in objectsFound)
    {
        if (!objectFound)
        {
            return; // Wenn ein Objekt nicht gefunden wurde, wird die Methode beendet
        }
    }
    finished = true; // Setzt den Status auf abgeschlossen
    Invoke("levelFinished", 1); // Ruft die Methode levelFinished nach 1 Sekunde auf
    }


    private void levelFinished()
    {
        // speechOut.Speak("Well done! You have found all Objects. Move the handles in the middle to continue to the next level");
        speechOut.Speak("You found the door. Oh no! The door is locked. Move the handles to the middle to continue");
        Invoke("finish", 6);
    }

    private void finish()
    {
        GameObject.FindGameObjectsWithTag("PlayArea")[0].transform.position = new Vector3(0, 0, -10);
    }


    private void loadNextLevel()
    {
    // Hier wird der Übergang zu Level 2 initiiert
    // Beispiel: SceneManager.LoadScene("Level2");
    levelManager.StartNextLevel();
    }
}
