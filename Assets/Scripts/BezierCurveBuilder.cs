using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierCurveBuilder : MonoBehaviour
{
    public Transform[] controlPoints; // Array to hold control points
    public int segmentCount = 50; // Number of segments for smoothness
    private LineRenderer lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.positionCount = segmentCount + 1;
        lineRenderer.widthMultiplier = 0.1f;
        DrawBezierCurve();
    }

    void DrawBezierCurve()
    {
        for (int i = 0; i <= segmentCount; i++)
        {
            float t = i / (float)segmentCount;
            Vector3 position = CalculateBezierPoint(t, controlPoints[0].position, controlPoints[1].position, controlPoints[2].position, controlPoints[3].position);
            lineRenderer.SetPosition(i, position);
        }
    }

    Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        Vector3 p = uuu * p0; // (1-t)^3 * p0
        p += 3 * uu * t * p1; // 3(1-t)^2 * t * p1
        p += 3 * u * tt * p2; // 3(1-t) * t^2 * p2
        p += ttt * p3; // t^3 * p3

        return p;
    }
}
