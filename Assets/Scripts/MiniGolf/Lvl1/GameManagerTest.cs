using System.Collections;
using System;
using System.Collections.Generic;
using SpeechIO;
using UnityEngine;

//******************************************************************
//
// Better GameManager1 version with async 
//
//******************************************************************

namespace DualPantoToolkit{
public class GameManagerTest : MonoBehaviour
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
    private GameObject[] walls;
    //Privates
    private float rotation_handle;
    private int rotation_lower_threshold=20;
    private int rotation_upper_threshold=200;
    private float k_factor=20f;
    async void Start()
    {
        //Initialization
        wiggle=this.GetComponent<Wiggle>();
        l = GameObject.Find("Panto").GetComponent<LowerHandle>();
        u = GameObject.Find("Panto").GetComponent<UpperHandle>();
        walls=GameObject.FindGameObjectsWithTag("Wall");
        PL=GameObject.Find("ItHandle");
        PU=GameObject.Find("MeHandle");
        rotation_handle = 0f;
        Physics.IgnoreCollision(GameObject.Find("MeHandleGodObject").GetComponent<Collider>(),GameObject.Find("Ball").GetComponent<Collider>());
        Physics.IgnoreCollision(GameObject.Find("ItHandleGodObject").GetComponent<Collider>(),GameObject.Find("Ball").GetComponent<Collider>());
        foreach(GameObject w in walls){ w.SetActive(false);}
        //Handle Preparation
        speechIO.Speak("Grab both handles to start the game");
        l.MoveToPosition(new Vector3(2,0,-5), 0.5f, true);
        u.MoveToPosition(new Vector3(-2,0,-5), 0.5f, true);
        //Tutorial explanation
        await l.MoveToPosition(GameObject.Find("Goal").transform.position, 0.8f, true);
        await u.MoveToPosition(GameObject.Find("Ball").transform.position, 0.8f, true);
        speechIO.Speak("Shoot the ball");
        wiggle.wiggle_wiggle_wiggle(true);
        await speechIO.Speak("in the hole");
        wiggle.wiggle_wiggle_wiggle(false);
        await speechIO.Speak("by drawing upper handle like a bow. Confirm by rotating upper handle");
        wiggle.wiggle_wiggle_wiggle(false);
        //Ready for aiming
        await l.MoveToPosition(GameObject.Find("Goal").transform.position, 0.5f, false);
        u.MoveToPosition(GameObject.Find("Ball").transform.position, 0.5f, true);
        //Issue: ForceField does not work on the UpperHandle
        Instantiate(forcefield,GameObject.Find("Ball").transform.position,Quaternion.identity);
        GameObject.Find("LinearForceField(Clone)").GetComponent<CenterForceField>().active=true;
        mode=2;
    }
    void Update()
    {
        if(mode==2&&Math.Abs(rotation_handle - u.GetRotation()) > rotation_lower_threshold && Math.Abs(rotation_handle - u.GetRotation()) < rotation_upper_threshold)
        {
            StartCoroutine(apply_shooting());
        }
        rotation_handle = u.GetRotation();
    }
    IEnumerator apply_shooting(){
        mode=3;
        GameObject.Find("LinearForceField(Clone)").SetActive(false);
        //Shooting
        foreach(GameObject w in walls){ w.SetActive(true);}
        u.SwitchTo(GameObject.Find("Ball"),2f);
        Shooting.shoot((GameObject.Find("Ball").transform.position-PU.transform.position)*k_factor);
        yield return new WaitForSeconds(3);
    }
}
}