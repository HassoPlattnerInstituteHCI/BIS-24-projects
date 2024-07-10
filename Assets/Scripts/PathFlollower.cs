using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFlollower : MonoBehaviour
{
    public GameObject monitoredObject;
    public float speed = 5f;
    private int currentSegment = 0;
    private MovementDetector movementDetector;
    private BezierCurveBuilder bezierCurveBuilder;

    // Start is called before the first frame update
    void Start()
    {
        if (monitoredObject != null)
        {
            movementDetector = monitoredObject.GetComponent<MovementDetector>();
        }

        // Get the BezierCurveBuilder component attached to the same GameObject
        bezierCurveBuilder = GetComponent<BezierCurveBuilder>();

        if (bezierCurveBuilder != null)
        {
            // Access properties or call methods on the BezierCurveBuilder component
            Debug.Log("BezierCurveBuilder component found!");
        }
        else
        {
            Debug.LogError("BezierCurveBuilder component not found on the same GameObject.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (movementDetector != null && movementDetector.IsMoving)
        {
            MoveAlongPath();
        }
    }

    void MoveAlongPath()
    {
        if (currentSegment <= bezierCurveBuilder.curve.Length -1)
        {
            Vector3 targetPosition = bezierCurveBuilder.curve[currentSegment];
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            if (transform.position == targetPosition)
            {
                currentSegment++;
            }
        }
    }
}
