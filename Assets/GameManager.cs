using System;
using System.Collections;
using System.Collections.Generic;
using DualPantoToolkit;
using UnityEditor.VersionControl;
using UnityEngine;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;
using SpeechIO;

public class GameManager : MonoBehaviour
{
    public GameObject hitBallPosition;
    public GameObject enemyBallPosition;
    public GameObject goalPosition;

    private LowerHandle _lowerHandle;
    private SpeechOut _speechOut;

    private void Awake(){
        _speechOut = new SpeechOut();
    }

async void Start(){
    await _speechOut.Speak("Welcome");
    _lowerHandle = GetComponent<LowerHandle>();
    _lowerHandle = GameObject.Find("Panto").GetComponent<LowerHandle>();
    await _speechOut.Speak("Here is the goal");
    await _lowerHandle.MoveToPosition(goalPosition.transform.position);
    await _speechOut.Speak("Here is the hitball");
    await _lowerHandle.MoveToPosition(hitBallPosition.transform.position);
    await _speechOut.Speak("Here is the enemy, you should not touch");
    await _lowerHandle.MoveToPosition(enemyBallPosition.transform.position);  
    _lowerHandle.Free();  
}
}
