using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpeechIO;

public class DotManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int totalDots = 0;
    public int placedDots = 0;
    SpeechOut speechOut = new SpeechOut();
    AudioSource ploeppSource;

    void Start()
    {
        GameObject[] dots = GameObject.FindGameObjectsWithTag("PacmanDot");

        totalDots = dots.Length;
        // Loop through the array and print the name of each object
        foreach (GameObject dot in dots)
        {
            Debug.Log("Found object: " + dot.name);
            dot.GetComponent<Dot>().manager = this;
            ploeppSource = GetComponent<AudioSource>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DotEaten() {
        ploeppSource.PlayOneShot(ploeppSource.clip, 1f);
        totalDots -= 1;
        // speechOut.Speak("bling");
        if (totalDots % 30 == 0) speechOut.Speak($"{totalDots}cookies left!");
        if (totalDots == 10) speechOut.Speak($"{totalDots}");
        if (totalDots == 0) speechOut.Speak("Well done!");
    }

    void OnApplicationQuit()
    {
        speechOut.Stop();
    }

    public void plotDots(Vector3 a, Vector3 b) {
        float length = (a-b).magnitude;
        int count = (int) Mathf.Floor(length / 0.2f);
        placedDots += count;
    }

    public void GameOver() {
        speechOut.Stop();
    }
}