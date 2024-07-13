using System.Collections;
using System;
using System.Collections.Generic;
using SpeechIO;
using UnityEngine;

using DualPantoToolkit;
public class GameManager2 : MonoBehaviour
{
    public static int mode=0; //0.. Explanation, 1.. Exploration, 2.. Aiming, 3.. Shooting (Watch)
    public static SpeechOut speechIO= new SpeechOut();
    public GameObject forcefield;
    //Referenzen
    private GameObject PL;
    private GameObject PU;
    private Wiggle wiggle;
    private LowerHandle l;
    private UpperHandle u;
    private GameObject Ball;
    private GameObject Goal;
    PantoCollider[] pantoColliders;
    //Privates
    private float rotation_handle;
    private int rotation_lower_threshold=20;
    private int rotation_upper_threshold=200;
    private float k_factor=20f;
    void Start()
    {
        wiggle=this.GetComponent<Wiggle>();
        l = GameObject.Find("Panto").GetComponent<LowerHandle>();
        u = GameObject.Find("Panto").GetComponent<UpperHandle>();
        PL=GameObject.Find("ItHandle");
        PU=GameObject.Find("MeHandle");
        Ball=GameObject.Find("Ball");
        Goal=GameObject.Find("Goal");
        rotation_handle = 0f;
        Physics.IgnoreCollision(GameObject.Find("MeHandleGodObject").GetComponent<Collider>(),Ball.GetComponent<Collider>());
        Physics.IgnoreCollision(GameObject.Find("ItHandleGodObject").GetComponent<Collider>(),Ball.GetComponent<Collider>());
        StartCoroutine(DelayedStart());
    }
    void EnableWalls() {
        GameObject r=GameObject.FindGameObjectsWithTag("ObstacleRenderer")[0];
        r.AddComponent<ActivateObstaclesSphere>();

    }
    void DisableWalls() 
    {
        GameObject r=GameObject.FindGameObjectsWithTag("ObstacleRenderer")[0];
        Destroy(r.GetComponent<ActivateObstaclesSphere>());
        pantoColliders = GameObject.FindObjectsOfType<PantoCollider>();
        foreach (PantoCollider collider in pantoColliders) {
            collider.Disable();
        }
    }

    void rUpdate()
    {
        if(Math.Abs(rotation_handle - u.GetRotation()) > rotation_lower_threshold && Math.Abs(rotation_handle - u.GetRotation()) < rotation_upper_threshold)
        {
            if (mode==1) StartCoroutine(aiming());
            if(mode==2)  StartCoroutine(shooting());
            else Debug.Log("Me-handle rotation detected without purpose");
        }
        rotation_handle = u.GetRotation();
    }


    IEnumerator DelayedStart(){
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
    IEnumerator explore(){
        //Exploration
        mode=1;
        EnableWalls();
        u.Free();
        yield return new WaitForSeconds(1);
    }
    IEnumerator aiming(){
        //Ready for aiming
        mode=2;
        l.MoveToPosition(Goal.transform.position, 0.5f, false);
        u.MoveToPosition(Ball.transform.position, 0.5f, true);
        yield return new WaitForSeconds(3);
        //Issue: ForceField does not work on the UpperHandle
        GameObject t= Instantiate(forcefield,Ball.transform.position,Quaternion.identity);
        t.name="LinearForceField";
        GameObject.Find("LinearForceField").GetComponent<CenterForceField>().active=true;
    }
    IEnumerator shooting(){
        mode=3;
        GameObject.Find("LinearForceField").SetActive(false);
        //Shooting
        EnableWalls();
        u.SwitchTo(Ball,2f);
        Shooting.shoot((Ball.transform.position-PU.transform.position)*k_factor);
        yield return new WaitForSeconds(3);
    }
}