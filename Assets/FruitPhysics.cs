using System;
using System.Collections;
using UnityEngine;

public class FruitPhysics : MonoBehaviour
{
    public Vector3 direction = new Vector3(-1, 0, 1);
    public Vector3 gravity = new Vector3(0, 0, -1);

    private Vector3 speed;
    private float startTime;
    public float deathTime = 10; // in seconds


    SphereCollider col;
    Rigidbody rb;

    
    
    // Start is called before the first frame update
    void Start()
    {
        speed = direction;
        startTime = Time.time;
        col = GetComponent<SphereCollider>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        speed += gravity * Time.deltaTime;
        transform.position += speed * Time.deltaTime;
        if (Time.time - startTime > deathTime) {
            Destroy(gameObject);
            
        }
    }

    void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag == "player") {
            Destroy(gameObject);
        }
    }



    
    
}
