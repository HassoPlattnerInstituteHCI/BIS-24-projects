using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionSelector : MonoBehaviour
{
    private PropertyHandler propertyHandler;
    private BezierCurveBuilder curveBuilder;
    public GameObject rotationSelector;

    private Boolean selecting = false;
    // Start is called before the first frame update
    void Start()
    {
        curveBuilder = GetComponent<BezierCurveBuilder>();

        if (curveBuilder == null )
        {
            Debug.LogError("curveBuilder not found.");
        }

        propertyHandler = GetComponent<PropertyHandler>();

        if ( propertyHandler == null )
        {
            Debug.LogError("No PropertyHandler found.");
        }

        if (rotationSelector == null)
        {
            Debug.LogError("No rotationselector set");
        }


    }

    // Update is called once per frame
    void Update()
    {
        if ( selecting )
        {
            if (propertyHandler.directionSelectorActive) { return; }

            Quaternion rotation = rotationSelector.transform.rotation;
            GetDirectionFromRotation( rotation );
            selecting = false;
            return;
        }

        if (!propertyHandler.directionSelectorEnabled) { return; }
        if (!propertyHandler.directionSelectorActive) { return; }

        selecting = true;
        
    }

    void GetDirectionFromRotation(Quaternion rotation)
    {
        // Convert the quaternion to Euler angles
        Vector3 euler = rotation.eulerAngles;
        float yRotation = euler.y;

        // Define the direction based on the Y rotation
        if (yRotation >= 315 || yRotation < 45)
        {
            curveBuilder.AddPoints(curveBuilder.controlPointsUp); // Facing up (0 degrees)
        }
        else if (yRotation >= 45 && yRotation < 135)
        {
            curveBuilder.AddPoints(curveBuilder.controlPointsRight); // Facing right (90 degrees)
        }
        else if (yRotation >= 135 && yRotation < 225)
        {
            curveBuilder.AddPoints(curveBuilder.controlPointsDown); // Facing down (180 degrees)
        }
        else if (yRotation >= 225 && yRotation < 315)
        {
            curveBuilder.AddPoints(curveBuilder.controlPointsLeft); // Facing left (270 degrees)
        }
        else
        {
            Debug.LogError("Unknown Rotation value!");
            curveBuilder.AddPoints(curveBuilder.controlPointsBase); // Fallback for unexpected cases
        }
    }
}
