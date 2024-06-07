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
        direction = Vector3.forward;

        ball = GameObject.Find("Ball");
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
        return ballDirection.x > 0;
    }

    void MoveTowardsBall() {
        direction = ball.transform.position.z > this.transform.position.z 
            ? Vector3.forward
            : Vector3.back;
    }

    void MoveTowardsCenter() {
        direction = (-0.2f < this.transform.position.z && this.transform.position.z < 0.2f)
            ? Vector3.back
            : Vector3.forward;
    }
}
