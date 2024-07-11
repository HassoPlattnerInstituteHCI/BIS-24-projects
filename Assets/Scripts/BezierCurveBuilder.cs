using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierCurveBuilder : MonoBehaviour
{
    public Vector3[] controlPointsUp; // Array to hold control points for "Up" direction
    public Vector3[] controlPointsRight; // Array to hold control points for "Right" direction
    public Vector3[] controlPointsLeft; // Array to hold control points for "Left" direction
    public Vector3[] controlPointsDown; // Array to hold control points for "Down" direction
    public Vector3[] controlPointsBase = new Vector3[0]; // Default empty array to avoid null reference

    public int segmentCount = 50; // Number of segments for smoothness
    private LineRenderer lineRenderer;
    public Vector3[] Curve { get; private set; }

    private Vector3[] controlPoints;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize control points based on rotation
        controlPoints = InitializeControlPoints();
        Vector3[] directionPoints = new Vector3[] { Vector3.zero };
        AddPoints(directionPoints);

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
            lineRenderer.SetPosition(i, position);
        }
        Curve = points.ToArray();
    }

    public void AddPoints(Vector3[] newPoints)
    {
        Vector3[] combinedArray = new Vector3[controlPoints.Length + newPoints.Length];
        Array.Copy(controlPoints, combinedArray, controlPoints.Length);
        Array.Copy(newPoints, 0, combinedArray, controlPoints.Length, newPoints.Length);
        controlPoints = combinedArray;
        DrawBezierCurve();
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
            }
        }

        return tempPoints[0];
    }
}
