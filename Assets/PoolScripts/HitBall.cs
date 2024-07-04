using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DualPantoToolkit;

public class HitBall : MonoBehaviour
{
    public float startingSpeed = 3f; // public attributes which can be set in the editor
    public float maxSpeed = 5f;
    private float speed;
    // private Vector3 direction = Vector3.zero;
    PantoHandle handle;
    private Rigidbody rb;
    private GameObject goal;

    // Start is called before the first frame update
    async void Start()
    {
        handle = GameObject.Find("Panto").GetComponent<LowerHandle>();
        rb = GetComponent<Rigidbody>();
        Physics.IgnoreCollision(GameObject.Find("ItHandleGodObject").GetComponent<Collider>(), GetComponent<Collider>());
        Physics.IgnoreCollision(GameObject.Find("MeHandleGodObject").GetComponent<Collider>(), GetComponent<Collider>());
        Physics.IgnoreCollision(GameObject.Find("ItHandleGodObject").GetComponent<Collider>(), GameObject.Find("MeHandleGodObject").GetComponent<Collider>());

        goal = GameObject.FindWithTag("Finish");
        await handle.MoveToPosition(goal.transform.position);
        await handle.SwitchTo(gameObject, 10f);
    }

    void FixedUpdate() {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision other) {
        // Vector3 reflection = Vector3.Reflect(direction, other.GetContact(0).normal);
    	
        if (other.collider.CompareTag("Player")) {
            //speed = Mathf.Min(maxSpeed, speed + 0.1f);
            // rb.velocity = reflection;
            

        }
        else if (other.collider.CompareTag("Finish")) {
            Debug.Log("Goal");
        }
        //this.direction = reflection;
        
    }

}
