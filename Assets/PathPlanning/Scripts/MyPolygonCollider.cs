using System.Collections;
using System.Collections.Generic;
using DualPantoToolkit;
using UnityEngine;

public class MyPolygonCollider : PantoCollider
{
    
    private Vector2[] _points2D;
    
    public override void CreateObstacle()
    {
        Debug.Log("[BIS] where am i?");
        Vector2[] points = GetComponentInChildren<PolygonCollider2D>().points;
        _points2D = new Vector2[points.Length];
        for (int i = 0; i < points.Length; i++)
        {        
            Debug.Log("[BIS] I made point " + i + "!");
            Vector3 newPoint = transform.TransformPoint(points[i].x, 0, points[i].y);
            _points2D[i] = new Vector2(newPoint.x, newPoint.z);
            
            // GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            // go.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            // go.transform.position = new Vector3(
            //     _points2D[i].x,
            //     0f,
            //     _points2D[i].y);
        }
        UpdateId();
        CreateFromCorners(_points2D);
        Debug.Log("[BIS] created obstacle");
    }
}