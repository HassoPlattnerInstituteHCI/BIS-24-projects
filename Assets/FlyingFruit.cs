using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DualPantoToolkit;

public class FlyingFruit : MonoBehaviour
{
    PantoHandle itHandle;
    Vector3 direction = Vector3.left;
    bool movementStarted = false;
    public float speed = 0.05f;
    async void Start()
    {
        itHandle = GameObject.Find("Panto").GetComponent<LowerHandle>();
        await itHandle.SwitchTo(gameObject, 20f);
        movementStarted = true;
    }

    void Update()
    {
        if (!movementStarted) return;
        if (transform.position.x > 2|| transform.position.x < -2)
        {
            direction *= -1;
        }
        transform.position += direction * speed;
   }


    void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag ("MeHandle"))
        {
            Debug.Log("Entered");
        }
    }

}