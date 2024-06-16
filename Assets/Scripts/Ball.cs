using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DualPantoToolkit;
public class Ball : MonoBehaviour {

    public float startingSpeed = 3f; // public attributes which can be set in the editor
    public float maxSpeed = 5f;
    private PlayerSoundEffect soundEffects;
    private float speed;
    private Vector3 initPosition = new Vector3(-1.15f, 0, -9.93f);
    private Vector3 direction; // modify this to change ball's movement
    private bool isOutOfBounds = false;
    private GameObject go;
    PantoHandle handle;

    private Rigidbody rb;
    // Start is called before the first frame update
    void Start() {
        handle = (PantoHandle)GameObject.Find("Panto").GetComponent<LowerHandle>();
        soundEffects = GetComponent<PlayerSoundEffect>();
        rb = GetComponent<Rigidbody>();
        go = GetComponent<GameObject>();
        Reset();
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

        if (other.collider.CompareTag("Player")) {
            soundEffects.PlayPaddleClip();
            reflection = ComputeReflection(other);
            speed = Mathf.Min(maxSpeed, speed + 0.1f);
        }
        else if (other.collider.CompareTag("Enemy")) {
            soundEffects.PlayPaddleClip();
            reflection = ComputeReflection(other);
            
            ContactPoint contact = other.contacts[0];
            Vector3 RecoilDirection = Vector3.Normalize(transform.position - contact.point);
            await handle.MoveToPosition(transform.position + 1.0f * RecoilDirection, 10.0f, true);
            await handle.SwitchTo(gameObject, 50.0f);
        }
        else if (other.collider.CompareTag("Wall")) {
            soundEffects.PlayWallClip();
        }
        else if (other.collider.CompareTag("PlayerScoreLine")) {
            soundEffects.PlayScoreClip();
            isOutOfBounds = true;
        }
        else if (other.collider.CompareTag("EnemyScoreLine")) {
            soundEffects.PlayPositiveScoreClip();
            isOutOfBounds = true;
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