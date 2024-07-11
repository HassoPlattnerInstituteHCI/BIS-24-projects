using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierCurveBuilder : MonoBehaviour
{
    private PropertyHandler propertyHandler;

    public Vector3[] controlPointsUp; // Array to hold control points for "Up" direction
    public Vector3[] controlPointsRight; // Array to hold control points for "Right" direction
    public Vector3[] controlPointsLeft; // Array to hold control points for "Left" direction
    public Vector3[] controlPointsDown; // Array to hold control points for "Down" direction
    public Vector3[] controlPointsBase = new Vector3[0]; // Default empty array to avoid null reference

    public int segmentCount = 50; // Number of segments for smoothness
    private LineRenderer lineRenderer;

    private Vector3[] controlPoints;

    // Start is called before the first frame update
    void Start()
    {

        propertyHandler = GetComponent<PropertyHandler>();

        if (propertyHandler == null)
        {
            Debug.LogError("Property Handler not set!");
        }

        // Initialize control points based on rotation
        controlPoints = InitializeControlPoints();

        // Setup LineRenderer
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.positionCount = segmentCount + 1;
        lineRenderer.widthMultiplier = 0.1f;

        // Draw initial Bezier curve
        DrawBezierCurve();
    }

    Vector3[] InitializeControlPoints()
    {
        // Create a new array with an extra slot
        Vector3[] newControlPoints = new Vector3[1];
        newControlPoints[0] = transform.position; // Set the first element to the root object's position
        return newControlPoints;
    }

    void DrawBezierCurve()
    {
        List<Vector3> points = new List<Vector3>();
        for (int i = 0; i <= segmentCount; i++)
        {
            float t = i / (float)segmentCount;
            Vector3 position = CalculateBezierPoint(t, controlPoints);
            points.Add(position);

            if (lineRenderer == null)
            {
                Debug.LogError("LineRenderer component could not be added!");
                return;
            }

            lineRenderer.SetPosition(i, position);
        }
        propertyHandler.path = points.ToArray();
    }

    public void AddPoints(Vector3[] newPoints)
    {
        if (!propertyHandler.pathCompleted &&  !propertyHandler.directionSelectorActive){ return; }
        Vector3[] combinedArray = new Vector3[controlPoints.Length + newPoints.Length];
        Array.Copy(controlPoints, combinedArray, controlPoints.Length);
        Array.Copy(newPoints, 0, combinedArray, controlPoints.Length, newPoints.Length);
        controlPoints = combinedArray;
        DrawBezierCurve();
        propertyHandler.directionSelected = -1f;
        propertyHandler.pathCompleted = false;
        Debug.Log("Adding Points");
    }

    Vector3 CalculateBezierPoint(float t, Vector3[] points)
    {
        int n = points.Length;
        Vector3[] tempPoints = new Vector3[n];

        for (int i = 0; i < n; i++)
        {
            tempPoints[i] = points[i];
        }

        for (int r = 1; r < n; r++)
        {
            for (int i = 0; i < n - r; i++)
            {
                tempPoints[i] = (1 - t) * tempPoints[i] + t * tempPoints[i + 1];
                tempPoints[i].y = 1;
            }
        }

        return tempPoints[0];
    }

    public void TranslateRotation()
    {
        float yRotation = propertyHandler.directionSelected;
        if (yRotation >= 315 || yRotation < 45)
        {
            AddPoints(controlPointsUp); // Facing up (0 degrees)
        }
        else if (yRotation >= 45 && yRotation < 135)
        {
            AddPoints(controlPointsRight); // Facing right (90 degrees)
        }
        else if (yRotation >= 135 && yRotation < 225)
        {
            AddPoints(controlPointsDown); // Facing down (180 degrees)
        }
        else if (yRotation >= 225 && yRotation < 315)
        {
            AddPoints(controlPointsLeft); // Facing left (270 degrees)
        }
        else
        {
            Debug.LogError("Unknown Rotation value!");
        }
       
    }
}
