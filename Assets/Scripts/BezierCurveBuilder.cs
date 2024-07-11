using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierCurveBuilder : MonoBehaviour
{
    public Vector3[] controlPoints; // Array to hold control points
    public int segmentCount = 50; // Number of segments for smoothness
    private LineRenderer lineRenderer;
    public Vector3[] curve { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        InitializeControlPoints();

        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.positionCount = segmentCount + 1;
        lineRenderer.widthMultiplier = 0.1f;
        DrawBezierCurve();
    }

    void InitializeControlPoints()
    {
        // Create a new array with an extra slot
        Vector3[] newControlPoints = new Vector3[(controlPoints != null ? controlPoints.Length : 0) + 1];

        // Set the first element to the root object's position
        newControlPoints[0] = transform.position;

        // Copy existing control points to the new array starting from index 1
        if (controlPoints != null)
        {
            for (int i = 0; i < controlPoints.Length; i++)
            {
                newControlPoints[i + 1] = controlPoints[i];
            }
        }

        // Assign the new array to controlPoints
        controlPoints = newControlPoints;
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
        curve = points.ToArray();
    }

    public void AddPoint(Vector3 position)
    {
        var tempPoints = new List<Vector3>(controlPoints);
        tempPoints.Add(position);
        controlPoints = tempPoints.ToArray();
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
