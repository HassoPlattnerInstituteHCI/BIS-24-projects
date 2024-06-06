using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    public float defaultSpeed = 5f;

    private bool isOutOfBounds = false;
    private PlayerSoundEffect soundEffects;
    private float speed;

    private Vector3 direction;

    private Rigidbody rb;
    // Start is called before the first frame update
    void Start() {
        soundEffects = GetComponent<PlayerSoundEffect>();
        rb = GetComponent<Rigidbody>();
        Reset();
    }

    private void FixedUpdate() {
        Vector3 movement = direction.normalized * 100 * (speed * Time.fixedDeltaTime);
        rb.velocity = movement;
    }

    private void Update() {
       if (isOutOfBounds) {
            this.Reset();
            isOutOfBounds = false;
       }
    }

    private void Reset() {
        transform.position = Vector3.zero;
        direction = Vector3.left;
        speed = defaultSpeed;
    }

    private Vector3 ComputeReflection(Collision other) {
        Vector3 normal = other.GetContact(0).normal.normalized;
        Vector3 collisionPoint = other.GetContact(0).point;
        
        float hitFactor = (collisionPoint.z - other.transform.position.z) / other.collider.bounds.size.z;
        hitFactor = Mathf.Max(-0.5f, Mathf.Min(0.5f, hitFactor)); // disallow very steep angles
        hitFactor = normal.x == 1 ? hitFactor * (-1): hitFactor;

        Vector3 reflection = Quaternion.Euler(0, hitFactor * 45, 0) * normal;
        return reflection;
    }

    void OnCollisionEnter(Collision other) {
        Vector3 reflection = Vector3.Reflect(direction, other.GetContact(0).normal);

        if (other.collider.CompareTag("Player")) {
            soundEffects.PlayPaddleClip();
            reflection = ComputeReflection(other);
        }
        else if (other.collider.CompareTag("Enemy")) {
            soundEffects.PlayPaddleClip();
            reflection = ComputeReflection(other);
        }
        else if (other.collider.CompareTag("Wall")) {
            soundEffects.PlayWallClip();
        }
        else if (other.collider.CompareTag("ScoreLine")) {
            soundEffects.PlayScoreClip();
            isOutOfBounds = true;
        }

        this.direction = reflection;

    }
}


// talk about collision, trigger, isKinematic 
