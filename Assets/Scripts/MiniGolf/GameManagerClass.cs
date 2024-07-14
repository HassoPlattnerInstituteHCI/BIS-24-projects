using System.Collections;
using System;
using System.Collections.Generic;
using SpeechIO;
using UnityEngine.SceneManagement;
using UnityEngine;

using DualPantoToolkit;
public abstract class GameManagerClass : MonoBehaviour
{
    public int currentlvl;
    public int mode=0; //0.. Explanation, 1.. Exploration, 2.. Aiming, 3.. Shooting, 4.. Watching, 5.. Level completed
    public int amount_hits = 0;
    public static SpeechOut speechIO= new SpeechOut();
    public GameObject forcefield;
    public Wiggle wiggle;
    
    string name = "MiniGolfLvl";
    //Referenzen
    protected GameObject PL;
    protected GameObject PU;
    protected LowerHandle l;
    protected UpperHandle u;
    protected GameObject Ball;
    protected GameObject Goal;
    PantoCollider[] pantoColliders;
    //protecteds
    protected float rotation_handle;
    protected int rotation_lower_threshold=20;
    protected int rotation_upper_threshold=200;
    protected float k_factor=20f;
    public void Start()
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
    public abstract IEnumerator DelayedStart();
    public void EnableWalls() {
        GameObject r=GameObject.FindGameObjectsWithTag("ObstacleRenderer")[0];
        r.AddComponent<ActivateObstaclesSphere>();
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
    public IEnumerator explore(){
        //Exploration
        mode=1;
        EnableWalls();
        u.Free();
        yield return new WaitForSeconds(1);
    }
    public IEnumerator aiming(){
        //Aiming
        mode=2;
        //l.MoveToPosition(Goal.transform.position, 0.5f, false);
        u.MoveToPosition(Ball.transform.position, 0.5f, true);
        yield return new WaitForSeconds(3);
        //Issue: ForceField does not work on the UpperHandle
        GameObject t= Instantiate(forcefield,Ball.transform.position,Quaternion.identity);
        t.name="LinearForceField";
        GameObject.Find("LinearForceField").GetComponent<CenterForceField>().active=true;
    }
    public IEnumerator shooting(){
        mode=3;
        GameObject.Find("LinearForceField").SetActive(false);
        amount_hits += 1;
        //Shooting
        EnableWalls();
        u.SwitchTo(Ball,4f);
        Shooting.shoot((Ball.transform.position-PU.transform.position).magnitude*(Ball.transform.position-PU.transform.position)*k_factor);
        mode = 4;
        yield return new WaitForSeconds(3);
    }
    public IEnumerator sleep(float i){
        yield return new WaitForSeconds(i);
        u.MoveToPosition(new Vector3(0,0,0),0.5f,true);
        yield return new WaitForSeconds(i);
        l.MoveToPosition(new Vector3(0,0,0),0.5f,true);
        yield return new WaitForSeconds(i);
        SceneManager.LoadScene(sceneName:name + (currentlvl+1).ToString());
    }
    public void NextLevel(){
        u.Free();
        l.Free();
        StartCoroutine(sleep(4f));
    }
}