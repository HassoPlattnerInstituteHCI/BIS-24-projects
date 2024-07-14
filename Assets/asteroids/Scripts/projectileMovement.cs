using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileMovement : MonoBehaviour
{
    public float speed = 5f;

    private Vector3 direction;
    private Rigidbody rb;

    void Start(){
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    async void OnCollisionEnter(Collision other) {
        if (other.collider.CompareTag("Enemy")) {
            //play sfx
            //substract enemy life
            Destroy(gameObject);
        }
        else if (other.collider.CompareTag("Wall")) {
            //play sfx
            Destroy(gameObject);
        }
    }
}
