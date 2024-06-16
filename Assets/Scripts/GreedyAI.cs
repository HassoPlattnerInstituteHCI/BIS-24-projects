using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * tracks position of the ball, moves back to central position after hitting the ball.
 */

public class GreedyAI : MonoBehaviour
{
    public float speed = 2f;

    private Rigidbody rb;
    private Vector3 direction;
    private GameObject ball;
    

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody>();
        direction = Vector3.right;

        ball = GameObject.FindGameObjectsWithTag("Ball")[0];
    }

    private void Update()
    {
        ball = GameObject.FindGameObjectsWithTag("Ball")[0];
    }

    void FixedUpdate() {
        Vector3 ballDirection = ball.GetComponent<Ball>().GetDirection();
        if (IsBallHeadingTowardsMe()) {
            MoveTowardsBall();
        }
        else {
            MoveTowardsCenter();
        }
        rb.velocity = direction.normalized * 100 * (speed * Time.fixedDeltaTime);
    }

    bool IsBallHeadingTowardsMe() {
        Vector3 ballDirection = ball.GetComponent<Ball>().GetDirection();
        return ballDirection.z > 0;
    }

    void MoveTowardsBall() {
        direction = ball.transform.position.x > this.transform.position.x 
            ? Vector3.right
            : Vector3.left;
    }

    void MoveTowardsCenter() {
        if (0.2f < this.transform.position.x)
            direction = Vector3.left;
        else if (this.transform.position.x < -0.2f)
            direction = Vector3.right;
        else
            direction = Vector3.zero;
    }
}
