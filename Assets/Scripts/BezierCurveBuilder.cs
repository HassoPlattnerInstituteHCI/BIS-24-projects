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

    public int initialSegmentCount = 50; // Initial number of segments for smoothness
    private LineRenderer lineRenderer;

    private List<Vector3> controlPoints;
    private int segmentCount;

    // Start is called before the first frame update
    void Start()
    {
        propertyHandler = GetComponent<PropertyHandler>();

        if (propertyHandler == null)
        {
            Debug.LogError("Property Handler not set!");
        }

        // Initialize control points based on rotation
        controlPoints = new List<Vector3> { transform.position };

        // Setup LineRenderer
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        segmentCount = initialSegmentCount;
        lineRenderer.positionCount = segmentCount + 1;
        lineRenderer.widthMultiplier = 0.1f;

        // Draw initial Bezier curve
        DrawBezierCurve();
    }

    void DrawBezierCurve()
    {
        segmentCount = (controlPoints.Count - 1) * initialSegmentCount;
        lineRenderer.positionCount = segmentCount + 1;

        List<Vector3> points = new List<Vector3>();
        for (int i = 0; i <= segmentCount; i++)
        {
            float t = i / (float)segmentCount;
            Vector3 position = CalculateBezierPoint(t, controlPoints.ToArray());
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
        if (!propertyHandler.pathCompleted && !propertyHandler.directionSelectorActive)
        {
            return;
        }

        foreach (Vector3 point in newPoints)
        {
            // Add the relative point to the current position
            controlPoints.Add(transform.position + point);
        }

        DrawBezierCurve();
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
        switch (propertyHandler.directionSelected)
        {
            case "AIR":
                AddPoints(controlPointsUp);
                Debug.Log("UP");
                break;
            case "EARTH":
                AddPoints(controlPointsDown);
                Debug.Log("DOWN");
                break;
            case "FIRE":
                AddPoints(controlPointsLeft);
                Debug.Log("LEFT");
                break;
            case "WATER":
                AddPoints(controlPointsRight);
                Debug.Log("RIGHT");
                break;
            default:
                Debug.Log("NOPE");
                // Add default behavior
                break;
        }
    }
}
