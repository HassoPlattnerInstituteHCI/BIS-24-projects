using System.Collections;
using System;
using System.Collections.Generic;
using SpeechIO;
using UnityEngine;

using DualPantoToolkit;

public class GameManager : MonoBehaviour
{
    public static int mode=0; //0.. Explanation, 1.. Exploration, 2.. Aiming, 3.. Shooting (Watch)
    public static SpeechOut speechIO= new SpeechOut();
    public GameObject PL;
    public GameObject PU;
    public GameObject forcefield;
    //Referenzen
    private Wiggle wiggle;
    private LowerHandle l;
    private UpperHandle u;
    private GameObject LeftBound;
    private GameObject RightBound;
    private GameObject TopBound;
    private GameObject BottomBound;
    //Privates
    private float rotation_handle;
    private int rotation_lower_threshold=20;
    private int rotation_upper_threshold=200;
    private float k_factor=20f;
    PantoCollider[] pantoColliders;
    void Start()
    {
        wiggle=this.GetComponent<Wiggle>();
        l = GameObject.Find("Panto").GetComponent<LowerHandle>();
        u = GameObject.Find("Panto").GetComponent<UpperHandle>();
        LeftBound=GameObject.Find("LeftBound");
        RightBound=GameObject.Find("RightBound");
        TopBound=GameObject.Find("UpperBound");
        BottomBound=GameObject.Find("LowerBound");
        PL=GameObject.Find("ItHandle");
        PU=GameObject.Find("MeHandle");
        rotation_handle = 0f;
        Physics.IgnoreCollision(GameObject.Find("MeHandleGodObject").GetComponent<Collider>(),GameObject.Find("Ball").GetComponent<Collider>());
        Physics.IgnoreCollision(GameObject.Find("ItHandleGodObject").GetComponent<Collider>(),GameObject.Find("Ball").GetComponent<Collider>());
        StartCoroutine(DelayedStart());
    }
    void Update()
    {
        if(mode==2&&Math.Abs(rotation_handle - u.GetRotation()) > rotation_lower_threshold && Math.Abs(rotation_handle - u.GetRotation()) < rotation_upper_threshold)
        {
            StartCoroutine(apply_shooting());
        }
        rotation_handle = u.GetRotation();
    }

    void DisableWalls() 
    {
        pantoColliders = GameObject.FindObjectsOfType<PantoCollider>();
        foreach (PantoCollider collider in pantoColliders) {
            collider.Disable();
        }
    }
    
    void EnableWalls() {
        pantoColliders = GameObject.FindObjectsOfType<PantoCollider>();
        foreach (PantoCollider collider in pantoColliders) {
            collider.Enable();
        }
    }

    void CreateObstacles() {
        pantoColliders = GameObject.FindObjectsOfType<PantoCollider>();
        foreach (PantoCollider collider in pantoColliders) {
            collider.CreateObstacle();
            // collider.Enable();
        }
    }

    IEnumerator DelayedStart(){
        this.CreateObstacles();
        this.DisableWalls();
        //Initial Position
        speechIO.Speak("Grab both handles to start the game");
        l.MoveToPosition(new Vector3(2,0,-5), 0.5f, true);
        yield return new WaitForSeconds(2);
        u.MoveToPosition(new Vector3(-2,0,-5), 0.5f, true);
        yield return new WaitForSeconds(3);
        //tutorial explanation
        l.MoveToPosition(GameObject.Find("Goal").transform.position, 0.8f, true);
        yield return new WaitForSeconds(3);
        u.MoveToPosition(GameObject.Find("Ball").transform.position, 0.8f, true);
        yield return new WaitForSeconds(3);
        speechIO.Speak("Shoot the ball");
        wiggle.wiggle_wiggle_wiggle(true);
        yield return new WaitForSeconds(3);
        speechIO.Speak("in the hole");
        wiggle.wiggle_wiggle_wiggle(false);
        yield return new WaitForSeconds(2);
        speechIO.Speak("by drawing. Confirm by rotating upper handle");
        yield return new WaitForSeconds(2);
        wiggle.wiggle_wiggle_wiggle(false); 
        //Ready for aiming
        l.MoveToPosition(GameObject.Find("Goal").transform.position, 0.5f, false);
        u.MoveToPosition(GameObject.Find("Ball").transform.position, 0.5f, true);
        yield return new WaitForSeconds(3);
        //Issue: ForceField does not work on the UpperHandle
        Instantiate(forcefield,GameObject.Find("Ball").transform.position,Quaternion.identity);
        GameObject.Find("LinearForceField(Clone)").GetComponent<CenterForceField>().active=true;
        mode=2;
    }
    IEnumerator apply_shooting(){
        mode=3;
        GameObject.Find("LinearForceField(Clone)").SetActive(false);
        //Shooting
        this.EnableWalls();
        u.SwitchTo(GameObject.Find("Ball"),2f);
        Shooting.shoot((GameObject.Find("Ball").transform.position-PU.transform.position)*k_factor);
        yield return new WaitForSeconds(3);
    }
}