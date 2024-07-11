using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFruit : MonoBehaviour
{
    Rigidbody rb;
    public int force;
    Vector3 vec = new Vector3(-0.2f, 0, 1);
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        // rb.AddForce(Vector3.forward * force);
        rb.AddForce(vec * force);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
