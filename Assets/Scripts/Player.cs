using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Player : MonoBehaviour
{
    
    private PlayerSoundEffect soundEffects;
    private Rigidbody rigidbody;
    Vector3 vec = new Vector3(1, 0, 0);

    private float speed = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        
        soundEffects = GetComponent<PlayerSoundEffect>();
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation |  RigidbodyConstraints.FreezePositionZ;
        
    }

    private void FixedUpdate()
    {
        



    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, 0).normalized;;
        transform.Translate(direction * (speed * Time.deltaTime));
        
    }

    void OnCollisionEnter(Collision other)
    {

    }
}
