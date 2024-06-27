using System.Collections;
using System;
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
    public GameObject PU;
    public GameObject forcefield;
    float rotation_handle;
    Wiggle wiggle;
    LowerHandle l;
    UpperHandle u;
    GameObject LeftBound;
    GameObject RightBound;
    GameObject TopBound;
    GameObject BottomBound;
    bool shooting = false;
    void Start()
    {
        wiggle=this.GetComponent<Wiggle>();
        l = GameObject.Find("Panto").GetComponent<LowerHandle>();
        u = GameObject.Find("Panto").GetComponent<UpperHandle>();
        LeftBound=GameObject.Find("LeftBound");
        RightBound=GameObject.Find("RightBound");
        TopBound=GameObject.Find("TopBound");
        BottomBound=GameObject.Find("BottomBound");
        PL=GameObject.Find("ItHandle");
        PU=GameObject.Find("MeHandle");
        rotation_handle = 0f;
        StartCoroutine(DelayedStart());
    }
    void Update()
    {
        if(Math.Abs(rotation_handle - l.GetRotation()) > 50)
        {
            if(Math.Abs(rotation_handle - l.GetRotation()) < 300)
            {
                shooting = true;
            }
        }
        rotation_handle = l.GetRotation();
    }

    IEnumerator DelayedStart(){
        LeftBound.SetActive(false);
        RightBound.SetActive(false);
        TopBound.SetActive(false);
        BottomBound.SetActive(false);
        yield return new WaitForSeconds(3);
        l.MoveToPosition(GameObject.Find("Goal").transform.position - PL.transform.position, 2, false);
        u.MoveToPosition(GameObject.Find("Ball").transform.position - PL.transform.position, 2, false);
        speechIO.Speak("Shoot the ball");
        wiggle.wiggle_wiggle_wiggle(true);
        yield return new WaitForSeconds(4);
        speechIO.Speak("in the hole");
        wiggle.wiggle_wiggle_wiggle(false);
        yield return new WaitForSeconds(4);
        speechIO.Speak("by drawing.");
        speechIO.Speak("Confirm by rotating lower handle");
        yield return new WaitForSeconds(2);
        wiggle.wiggle_wiggle_wiggle(false);
        yield return new WaitForSeconds(3);
        Instantiate(forcefield,new Vector3(0,0,-7.3f),Quaternion.identity);
    }
}
}