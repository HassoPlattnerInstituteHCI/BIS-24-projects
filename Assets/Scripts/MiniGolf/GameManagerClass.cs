using System.Collections;
using System;
using System.Collections.Generic;
using SpeechIO;
using UnityEngine.SceneManagement;
using UnityEngine;

using DualPantoToolkit;
public abstract class GameManagerClass : MonoBehaviour
{
    //public Variables
    public int currentlvl;
    public int mode=0; //0.. Explanation, 1.. Exploration, 2.. Aiming, 3.. Shooting, 4.. Watching, 5.. Level completed
    public int amount_hits = 0;
    public static SpeechOut speechIO= new SpeechOut();
    public GameObject forcefield;
    public Wiggle wiggle;
    //Referenzen
    protected GameObject PL;
    protected GameObject PU;
    protected LowerHandle l;
    protected UpperHandle u;
    protected GameObject Ball;
    protected GameObject Goal;
    PantoCollider[] pantoColliders;
    //Internal variables
    protected string scene_name = "MiniGolfLvl";
    protected bool isbusy=false;
    protected float rotation_handle;
    protected int rotation_lower_threshold=20;
    protected int rotation_upper_threshold=200;
    protected float k_factor=15f;
    public void Awake()
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
    }
    public void Start(){
        StartCoroutine(Initialization());
    }
    public void DisableWalls() 
    {
        GameObject r=GameObject.FindGameObjectsWithTag("ObstacleRenderer")[0];
        Destroy(r.GetComponent<ActivateObstaclesSphere>());
        pantoColliders = GameObject.FindObjectsOfType<PantoCollider>();
        foreach (PantoCollider collider in pantoColliders) {
            collider.Disable();
        }
    }
    public IEnumerator Initialization(){
        isbusy=true;
        this.DisableWalls();
        yield return new WaitForSeconds(2);
        //Initial Position
        l.MoveToPosition(new Vector3(0,0,-5), 0.5f, true);
        yield return new WaitForSeconds(3);
        u.MoveToPosition(new Vector3(0,0,-5), 0.5f, true);
        yield return new WaitForSeconds(3);
        l.MoveToPosition(new Vector3(2,0,-5), 0.5f, true);
        yield return new WaitForSeconds(3);
        u.MoveToPosition(new Vector3(-2,0,-5), 0.5f, true);
        yield return new WaitForSeconds(2);
        speechIO.Speak("Grab both handles to start level "+currentlvl);
        yield return new WaitForSeconds(4);
        StartCoroutine(DelayedStart());
    }
    public abstract IEnumerator DelayedStart();
    public void EnableWalls() {
        GameObject r=GameObject.FindGameObjectsWithTag("ObstacleRenderer")[0];
        r.AddComponent<ActivateObstaclesSphere>();
    }
    public IEnumerator explore(){
        //Exploration
        isbusy=true;
        mode=1;
        EnableWalls();
        u.Free();
        yield return new WaitForSeconds(1);
        isbusy=false;
    }
    public IEnumerator aiming(){
        //Aiming
        DisableWalls();
        isbusy=true;
        mode=2;
        //l.MoveToPosition(Goal.transform.position, 0.5f, false);
        foreach(GameObject w in GameObject.FindGameObjectsWithTag("Wall")){
            w.GetComponent<Collider>().isTrigger=false;
        }
        u.Free();
        u.MoveToPosition(Ball.transform.position, 0.5f, true);
        yield return new WaitForSeconds(3);
        //u.Free();
        //Issue: ForceField does not work on the UpperHandle
        GameObject t= Instantiate(forcefield,Ball.transform.position,Quaternion.identity);
        t.name="LinearForceField";
        GameObject.Find("LinearForceField").GetComponent<CenterForceField>().active=true;
        speechIO.Speak("You can now shoot");
        yield return new WaitForSeconds(1);
        isbusy=false;
    }
    public IEnumerator shooting(){
        isbusy=true;
        mode=3;
        GameObject.Find("LinearForceField").SetActive(false);
        amount_hits += 1;
        //Shooting
        u.SwitchTo(Ball,3f);
        Shooting.shoot((Ball.transform.position-PU.transform.position).magnitude*(Ball.transform.position-PU.transform.position)*k_factor);
        EnableWalls();
        yield return new WaitForSeconds(0.5f);
        mode = 4;
    }
    public IEnumerator NextLevel(){
        GameObject.Find("LinearForceField").SetActive(false);
        u.Free();
        l.Free();
        u.MoveToPosition(new Vector3(0,0,-5),0.5f,true);
        yield return new WaitForSeconds(3);
        l.MoveToPosition(new Vector3(0,0,-5),0.5f,true);
        yield return new WaitForSeconds(3);
        u.MoveToPosition(new Vector3(0,0,0),0.5f,true);
        yield return new WaitForSeconds(3);
        l.MoveToPosition(new Vector3(0,0,0),0.5f,true);
        yield return new WaitForSeconds(4);
        DualPantoSync.Reset();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneName:scene_name + (currentlvl+1).ToString());
    }
}