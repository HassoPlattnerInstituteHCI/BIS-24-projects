using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * moves in a straight line and bounces off walls.
 */
public class DumbAI : MonoBehaviour
{
    public float speed = 2f;

    private Rigidbody rb;
    private Vector3 direction;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody>();
        direction = Vector3.right;
    }

    // Update is called once per frame
    void Update() {

    }
    // FixedUpdate is called once per physics update
    void FixedUpdate() {
        rb.velocity = direction.normalized * 100 * (speed * Time.fixedDeltaTime);
    }

    void OnCollisionEnter(Collision other) {
        
        if (other.collider.CompareTag("Wall")) {
            this.direction *= -1;
        }
    }
}
