using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Player : MonoBehaviour
{
    

    private float speed = 2.0f;
    private PlayerSoundEffect soundEffects;
    private int score = 0;
    private Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start() {
        soundEffects = GetComponent<PlayerSoundEffect>();
        rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        rigidbody.velocity = direction.normalized * 100 * (speed * Time.fixedDeltaTime);
    }

    void Update() {
        
    }

    void OnCollisionEnter(Collision other) {

    }
}
