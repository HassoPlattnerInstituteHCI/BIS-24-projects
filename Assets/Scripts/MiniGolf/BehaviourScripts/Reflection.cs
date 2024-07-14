using System.Collections;
using System.Collections.Generic;   
using UnityEngine;

public class Reflection : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 velocity;

  // Use this for initialization
  void Start()
  {
    rb = this.GetComponent<Rigidbody>();
  }

  void OnCollisionEnter(Collision collision){
    ReflectProjectile(rb, collision.contacts[0].normal);
  }

  private void ReflectProjectile(Rigidbody rb, Vector3 reflectVector)
  {    
    velocity = Vector3.Reflect(rb.velocity, reflectVector);
    Debug.Log("Start: " + rb.velocity + "End: " + velocity);
    rb.velocity = velocity;
  }
}
