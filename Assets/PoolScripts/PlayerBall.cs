using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DualPantoToolkit;

public class PlayerBall : MonoBehaviour
{
    bool free = true;
    Vector3 prevPosition = new Vector3(0, 0, 0);
    Vector3 newPosition = new Vector3(0, 0, 0);
    PantoHandle upperHandle;
    void Start()
    {

        upperHandle = GameObject.Find("Panto").GetComponent<UpperHandle>();
        Physics.IgnoreCollision(GameObject.Find("ItHandleGodObject").GetComponent<Collider>(), GetComponent<Collider>());
        Physics.IgnoreCollision(GameObject.Find("MeHandleGodObject").GetComponent<Collider>(), GetComponent<Collider>());
    }

    void FixedUpdate()
    {
        transform.position = (upperHandle.HandlePosition(transform.position));
        transform.eulerAngles = new Vector3(0, upperHandle.GetRotation(), 0);
        prevPosition = newPosition;
        newPosition = gameObject.transform.position;
    }


    void OnCollisionEnter(Collision other) {
        Vector3 travelDirection = newPosition - prevPosition;
    
        if (other.collider.CompareTag("Hitball")) {
            other.collider.GetComponent<Rigidbody>().AddForce(120 * travelDirection);
        }
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (free)
            {
                upperHandle.Freeze();
            }
            else
            {
                upperHandle.Free();
            }
            free = !free;
        }
    }
}