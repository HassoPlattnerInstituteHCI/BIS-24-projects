using System;
using System.Collections;
using System.Collections.Generic;
using DualPantoToolkit;
using UnityEditor.VersionControl;
using UnityEngine;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;
using SpeechIO;

public class Level5Manager : MonoBehaviour
{
    public GameObject hitBallIntro;
    public GameObject goal1Intro;
    public GameObject goal2Intro;
    public GameObject goal3Intro;
    public GameObject goal4Intro;

    private GameObject hitBall;
    private LowerHandle _lowerHandle;
    private SpeechOut _speechOut;

    private void Awake(){
        _speechOut = new SpeechOut();
        hitBall = GameObject.FindWithTag("Hitball");
    }

async void Start(){
    await _speechOut.Speak("Welcome to Level 5");
    _lowerHandle = GetComponent<LowerHandle>();
    _lowerHandle = GameObject.Find("Panto").GetComponent<LowerHandle>();
    await _speechOut.Speak("Here is the first goal");
    await _lowerHandle.MoveToPosition(goal1Intro.transform.position);
    await _speechOut.Speak("Here is the second goal");
    await _lowerHandle.MoveToPosition(goal2Intro.transform.position);
    await _speechOut.Speak("Here is the third goal");
    await _lowerHandle.MoveToPosition(goal3Intro.transform.position);
    await _speechOut.Speak("Here is the fourth goal");
    await _lowerHandle.MoveToPosition(goal4Intro.transform.position);  
    await _speechOut.Speak("Now hit all goals with this ball");
    _lowerHandle.SwitchTo(hitBall, 10f);  
}
}
