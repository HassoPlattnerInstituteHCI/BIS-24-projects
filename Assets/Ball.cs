using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DualPantoToolkit;
using SpeechIO;
public class Ball : MonoBehaviour {
    

    public float startingSpeed = 3f; // public attributes which can be set in the editor
    public float maxSpeed = 5f;
    //private PlayerSoundEffect soundEffects;
    private float speed;
    private Vector3 initPosition = new Vector3(-1.15f, 0, -9.93f);
    private Vector3 direction; // modify this to change ball's movement
    private bool isOutOfBounds = false;
    private GameObject go;
    PantoHandle handle;
    SpeechIn speechIn;

    

    private Rigidbody rb;
    // Start is called before the first frame update
    void Start() {
        // OPTIONAL TODO: 
        // speechIn = new SpeechIn(onRecognized); 	
        // speechIn.StartListening();
        // SpeedUpListener();

        handle = (PantoHandle)GameObject.Find("Panto").GetComponent<LowerHandle>();
        //soundEffects = GetComponent<PlayerSoundEffect>();
        rb = GetComponent<Rigidbody>();
        go = GetComponent<GameObject>();
        Reset();
    }
    
    public void OnApplicationQuit()
    {
        // OPTIONAL TODO: 
        // speechIn.StopListening(); 
    }
    
    void onRecognized(string message)
    {
        Debug.Log("[" + this.GetType() + "]: " + message);
    }


    // FixedUpdate is called once per physics update
    void FixedUpdate() {
        Vector3 movement = direction.normalized * 100 * (speed * Time.fixedDeltaTime);
        rb.velocity = movement;
    }

    void Update() {
       if (isOutOfBounds) {
            isOutOfBounds = false;
            Reset();
       }
    }

    void Reset() {
        transform.position = initPosition;
        int initAngle = UnityEngine.Random.Range(-20, 20);
        direction = Quaternion.Euler(0, initAngle, 0) * new Vector3(0, 0, -1);
        speed = startingSpeed;
    }

    Vector3 ComputeReflection(Collision other) {
        Vector3 normal = other.GetContact(0).normal.normalized;
        Vector3 collisionPoint = other.GetContact(0).point;
        
        float hitFactor = (collisionPoint.x - other.transform.position.x) / other.collider.bounds.size.x;
        hitFactor = Mathf.Max(-0.4f, Mathf.Min(0.4f, hitFactor)); // disallow very steep angles
        hitFactor = normal.z < 0 ? hitFactor * (-1): hitFactor;

        Vector3 reflection = Quaternion.Euler(0, hitFactor * 180, 0) * new Vector3(0, 0, normal.z);
        return reflection;
    }

    async void OnCollisionEnter(Collision other) {
        Vector3 reflection = Vector3.Reflect(direction, other.GetContact(0).normal);

        if (other.collider.CompareTag("club")) {
            //soundEffects.PlayPaddleClip();
            reflection = ComputeReflection(other);
            speed = Mathf.Min(maxSpeed, speed + 0.1f);
        }
        else if (other.collider.CompareTag("ItHandle"))
        {
            Physics.IgnoreCollision(other.collider, GetComponent<Collider>());
        }
        else if (other.collider.CompareTag("MeHandle"))
        {
            Physics.IgnoreCollision(other.collider, GetComponent<Collider>());
        }
        else if (other.collider.CompareTag("PlayerWall"))
        {
            Physics.IgnoreCollision(other.collider, GetComponent<Collider>()); 
        }

        this.direction = reflection;

    }

    void OnTriggerEnter(Collider other) {
        
        if (other.CompareTag("Speedboost")) {
            speed = Mathf.Min(maxSpeed, speed + 1f);
            Destroy(other.gameObject); // a single speedboost can only be used once
        }
    }

    public Vector3 GetDirection() {
        return this.direction;
    }
    


}