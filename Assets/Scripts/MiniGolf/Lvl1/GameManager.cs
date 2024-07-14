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
        if(!isbusy&&mode==2&&Input.GetKey("s")) /* Math.Abs(rotation_handle - u.GetRotation()) > rotation_lower_threshold && Math.Abs(rotation_handle - u.GetRotation()) < rotation_upper_threshold */
        {
            StartCoroutine(shooting());
            Debug.Log("Hlee0");
        }
        rotation_handle = u.GetRotation();
        /*if(mode == 4 && Ball.GetComponent<Rigidbody>().velocity.magnitude==0)
        {
            Ball.GetComponent<Rigidbody>().velocity=Vector3.zero;
            Ball.GetComponent<Rigidbody>().angularVelocity=Vector3.zero;
            StartCoroutine(aiming());
        }*/
    }
    public override IEnumerator DelayedStart(){
        //tutorial explanation
        isbusy=true;
        l.MoveToPosition(Goal.transform.position, 0.8f, false);
        yield return new WaitForSeconds(3);
        u.MoveToPosition(Ball.transform.position, 0.8f, false);
        yield return new WaitForSeconds(3);
        speechIO.Speak("Shoot the ball");
        yield return new WaitForSeconds(0.7f);
        wiggle.wiggle_wiggle_wiggle(true);
        u.MoveToPosition(Ball.transform.position, 0.8f, false);
        yield return new WaitForSeconds(3);
        speechIO.Speak("in the hole, by drawing the upper handle like a bow");
        yield return new WaitForSeconds(0.7f);
        wiggle.wiggle_wiggle_wiggle(false);
        l.MoveToPosition(Goal.transform.position, 0.8f, false);
        yield return new WaitForSeconds(2);
        speechIO.Speak("Confirm to shoot by pressing s."); //" rotating upper handle or "
        yield return new WaitForSeconds(2f);
        wiggle.wiggle_wiggle_wiggle(true);
        u.MoveToPosition(Ball.transform.position, 0.8f, false); 
        //Ready for aiming
        l.MoveToPosition(Goal.transform.position, 0.5f, false);
        u.MoveToPosition(Ball.transform.position, 0.5f, true);
        yield return new WaitForSeconds(1);
        StartCoroutine(aiming());
    }
}