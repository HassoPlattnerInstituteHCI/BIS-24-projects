using System;
using System.Collections;
using System.Collections.Generic;
using DualPantoToolkit;
using UnityEditor.VersionControl;
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
    await _speechOut.Speak("Here is one goal");
    await _lowerHandle.MoveToPosition(goal1Intro.transform.position);
    await _speechOut.Speak("Here is another goal");
    await _lowerHandle.MoveToPosition(goal2Intro.transform.position);
    await _speechOut.Speak("Here is the hitball");
    await _lowerHandle.MoveToPosition(hitBallIntro.transform.position);
    await _speechOut.Speak("Here is the enemy, you should not touch");
    await _lowerHandle.MoveToPosition(enemyBallIntro.transform.position);  
    _lowerHandle.SwitchTo(hitBall, 10f);  
}
}
