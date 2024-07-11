using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionSelector : MonoBehaviour
{
    private PropertyHandler propertyHandler;
    private BezierCurveBuilder curveBuilder;
    public GameObject rotationSelector;

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
        if (!propertyHandler.pathCompleted && propertyHandler.directionSelectorActive) { return; }
        GetDirectionFromRotation(rotationSelector.transform.rotation);

    }

    void GetDirectionFromRotation(Quaternion rotation)
    {
        
        // Convert the quaternion to Euler angles
        Vector3 euler = rotation.eulerAngles;
        float yRotation = euler.y;

        // Define the direction based on the Y rotation

        propertyHandler.directionSelected = yRotation;
    }
}
