using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using DualPantoToolkit;

public class Player : MonoBehaviour
{


    // private float speed = 2.0f;
    // private PlayerSoundEffect soundEffects;
    // private int score = 0;
    // private Rigidbody rigidbody;
    // PantoHandle handle;

    // public bool isUpper = true;

    // // Start is called before the first frame update
    // void Start()
    // {
    //     handle = isUpper
    //         ? (PantoHandle)GameObject.Find("Panto").GetComponent<UpperHandle>()
    //         : (PantoHandle)GameObject.Find("Panto").GetComponent<LowerHandle>();

    // }
    

    // async void OnCollisionEnter(Collision other)
    // {
    //     if (other.collider.CompareTag("Ball"))
    //     {
    //         ContactPoint contact = other.contacts[0];
    //         Vector3 RecoilDirection = Vector3.Normalize(transform.position - contact.point);
    //         await handle.MoveToPosition(transform.position + 1 * RecoilDirection, 10.0f, true);
    //     }
    // }
}
