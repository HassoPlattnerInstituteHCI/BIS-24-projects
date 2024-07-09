using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DualPantoToolkit;

public class HitBall4 : MonoBehaviour
{
    public float startingSpeed = 3f; // public attributes which can be set in the editor
    public float maxSpeed = 5f;
    private float speed;
    // private Vector3 direction = Vector3.zero;
    PantoHandle handle;
    private Rigidbody rb;
    private GameObject goal;
    private GameObject enemy;
    private AudioSource audioSource;

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
        if (other.collider.CompareTag("Finish")) {
            audioSource.Play();

        }
    }

}