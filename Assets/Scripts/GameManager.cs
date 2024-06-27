using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpeechIO;

public class GameManager : MonoBehaviour
{
    public int mode=0; 
    public SpeechOut speechIO= new SpeechOut();
    public GameObject PL;
    public GameObject PU;
    void Start()
    {
        StartCoroutine(DelayedStart());
    }
    IEnumerator DelayedStart(){
        yield return new WaitForSeconds(4);
        PL=GameObject.Find("ItHandle");
        PU=GameObject.Find("MeHandle");
        speechIO.Speak("Shoot the ball");
        this.GetComponent<Wiggle>().wiggle_wiggle_wiggle(true);
        yield return new WaitForSeconds(4);
        speechIO.Speak("in the hole");
        this.GetComponent<Wiggle>(). wiggle_wiggle_wiggle(false);
        yield return new WaitForSeconds(4);
        speechIO.Speak("by drawing.");
        speechIO.Speak("Confirm by rotating lower handle");
        this.GetComponent<Wiggle>().wiggle_wiggle_wiggle(true);
        yield return new WaitForSeconds(4);
    }
}
