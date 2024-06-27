using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpeechIO;
using UnityEngine;

namespace DualPantoToolkit{
public class GameManager : MonoBehaviour
{
    public int mode=0; 
    public SpeechOut speechIO= new SpeechOut();
    public GameObject PL;

    LowerHandle l;
    UpperHandle u;
    public GameObject PU;
    void Start()
    {
        l = GameObject.Find("Panto").GetComponent<LowerHandle>();
        u = GameObject.Find("Panto").GetComponent<UpperHandle>();
        StartCoroutine(DelayedStart());
    }
    IEnumerator DelayedStart(){
        yield return new WaitForSeconds(4);
        PL=GameObject.Find("ItHandle");
        PU=GameObject.Find("MeHandle");
        l.MoveToPosition(GameObject.Find("Goal").transform.position - PL.transform.position, 2, false);
        u.MoveToPosition(GameObject.Find("Ball").transform.position - PL.transform.position, 2, false);
        speechIO.Speak("Shoot the ball");
        this.GetComponent<Wiggle>().wiggle_wiggle_wiggle(true);
        yield return new WaitForSeconds(4);
        speechIO.Speak("in the hole");
        this.GetComponent<Wiggle>(). wiggle_wiggle_wiggle(false);
        yield return new WaitForSeconds(4);
        speechIO.Speak("by drawing.");
        speechIO.Speak("Confirm by rotating lower handle");
        yield return new WaitForSeconds(2);
        this.GetComponent<Wiggle>().wiggle_wiggle_wiggle(false);
        yield return new WaitForSeconds(4);
    }
}

}