using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFlollower : MonoBehaviour
{
    public float speed = 5f;
    
    private BezierCurveBuilder bezierCurveBuilder;
    private PropertyHandler propertyHandler;

    private int currentSegment = 0;

    // Start is called before the first frame update
    void Start()
    {

        // Get the BezierCurveBuilder component attached to the same GameObject
        bezierCurveBuilder = GetComponent<BezierCurveBuilder>();

        if (bezierCurveBuilder == null)
        {
            // Access properties or call methods on the BezierCurveBuilder component
            Debug.Log("BezierCurveBuilder component not found!");
        }

        propertyHandler = GetComponent<PropertyHandler>();
        if (propertyHandler == null)
        {
            Debug.LogError("Property Handler not Set!");
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (propertyHandler.caldronActionActive)
        {
            MoveAlongPath();
        }
    }

    void MoveAlongPath()
    {
        if (currentSegment >= propertyHandler.path.Length) {
            propertyHandler.pathCompleted = true;
            return;
        }

        Vector3 targetPosition = propertyHandler.path[currentSegment];
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (transform.position == targetPosition)
        {
            currentSegment++;
        }
    }
}
