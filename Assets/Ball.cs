using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using DualPantoToolkit;




public class Ball : MonoBehaviour
{
    public float maxSpeed = 10f; // Maximale Geschwindigkeit des Balls

    private Rigidbody rb; // Rigidbody des Balls
    private Vector3 direction; // Bewegungsrichtung des Balls
    private float speed; // Aktuelle Geschwindigkeit des Balls
    private bool isMoving = false; // Variable zur Steuerung der Bewegung des Balls
    private PantoHandle handle; // Referenz zum LowerHandle

    // Startposition des Balls (kann im Editor angepasst werden)
    private Vector3 initPosition = new Vector3(-0.55f, 0, -2.38f);

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        handle = (PantoHandle)GameObject.Find("Panto").GetComponent<LowerHandle>(); // Referenz zum LowerHandle
        ResetBall();
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            MoveBall();
        }
    }

    void MoveBall()
    {
        //speed = 50f; 
        Vector3 movement = direction.normalized * speed * Time.fixedDeltaTime;
        rb.velocity = movement;
    }

    void ResetBall()
    {
        transform.position = initPosition;
        direction = Vector3.zero;
        speed = 0f;
        rb.velocity = Vector3.zero; // Ball stoppen
        isMoving = false;
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

    void OnCollisionEnter(Collision collision)
    {
        Vector3 reflection = Vector3.Reflect(direction, collision.GetContact(0).normal);

        if (collision.collider.CompareTag("club"))
        {
            Vector3 normal = collision.GetContact(0).normal;
            //direction = Vector3.Reflect(direction, normal);
            reflection = ComputeReflection(collision);
            //speed = Mathf.Min(maxSpeed, collision.relativeVelocity.magnitude);
            speed = Mathf.Min(maxSpeed, speed + 50f);
            //MoveBall();
            //speed= maxSpeed;
            isMoving = true; // Ball beginnt sich zu bewegen
        }
        else if (collision.collider.CompareTag("wall"))
        {
            Vector3 normal = collision.GetContact(0).normal;
            direction = Vector3.Reflect(direction, normal);
            speed = Mathf.Min(maxSpeed, collision.relativeVelocity.magnitude);
            isMoving = true; // Ball beginnt sich zu bewegen
        }
        else if (collision.collider.CompareTag("ItHandle"))
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }
        this.direction = reflection;
    }
}
