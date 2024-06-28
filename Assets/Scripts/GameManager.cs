using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using SpeechIO;
using UnityEngine;

namespace DualPantoToolkit{
public class GameManager : MonoBehaviour
{
    public int mode=0; //0.. Explanation, 1.. Exploration, 2.. Aiming, 3.. Shooting (Watch)
    public SpeechOut speechIO= new SpeechOut();
    public GameObject PL;
    public GameObject PU;
    public GameObject forcefield;
    //Referenzen
    private Wiggle wiggle;
    private LowerHandle l;
    private UpperHandle u;
    private GameObject LeftBound;
    private GameObject RightBound;
    private GameObject TopBound;
    private GameObject BottomBound;
    //Privates
    private bool shooting = false;
    private float rotation_handle;
    private int rotation_lower_threshold=20;
    private int rotation_upper_threshold=200;
    void Start()
    {
        wiggle=this.GetComponent<Wiggle>();
        l = GameObject.Find("Panto").GetComponent<LowerHandle>();
        u = GameObject.Find("Panto").GetComponent<UpperHandle>();
        LeftBound=GameObject.Find("LeftBound");
        RightBound=GameObject.Find("RightBound");
        TopBound=GameObject.Find("UpperBound");
        BottomBound=GameObject.Find("LowerBound");
        PL=GameObject.Find("ItHandle");
        PU=GameObject.Find("MeHandle");
        rotation_handle = 0f;
        StartCoroutine(DelayedStart());
    }
    void Update()
    {
        if(mode==2&&Math.Abs(rotation_handle - l.GetRotation()) > rotation_lower_threshold && Math.Abs(rotation_handle - l.GetRotation()) < rotation_upper_threshold)
        {
            shooting = true;
            Debug.Log("Shshshshoooting");
        }
        rotation_handle = l.GetRotation();
    }

    IEnumerator DelayedStart(){
        LeftBound.SetActive(false);
        RightBound.SetActive(false);
        TopBound.SetActive(false);
        BottomBound.SetActive(false);
        //Initial Position
        speechIO.Speak("Grab both handles to start the game");
        l.MoveToPosition(new Vector3(0,0,-5), 0.5f, true);
        yield return new WaitForSeconds(2);
        u.MoveToPosition(new Vector3(0,0,-5), 0.5f, true);
        yield return new WaitForSeconds(3);
        //tutorial explanation
        l.MoveToPosition(GameObject.Find("Goal").transform.position - PL.transform.position, 0.5f, true);
        yield return new WaitForSeconds(2);
        u.MoveToPosition(GameObject.Find("Ball").transform.position - PU.transform.position, 0.5f, true);
        yield return new WaitForSeconds(2);
        speechIO.Speak("Shoot the ball");
        wiggle.wiggle_wiggle_wiggle(true);
        yield return new WaitForSeconds(5);
        speechIO.Speak("in the hole");
        wiggle.wiggle_wiggle_wiggle(false);
        yield return new WaitForSeconds(5);
        speechIO.Speak("by drawing. Confirm by rotating lower handle");
        yield return new WaitForSeconds(5);
        wiggle.wiggle_wiggle_wiggle(false);
        //Ready for aiming
        l.MoveToPosition(GameObject.Find("Goal").transform.position - PL.transform.position, 0.5f, false);
        yield return new WaitForSeconds(3);
        Instantiate(forcefield,GameObject.Find("Ball").transform.position,Quaternion.identity);
        mode=2;
        yield return new WaitForSeconds(3);
        LeftBound.SetActive(true);
        RightBound.SetActive(true);
        TopBound.SetActive(true);
        BottomBound.SetActive(true);
    }
}
}