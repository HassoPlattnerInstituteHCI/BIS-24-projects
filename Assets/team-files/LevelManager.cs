using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpeechIO;

public class LevelManager : MonoBehaviour
{
    public GameObject[] levels;

    public GameObject currentLevel;

    private ObjectHandler objectHandler;

    private SpeechOut speechOut;

    public GameObject panto;

    private bool tutorialCompleted = false;

    private int nextLevelId = 0;

    void Start()
    {
        objectHandler = GameObject.FindObjectsOfType<ObjectHandler>()[0];
        speechOut = new SpeechOut();
    }

    public void StartNextLevel()
    {
        if (!tutorialCompleted) {
            Debug.Log("Starting next level...");
            LoadLevel(nextLevelId++);
        }
    }

    public void LoadLevel(int levelId)
    {
        // remove current level + destroy all walls in dp
        if (currentLevel)
        {
            panto.GetComponent<GameManager>().DestroyObstacle();
            Destroy(currentLevel);
            objectHandler.destroyAllObjectsWithTag("PlacedObject");
            objectHandler.destroyAllObjectsWithTag("Wall");
        }
        
        currentLevel = Instantiate(levels[levelId], new Vector3(0,0,0), Quaternion.identity);
        // gamemanager play intro
        Invoke("x", 0.1f);
    }

    private void x() 
    {
        panto.GetComponent<GameManager>().StartGame();
    }

    public void reset()
    {
        nextLevelId = 0;

        GameObject.FindGameObjectsWithTag("PlayArea")[0].transform.position = new Vector3(0,0,-10);
        
        speechOut.Speak("Move both handles in the middle to start the intro.");

        panto.GetComponent<GameManager>().DestroyObstacle();
        Destroy(currentLevel);
        objectHandler.destroyAllObjectsWithTag("PlacedObject");
        objectHandler.destroyAllObjectsWithTag("Wall");
    }
}
