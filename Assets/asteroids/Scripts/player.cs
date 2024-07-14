using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using DualPantoToolkit;

public class player : MonoBehaviour
{
    public GameObject projectile;

    private int spawnTimer = 50;
    private float speed = 2.0f;
    //private PlayerSoundEffect soundEffects;
    private int score = 0;
    private Rigidbody rb;
    PantoHandle handle;

    public bool isUpper = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        handle = isUpper
            ? (PantoHandle)GameObject.Find("Panto").GetComponent<UpperHandle>()
            : (PantoHandle)GameObject.Find("Panto").GetComponent<LowerHandle>();
        handle.Freeze();
    }

    void FixedUpdate(){
        spawnTimer--;
        if(spawnTimer <= 0){
            Instantiate(projectile, transform.position, transform.rotation);
            spawnTimer = 50;
        }
    }
    

    async void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Enemy"))
        {
            //so sth to symbolize death
            ContactPoint contact = other.contacts[0];
            Vector3 RecoilDirection = Vector3.Normalize(transform.position - contact.point);
            await handle.MoveToPosition(transform.position + 1 * RecoilDirection, 10.0f, true);
        }
        if (other.collider.CompareTag("Wall"))
        {
            //bounce
        }
    }
}
