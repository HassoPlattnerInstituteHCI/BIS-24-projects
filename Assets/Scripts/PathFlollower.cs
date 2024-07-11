using System;
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
    private Boolean AbleToSelectNewDirection {  get; set; }
    public GameObject directionSelector;
    public GameObject collisionReporter;
    private Quaternion selectedDirection;

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

        AbleToSelectNewDirection = false;
        selectedDirection = Quaternion.identity;
    }

    // Update is called once per frame
    void Update()
    {
        // catch stuff 
        if (bezierCurveBuilder == null)
        {
            Debug.LogError("BezierCurveBuilder component is null.");
            return;
        }

        if (bezierCurveBuilder.Curve == null)
        {
            Debug.LogError("BezierCurveBuilder.Curve is null.");
            return;
        }

        if (movementDetector == null)
        {
            Debug.LogError("movementDetector is null.");
            return;
        }

        if (movementDetector.IsMoving && currentSegment <= bezierCurveBuilder.Curve.Length - 1)
        {
            MoveAlongPath();
        }
    }

    void MoveAlongPath()
    {
        Vector3 targetPosition = bezierCurveBuilder.Curve[currentSegment];
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (transform.position == targetPosition)
        {
            currentSegment++;
        }
    }
}
