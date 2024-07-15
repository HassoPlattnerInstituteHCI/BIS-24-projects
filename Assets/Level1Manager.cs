using System;
using System.Collections;
using System.Collections.Generic;
using DualPantoToolkit;
using UnityEngine;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;
using SpeechIO;

public class Level1Manager : MonoBehaviour
{
    public GameObject hitBallIntro;
    private GameObject hitBall;
    
    private LowerHandle _lowerHandle;
    private SpeechOut _speechOut;

    private void Awake(){
        _speechOut = new SpeechOut();
        hitBall = GameObject.FindWithTag("Hitball");
    }

    async void Start(){
    await _speechOut.Speak("Welcome to Level 1");
    _lowerHandle = GetComponent<LowerHandle>();
    _lowerHandle = GameObject.Find("Panto").GetComponent<LowerHandle>();
    _speechOut.Speak("Hit this Ball with the upper handle");
    await _lowerHandle.MoveToPosition(hitBallIntro.transform.position);
    _lowerHandle.SwitchTo(hitBall, 10f);  
}
}
