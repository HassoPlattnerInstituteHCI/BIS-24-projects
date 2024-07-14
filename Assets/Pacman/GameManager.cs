using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    Ghost ghost;
    DotManager dotManager;
    void Start()
    {
        dotManager = GameObject.FindGameObjectsWithTag("PacmanDotManager")[0].GetComponent<DotManager>();
        ghost = GameObject.FindGameObjectsWithTag("PacmanGhost")[0].GetComponent<Ghost>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver() {
        Application.Quit();
        // dotManager.GameOver();
    }
}
