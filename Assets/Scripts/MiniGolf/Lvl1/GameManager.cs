using System.Collections;
using System;
using System.Collections.Generic;
using SpeechIO;
using UnityEngine;

//************************************
//
// Tutorial GameManager
//
//************************************

using DualPantoToolkit;

public class GameManager : GameManagerClass
{
    public void Update()
    {
        if(mode==2&&Input.GetKey("s")) /* Math.Abs(rotation_handle - u.GetRotation()) > rotation_lower_threshold && Math.Abs(rotation_handle - u.GetRotation()) < rotation_upper_threshold */
        {
            StartCoroutine(shooting());
        }
        //Debug.Log("Rotation: " + Math.Abs(rotation_handle - u.GetRotation()));
        rotation_handle = u.GetRotation();
        /*if(mode == 4 && Ball.GetComponent<Rigidbody>().velocity.magnitude==0)
        {
            Ball.GetComponent<Rigidbody>().velocity=Vector3.zero;
            Ball.GetComponent<Rigidbody>().angularVelocity=Vector3.zero;
            StartCoroutine(aiming());
        }*/
    }
    public override IEnumerator DelayedStart(){
        this.DisableWalls();
        yield return new WaitForSeconds(2);
        //Initial Position
        l.MoveToPosition(new Vector3(0,0,-5), 0.5f, true);
        yield return new WaitForSeconds(3);
        u.MoveToPosition(new Vector3(0,0,-5), 0.5f, true);
        yield return new WaitForSeconds(3);
        speechIO.Speak("Grab both handles to start the game");
        l.MoveToPosition(new Vector3(2,0,-5), 0.5f, true);
        yield return new WaitForSeconds(4);
        u.MoveToPosition(new Vector3(-2,0,-5), 0.5f, true);
        yield return new WaitForSeconds(4);
        //tutorial explanation
        l.MoveToPosition(Goal.transform.position, 0.8f, false);
        yield return new WaitForSeconds(3);
        u.MoveToPosition(Ball.transform.position, 0.8f, false);
        yield return new WaitForSeconds(3);
        speechIO.Speak("Shoot the ball");
        wiggle.wiggle_wiggle_wiggle(true);
        u.MoveToPosition(Ball.transform.position, 0.8f, false);
        yield return new WaitForSeconds(3);
        speechIO.Speak("in the hole");
        wiggle.wiggle_wiggle_wiggle(false);
        l.MoveToPosition(Goal.transform.position, 0.8f, false);
        yield return new WaitForSeconds(2);
        speechIO.Speak("by drawing. Confirm by rotating upper handle");
        yield return new WaitForSeconds(2);
        wiggle.wiggle_wiggle_wiggle(true);
        u.MoveToPosition(Ball.transform.position, 0.8f, false); 
        //Ready for aiming
        l.MoveToPosition(Goal.transform.position, 0.5f, false);
        u.MoveToPosition(Ball.transform.position, 0.5f, true);
        yield return new WaitForSeconds(3);
        //Issue: ForceField does not work on the UpperHandle
        StartCoroutine(aiming());
    }
}