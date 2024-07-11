using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPointGenerator : MonoBehaviour
{
    public GameObject monitoredHandle;

    public Vector3[] controlPointsUp;
    public Vector3[] controlPointsRight;
    public Vector3[] controlPointsDown;
    public Vector3[] controlPointsLeft;

    private Vector3[] basePoints;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Vector3[] GetDirectionFromRotation(Quaternion rotation)
    {
        // Convert the quaternion to Euler angles
        Vector3 euler = rotation.eulerAngles;
        float yRotation = euler.y;

        // Define the direction based on the Y rotation
        if (yRotation >= 315 || yRotation < 45)
        {
            return controlPointsUp; // Facing up (0 degrees)
        }
        else if (yRotation >= 45 && yRotation < 135)
        {
            return controlPointsRight; // Facing right (90 degrees)
        }
        else if (yRotation >= 135 && yRotation < 225)
        {
            return controlPointsDown; // Facing down (180 degrees)
        }
        else if (yRotation >= 225 && yRotation < 315)
        {
            return controlPointsLeft; // Facing left (270 degrees)
        }
        else
        {
            return basePoints; // Fallback for unexpected cases
        }
    }
}
