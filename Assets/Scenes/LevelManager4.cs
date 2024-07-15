using System;
using System.Collections;
using System.Collections.Generic;
using DualPantoToolkit;
//
using UnityEngine;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;
using SpeechIO;

public class LevelManager4 : MonoBehaviour
{
    public GameObject hitBallIntro;
    public GameObject enemyBallIntro;
    public GameObject goal1Intro;
    public GameObject goal2Intro;
    public GameObject goal11Intro;
    public GameObject goal22Intro;
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
    await _speechOut.Speak("Welcome to Level 4");
    _lowerHandle = GetComponent<LowerHandle>();
    _lowerHandle = GameObject.Find("Panto").GetComponent<LowerHandle>();
    await _speechOut.Speak("These are the four corners of the playing field");

    await _lowerHandle.MoveToPosition(goal11Intro.transform.position);
    await Task.Delay(500);
    await _lowerHandle.MoveToPosition(goal22Intro.transform.position);
    await Task.Delay(500);
    await _lowerHandle.MoveToPosition(goal3Intro.transform.position);
    await Task.Delay(500);
    await _lowerHandle.MoveToPosition(goal4Intro.transform.position); 
    await Task.Delay(1000);
    _speechOut.Speak("Here is one goal");
    await _lowerHandle.MoveToPosition(goal1Intro.transform.position);
    await Task.Delay(1500);
    _speechOut.Speak("Here is another goal");
    await _lowerHandle.MoveToPosition(goal2Intro.transform.position);
    await Task.Delay(1000);
    _speechOut.Speak("Here is the enemy, you should not touch");
    await _lowerHandle.MoveToPosition(enemyBallIntro.transform.position);  
    await Task.Delay(1500);
    _speechOut.Speak("Put this ball into the lower goal");
    await _lowerHandle.MoveToPosition(hitBallIntro.transform.position);
    _lowerHandle.SwitchTo(hitBall, 10f);

}
}
