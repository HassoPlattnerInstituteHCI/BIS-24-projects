using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    private bool playerGetScore = false;
    private bool enemyGetScore = false;
    private PlayerSoundEffect soundEffects;
    public float speed;

    private Vector3 direction;

    private Rigidbody rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        soundEffects = GetComponent<PlayerSoundEffect>();
        rigidbody = GetComponent<Rigidbody>();
        direction = CreateVectorByDegree(45);
    }

    private void FixedUpdate()
    {
        Vector3 movement = direction * speed;
        rigidbody.velocity = movement;
    }

    private void Update()
    {
        if (transform.position.z <= -16.55 || transform.position.z >= -3.16 && playerGetScore==false)
        {
            soundEffects.PlayScoreClip();
            // Destroy(gameObject, clipTime);
            playerGetScore = true;
        }
    }

    private Vector3 CreateVectorByDegree(float degrees)
    {
        var angle = degrees * Mathf.Deg2Rad;
        var x = Mathf.Cos(angle);
        var y = Mathf.Sin(angle);

        return new Vector3(x, 0, y);
    }

    void OnCollisionEnter(Collision other)
    {
        
        Vector3 normal = other.GetContact(0).normal;
        Vector3 reflection = Vector3.Reflect(direction, normal);
    
        direction = reflection;
        
        if (other.collider.CompareTag("Player") )
        {
            soundEffects.PlayPaddleClip();
        }else if (other.collider.CompareTag("Enemy"))
        {
            soundEffects.PlayPaddleClip();  
        }
        else if (other.collider.CompareTag("Wall"))
        {
            soundEffects.PlayWallClip();
        }
    
        
    
    }
    
    // void OnCollisionEnter(Collision collision)
    // {
    //     GameObject other = collision.gameObject;
    //     
    //     
    //     if (other.CompareTag("Player"))
    //     {
    //         soundEffects.PlayPaddleClip();
    //     }else if (other.CompareTag("Enemy"))
    //     {
    //         soundEffects.PlayPaddleClip();  
    //     }
    //     else if (other.CompareTag("Wall"))
    //     {
    //         soundEffects.PlayWallClip();
    //     }
    //
    //     
    //
    // }
    //
    //
    //
    
}
