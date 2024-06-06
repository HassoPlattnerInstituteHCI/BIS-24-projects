using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DumbAI : MonoBehaviour
{
    public float speed = 2f;

    private Rigidbody rb;
    private Vector3 direction;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody>();
        direction = Vector3.forward;
    }

    // Update is called once per frame
    void Update() {

    }

    private void FixedUpdate() {
        rb.velocity = direction.normalized * 100 * (speed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter(Collision other) {
        
        if (other.collider.CompareTag("Wall")) {
            this.direction *= -1;
        }
    }
}
