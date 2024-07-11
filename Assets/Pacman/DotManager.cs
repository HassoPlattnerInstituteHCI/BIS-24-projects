using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpeechIO;

public class DotManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int totalDots = 0;
    SpeechOut speechOut = new SpeechOut();

    void Start()
    {
        GameObject[] dots = GameObject.FindGameObjectsWithTag("PacmanDot");

        totalDots = dots.Length;
        // Loop through the array and print the name of each object
        foreach (GameObject dot in dots)
        {
            Debug.Log("Found object: " + dot.name);
            dot.GetComponent<Dot>().manager = this;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DotEaten() {
        totalDots -= 1;
        // speechOut.Speak("bling");
        if (totalDots == 0) speechOut.Speak("Well done!");
    }

    void OnApplicationQuit()
    {
        speechOut.Stop();
    }
}