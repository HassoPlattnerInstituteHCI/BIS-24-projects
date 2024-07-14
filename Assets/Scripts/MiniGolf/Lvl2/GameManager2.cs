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
        if(Input.GetKey("s")) //Math.Abs(rotation_handle - u.GetRotation()) > rotation_lower_threshold && Math.Abs(rotation_handle - u.GetRotation()) < rotation_upper_threshold)
        {
            if (mode==1) StartCoroutine(aiming());
            if(mode==2)  StartCoroutine(shooting());
            //else Debug.Log("Me-handle rotation detected without purpose");
        }
        rotation_handle = u.GetRotation();
        if(mode == 4 && Ball.GetComponent<Rigidbody>().velocity.magnitude <0.01)
        {
            Ball.GetComponent<Rigidbody>().velocity=new Vector3(0,0,0);
            Ball.GetComponent<Rigidbody>().angularVelocity=new Vector3(0,0,0);
            StartCoroutine(aiming());
        }
    }
    public override IEnumerator DelayedStart(){
        DisableWalls();
        yield return new WaitForSeconds(2);
        //Init handles
        speechIO.Speak("Grab both handles to start the game");
        l.MoveToPosition(new Vector3(0,0,-5), 0.5f, true);
        yield return new WaitForSeconds(2);
        u.MoveToPosition(new Vector3(0,0,-5), 0.5f, true);
        yield return new WaitForSeconds(2);
        //Initial Position
        speechIO.Speak("You now entered level 2. Explore the map to prepare your shooting, exit exploration by rotating upper handle");
        yield return new WaitForSeconds(3);
        //Preparation game
        l.MoveToPosition(Goal.transform.position, 0.8f, false);
        yield return new WaitForSeconds(3);
        u.MoveToPosition(Ball.transform.position, 0.8f, false);
        yield return new WaitForSeconds(6);
        StartCoroutine(explore());
    }
}