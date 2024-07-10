using System;
using System.Collections;
using System.Collections.Generic;
using DualPantoToolkit;
using UnityEditor.VersionControl;
using UnityEngine;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;
using SpeechIO;

public class Level3Manager : MonoBehaviour
{
    public GameObject hitBallIntro;
    public GameObject enemyBallIntro;
    public GameObject goalIntro;
    private GameObject hitBall;

    private LowerHandle _lowerHandle;
    private SpeechOut _speechOut;

    private void Awake(){
        _speechOut = new SpeechOut();
         hitBall = GameObject.FindWithTag("Hitball");
    }

async void Start(){
    await _speechOut.Speak("Welcome to Level 3");
    _lowerHandle = GetComponent<LowerHandle>();
    _lowerHandle = GameObject.Find("Panto").GetComponent<LowerHandle>();
    _speechOut.Speak("Here is the goal");
    await _lowerHandle.MoveToPosition(goalIntro.transform.position);
    await Task.Delay(1000);
    _speechOut.Speak("Here is the enemy, you should not touch");
    await _lowerHandle.MoveToPosition(enemyBallIntro.transform.position);
    await Task.Delay(1000);
    _speechOut.Speak("Here is the ball to put in the hole");
    await _lowerHandle.MoveToPosition(hitBallIntro.transform.position); 
    _lowerHandle.SwitchTo(hitBall, 10f);       
}
}
