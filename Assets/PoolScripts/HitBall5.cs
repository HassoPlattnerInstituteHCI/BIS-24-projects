using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DualPantoToolkit;

public class HitBall5 : MonoBehaviour
{
    PantoHandle handle;
    private Rigidbody rb;
    private AudioSource audioSource;
    private bool goal1hit=false;
    private bool goal2hit=false;
    private bool goal3hit=false;
    private bool goal4hit=false;
    // Start is called before the first frame update
    void Start()
    {
        handle = GameObject.Find("Panto").GetComponent<LowerHandle>();
        rb = GetComponent<Rigidbody>();
        audioSource = gameObject.GetComponent<AudioSource>();
        Physics.IgnoreCollision(GameObject.Find("ItHandleGodObject").GetComponent<Collider>(), GetComponent<Collider>());
        Physics.IgnoreCollision(GameObject.Find("MeHandleGodObject").GetComponent<Collider>(), GetComponent<Collider>());
        //Physics.IgnoreCollision(GameObject.Find("ItHandleGodObject").GetComponent<Collider>(), GameObject.Find("MeHandleGodObject").GetComponent<Collider>());
       
    }
    // Update is called once per frame
    void Update()
    {
     
    }
    void OnCollisionEnter(Collision other) {
        if (other.collider.CompareTag("Goal1")) {
            goal1hit= true;
            audioSource.Play();
        }
        else if (other.collider.CompareTag("Goal2")) {
            goal2hit= true;
            audioSource.Play();
        }
        else if (other.collider.CompareTag("Goal3")) {
            goal3hit= true;
            audioSource.Play();
        }
        else if (other.collider.CompareTag("Goal4")) {
            goal4hit= true;
            audioSource.Play();
        }
        if(goal1hit&& goal2hit &&goal3hit &&goal4hit){
            audioSource.Play();//play special sound
            audioSource.Play();
        }
    }
}
