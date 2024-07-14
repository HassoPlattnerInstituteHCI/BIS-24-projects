using System.Collections;
using System;
using System.Collections.Generic;
using SpeechIO;
using UnityEngine;

using DualPantoToolkit;
public class GameManager2 : GameManagerClass
{
    public void Update()
    {
        Debug.Log("mode: "+mode);
        if(!isbusy&&Input.GetKey("s")) //Math.Abs(rotation_handle - u.GetRotation()) > rotation_lower_threshold && Math.Abs(rotation_handle - u.GetRotation()) < rotation_upper_threshold)
        {
            if (mode==2) {
                StartCoroutine(shooting());
                isbusy=true;
            }
            if (mode==1) {
                StartCoroutine(aiming());
                isbusy=true;
            }
            //else Debug.Log("Me-handle rotation detected without purpose");
        }
        rotation_handle = u.GetRotation();
        Debug.Log(mode);
    }
    void FixedUpdate()
    {
        if(mode == 4 && Ball.GetComponent<Rigidbody>().velocity.magnitude <0.1)
        {
            Ball.GetComponent<Rigidbody>().velocity=new Vector3(0,0,0);
            Ball.GetComponent<Rigidbody>().angularVelocity=new Vector3(0,0,0);
            StartCoroutine(aiming());
        }
    }
    public override IEnumerator DelayedStart(){
        isbusy=true;
        //Initial Position
        speechIO.Speak("Explore the map to prepare your shooting with the upper handle, exit exploration by rotating upper handle or pressing s");
        //Preparation game
        l.MoveToPosition(Goal.transform.position, 0.8f, false);
        yield return new WaitForSeconds(3);
        u.MoveToPosition(Ball.transform.position, 0.8f, false);
        yield return new WaitForSeconds(6);
        StartCoroutine(explore());
    }
}